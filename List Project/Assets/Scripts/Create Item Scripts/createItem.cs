using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class createItem : MonoBehaviour
{
    public lists l;
    public InputField InputItemName;
    public InputField InputItemDesc;
    public InputField InputItemVal;
    public int CreateSwitch;
    public int DestroySwitch;
    public TMP_Text CurrentCreateModeText;
    public GameObject TagPrefab;
    public List<char> alphabet;
    public List<string> RandomNames;
    public Transform TagContent;
    public Transform TagContentListed;
    public GameObject CreateObjectsParent;
    public Button CreateItemButton;

    


    public List<tag> sortedTags = new List<tag>();


  

    public void Awake()
    {
        
    }
    public void SwitchDestroyThings()
    {
        Application.Quit();
       switch(DestroySwitch)
        {
            case 0:
                CreateObjectsParent.SetActive(false);
                DestroySwitch = 1;
                return;
            case 1:
                CreateObjectsParent.SetActive(true);
                DestroySwitch = 0;
                return;
        }
    }

    public void SwitchCreateThings()
    {
        switch(CreateSwitch)
        {   
            case 0:
                for(int i = 0;i <TagContent.childCount;i++)
                {
                    Destroy(TagContent.GetChild(i).gameObject);
                }

                for(int i = 0;i <TagContentListed.childCount;i++)
                {
                    Destroy(TagContentListed.GetChild(i).gameObject);
                }
                CreateSwitch = 1;
                return;
            case 1:
              
                CreateSwitch = 2;
                return;
            case 2:
                tagCreate();
                ComparerByNameTag c = new ComparerByNameTag();
                l.t.data.tags.Sort(c);
                CreateSwitch = 0;
                return;

                
        }


        if(CreateSwitch == 1)
        {
            Debug.Log("lol");
           
        }
        else
        {
           
        }
    }

    
  


    public void Start()
    {

        


    //   string alphabet1 = "abcdefghijklmnopqrstuvwxyz";
    //
    //
    //   for(int i = 0;i < 26;i++)
    //   {
    //       alphabet.Add(alphabet1.ToCharArray(0, 26)[i]);
    //   }
    //
    //   for(int i = 0;i < 250;i++)
    //   {
    //       RandomNames.Add(alphabet[Random.Range(0, 25)].ToString() + alphabet[Random.Range(0, 25)] + alphabet[Random.Range(0, 25)] + alphabet[Random.Range(0, 25)] +
    //           alphabet[Random.Range(0, 25)] + alphabet[Random.Range(0, 25)] + alphabet[Random.Range(0, 25)]  + "");
    //   }
    //
    // for(int i = 0; i < 250; i++)
    //   {
    //       l.t.data.tags.Add(l.newTag(RandomNames[i], "bla", null));
    //   }
    //   ComparerByNameTag c = new ComparerByNameTag();
    //   l.t.data.tags.Sort(c);
    //
    //   tagCreate();
    //
    }


    public void tagDelete()
    {
        tag m = l.t.data.tags[0];
        l.DestroyThis = m;
        l.t.data.tags.Remove(m);
       
        
    }
    public void tagCreate()
    {

       

        for(int i = 0;i <TagContent.childCount;i++)
        {
            Destroy(TagContent.GetChild(i).gameObject);
        }


        for(int i = 0;i <TagContentListed.childCount;i++)
        {
            Destroy(TagContentListed.GetChild(i).gameObject);
        }



        if(l.listedTags.Count != 0)
        {
            for(int i = 0;i < l.t.data.tags.Count;i++)
            {
                bool isNull = false;


                foreach(tag c in l.listedTags)
                {
                    if(c == l.t.data.tags[i])
                    {
                        isNull = true;
                    }
                }


                if(isNull == false)
                {
                    GameObject prefab = Instantiate(TagPrefab, TagContent);
                    prefab.name = l.t.data.tags[i].tagname + i;
                    tag thisTag = l.t.data.tags[i];
                    Text nameText = prefab.transform.GetChild(0).GetComponent<Text>();
                    nameText.text = l.t.data.tags[i].tagname;
                    Text descText = prefab.transform.GetChild(1).GetComponent<Text>();
                    descText.text = l.t.data.tags[i].tagdesc;

                    prefab.transform.GetChild(2).GetComponent<uiTag>().Tag = thisTag;
                }
                else
                {

                    //the universe is rehearsal of the fuckin' hell
                    GameObject prefab = Instantiate(TagPrefab, TagContentListed);
                    prefab.name = l.t.data.tags[i].tagname + i;
                    tag thisTag = l.t.data.tags[i];
                    Text nameText = prefab.transform.GetChild(0).GetComponent<Text>();
                    nameText.text = l.t.data.tags[i].tagname;
                    Text descText = prefab.transform.GetChild(1).GetComponent<Text>();
                    descText.text = l.t.data.tags[i].tagdesc;

                    prefab.transform.GetChild(2).GetComponent<uiTag>().Tag = thisTag;
                }

            }
        }
        else
        {
            for(int i = 0; i < l.t.data.tags.Count; i++)
            {

                GameObject prefab = Instantiate(TagPrefab, TagContent);
                prefab.name = l.t.data.tags[i].tagname + i;
                tag thisTag = l.t.data.tags[i];
                Text nameText = prefab.transform.GetChild(0).GetComponent<Text>();
                nameText.text = l.t.data.tags[i].tagname;
                Text descText = prefab.transform.GetChild(1).GetComponent<Text>();
                descText.text = l.t.data.tags[i].tagdesc;

                prefab.transform.GetChild(3).GetComponent<uiTag>().Tag = thisTag;
            }
        }

       
    }

    public void reSortObject()
    {


        

        
        for(int i = 0;i <TagContent.childCount;i++)
        {
           Destroy(TagContent.GetChild(i).gameObject);
        }

       
       
        for(int i = 0;i < l.t.data.tags.Count;i++)
        {
            bool isNull = false;

         
             foreach(tag c in l.listedTags)
            {
                if(c == l.t.data.tags[i])
                {
                    isNull = true;
                }
            }

             foreach(tag c in l.destroyListedTags)
            {
                if(c == l.t.data.tags[i])
                {
                    isNull = true;
                }
            }
              
       
            if(isNull == false)
            {
                GameObject prefab = Instantiate(TagPrefab, TagContent);
                prefab.name = l.t.data.tags[i].tagname + i;
                tag thisTag = l.t.data.tags[i];
                Text nameText = prefab.transform.GetChild(0).GetComponent<Text>();
                nameText.text = l.t.data.tags[i].tagname;
                Text descText = prefab.transform.GetChild(1).GetComponent<Text>();
                descText.text = l.t.data.tags[i].tagdesc;

                prefab.transform.GetChild(3).GetComponent<uiTag>().Tag = thisTag;
            }
           
        }
       
    }

   
    public void Update()
    {
        if(CreateSwitch == 0)
        {
            InputItemName.interactable = true;
            InputItemDesc.interactable = true;
            InputItemVal.interactable = true;
            CurrentCreateModeText.text = "Current create mode: item";
        }
        else if(CreateSwitch == 1)
        {
            InputItemName.interactable = true;
            InputItemDesc.interactable = true;
            InputItemVal.interactable = false;
            CurrentCreateModeText.text = "Current create mode: tag";
        }
        else if(CreateSwitch == 2)
        {
            InputItemName.interactable = true;
            InputItemDesc.interactable = false;
            InputItemVal.interactable = false;
            CurrentCreateModeText.text = "Current create mode: tagEnum";
        }

        if(InputItemName.text.Length != 0 && InputItemDesc.text.Length != 0 && InputItemVal.text.Length != 0)
        {
            if(!InputItemName.text.StartsWith(" ") && !InputItemDesc.text.StartsWith(" ") && !InputItemVal.text.StartsWith("-") && !InputItemVal.text.StartsWith("0"))
            {
                CreateItemButton.interactable = true;
            }
            else
            {
                CreateItemButton.interactable = false;
            }
        }
        else
        {
            CreateItemButton.interactable = false;
        }
    }

    public void CreateNewItem()
    {
        if(CreateSwitch == 0)
        {
            l.t.data.items.Add(l.newItem(InputItemName.text, InputItemDesc.text, l.listedTags, int.Parse(InputItemVal.text)));
            l.sortedItems.Clear();
            l.sortItemsByValue();
        }
        else if(CreateSwitch == 1)
        {
            l.t.data.tags.Add(l.newTag(InputItemName.text, InputItemDesc.text, null));
        }
        else if(CreateSwitch == 2)
        {
            l.t.data.tagEnums.Add(l.newTagEnum(InputItemName.text));

        }
    }



}

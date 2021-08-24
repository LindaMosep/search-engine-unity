using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class lists : MonoBehaviour
{
    
    //i prefer two-step save system - iki adımlı save sistemi kullanıyorum.
    public DatalarAraci t;

    public List<item> sortedItems;
    public Tooltip tooltip;
    public int currentPage;
    public Text CurrentPageText;
    public TMP_Text CurrentPageTMP;
    public List<char> alphabet;
    public List<string> RandomNames;
    public tag DestroyThis;
    public Text FPSText;
    public float deltaTime;
    public List<tag> listedTags= new List<tag>();
    public List<tag> destroyListedTags= new List<tag>();
    public Slider changePage;
    public Text NoItemText;
    public TMP_Text NoItemTextTMP;
    public Text switchSortButtonText;
    int switchSort;
    public Image LoadingImage;
    public void Start()
    {
        UnityEditor.EditorApplication.isPlaying = false;
      // t.data.items.Clear();
      //
      // string alphabet1 = "abcdefghijklmnopqrstuvwxyz";
      // Debug.Log(t.data.tags.Count);
      //
      // for(int i = 0; i < 26; i++)
      // {
      //     alphabet.Add(alphabet1.ToCharArray(0, 26)[i]);
      //    
      // }
      //
      // for(int i = 0; i < 10000; i++)
      // {
      //     string randomname = alphabet[Random.Range(0, 25)].ToString() + alphabet[Random.Range(0, 25)] + alphabet[Random.Range(0, 25)] + alphabet[Random.Range(0, 25)] +
      //          alphabet[Random.Range(0, 25)] + "";
      //     while(RandomNames.Contains(randomname))
      //     {
      //         randomname += alphabet[Random.Range(0, 25)];
      //     }
      //     RandomNames.Add(randomname);
      //    
      // }
      //
      //
      // List<int> values = new List<int>();
      // for(int i = 0; i < 10000; i++)
      // {
      //     values.Add(i);
      // }
      //
      //for(int i = 0; i < 10000; i++)
      //{
      //    int random = Random.Range(0, values.Count);
      //    t.data.items.Add(newItem(RandomNames[i], "jojo", null, values[random]));
      //     
      //    
      //    values.RemoveAt(random);
      //}
      //
      // List<int> randomValues = new List<int>();

        

       // for(int i = 0; i < t.data.items.Count;i++)
       // {
       //     int random = Random.Range(5, 11);
       //
       //     for(int m = 0; m < random; m++)
       //     {
       //         int random2 = Random.Range(0, t.data.tags.Count);
       //         List<tag> bobo =  t.data.items[i].tags;
       //
       //         if(!bobo.Contains(t.data.tags[random2]))
       //         {
       //             t.data.items[i].tags.Add(t.data.tags[random2]);
       //         }
       //    
       //     }
       //     
       //     Debug.Log(t.data.items[i].tags.Count);
       //
       //
       //
       //
       // }


       // randomValues.Clear();
       // values.Clear();
        RandomNames.Clear();
       
       
       
      sortItemsByName();

        switchSort = 0;
        switchSortButtonText.text = "Current Sort Mode: " + " Alphabetically";
        currentPage = 1;

        timeflows = 1;
        UnityEditor.EditorApplication.isPlaying = true;
    }
  
    public void SwitchSort()
    {
        if(switchSort == 0)
        {
            sortItemsByValue();
            switchSortButtonText.text = "Current Sort Mode: " + " Value";
            switchSort++;
        }
        else if(switchSort == 1)
        {
            SortItemsByIndex();
            switchSortButtonText.text = "Current Sort Mode: " + " Date";
            switchSort++;
        }
        else if(switchSort == 2)
        {
            sortItemsByName();
            switchSortButtonText.text = "Current Sort Mode: " + " Alphabetically";
            switchSort = 0;
        }
    }
    public void SortItemsByIndex()
    {
        sortedItems.Clear();
        
        for(int i = 0; i < t.data.items.Count; i++)
        {
            sortedItems.Add(t.data.items[i]);
        }
    }
    public void sortItemsByValue()
    {
        sortedItems.Clear();

        for(int i = 0;i < t.data.items.Count;i++)
        {
            sortedItems.Add(t.data.items[i]);
        }


        ComparerByValue c = new ComparerByValue();
        sortedItems.Sort(c);

        RefreshIndex(sortedItems);

    }

    public void sortItemsByName()
    {
        sortedItems.Clear();

        for(int i = 0;i < t.data.items.Count;i++)
        {
            sortedItems.Add(t.data.items[i]);
        }


        ComparerByName c = new ComparerByName();
        sortedItems.Sort(c);

        RefreshIndex(sortedItems);
    }

    public void sortItemsByDate()
    {
        sortedItems.Clear();

        for(int i = 0;i < t.data.items.Count;i++)
        {
            sortedItems.Add(t.data.items[i]);
        }


        ComparerByDate c = new ComparerByDate();
        sortedItems.Sort(c);

        RefreshIndex(sortedItems);
    }


    public item newItem(string name, string desc, List<tag> tags, int value)
    {
        item itemToAdd = new item(-1,name,desc, tags, value, System.DateTime.Now);
        return itemToAdd;
        

    }

    public tag newTag(string name, string desc,List<tagEnum> tagenum)
    {
        tag tagToAdd = new tag(-1, name, desc,tagenum);
        return tagToAdd;
    }

    public tagEnum newTagEnum(string name)
    {
        tagEnum tagEnumToAdd = new tagEnum(-1, name);
        return tagEnumToAdd;
    }

    

    public void RefreshIndex(List<item> itemlist)
    {
        if(t.data.items.Count != 0)
        {
            foreach(var item in itemlist)
            {
                if(item.indexNo != itemlist.FindIndex(indexeditem => indexeditem == item))
                {
                    item.indexNo = itemlist.FindIndex(indexeditem => indexeditem == item);
                }
               

            }
        }

        if(t.data.tagEnums.Count != 0)
        {
            foreach(var tagenums in t.data.tagEnums)
            {
                if(tagenums.enumIndexNo != t.data.tagEnums.FindIndex(indexeditem => indexeditem == tagenums))
                {
                    tagenums.enumIndexNo = t.data.tagEnums.FindIndex(indexeditem => indexeditem == tagenums);
                }
               

            }
        }

        if(t.data.tags.Count != 0)
        {
            foreach(var tag in t.data.tags)
            {
                if(tag.tagIndexNo != t.data.tags.FindIndex(indexeditem => indexeditem == tag))
                {
                    tag.tagIndexNo = t.data.tags.FindIndex(indexeditem => indexeditem == tag);
                }
                

            }
        }
    }


    float timeflows;
    public void Update()
    {
        CurrentPageText.text = currentPage.ToString();
        CurrentPageTMP.text = currentPage.ToString();
        

        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        timeflows += Time.deltaTime;
        float fps = 1.0f / deltaTime;
        if(timeflows >= 0.1)
        {
            FPSText.text = "FPS:" + Mathf.Ceil(fps).ToString();
            timeflows -= timeflows;
        }
       
       
        changePage.minValue = 1;
        if(sortedItems.Count > 0)
        {
            NoItemText.gameObject.SetActive(false);
            NoItemTextTMP.gameObject.SetActive(false);
            double items = sortedItems.Count;
            double pagecount = items / 8;
            if(sortedItems.Count > 8)
            {
                if((int)pagecount != pagecount)
                {
                    changePage.maxValue = (int)pagecount + 1;
                }
                else
                {
                    changePage.maxValue = (int)pagecount;
                }
                
            }
            else
            {
             
                changePage.maxValue = 1;
            }
            
        }
        else
        {
            NoItemText.gameObject.SetActive(!false);
            NoItemTextTMP.gameObject.SetActive(!false);
            changePage.maxValue = 1;
        }

        currentPage = (int)changePage.value;

         
    }





}

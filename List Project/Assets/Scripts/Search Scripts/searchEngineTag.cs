using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;



public class searchEngineTag : MonoBehaviour
{
    public InputField inputName;
    public char[] charArray = new char[25];
    public int length;
    public bool isHappened;
    public string oldSearched;

    public List<tag> searchedTags = new List<tag>();
    public List<searchTag> searchedTagsPercent = new List<searchTag>();
    public List<tag> selectedTags = new List<tag>();
    public Transform selectedResultContent;
    public GameObject selectedResultPrefab;
    
    public lists l;
    public int switchSearchInt;
    public int currentPage;

    public Toggle MatchAnyLettersToggle;
    public Button nextPageButton;
    public Button previousPageButton;
    public Text CurrentPageText;
    public Text ResultCountText;
    public bool pageChanged;

    public List<uiResultTag> results = new List<uiResultTag>();
    public Text switchSearchText;
    public Button ApplySelectedButton;
    public searchEngine s;




    public void ApplySelected()
    {
        List<tag> tags = new List<tag>();
        for(int i = 0; i < searchedTagsPercent.Count; i++)
        {
            tags.Add(searchedTagsPercent[i].tag);
        }
        for(int i = 0; i < results.Count; i++)
        {
            if(results[i].toggleSelected.isOn)
            {
                var selected =results[i].result.tag;
                selectedTags.Add(selected);
                if(tags.Contains(selected))
                {
                    searchedTagsPercent.Remove(searchedTagsPercent.Find(m => m.tag == selected));
                }
                GameObject prefab = Instantiate(selectedResultPrefab, selectedResultContent);
                var scr = prefab.transform.GetChild(1).GetComponent<uiTagResultsPrefab>();
                scr.thisTag = selected;
            }
        }



        for(int i = 0; i < results.Count; i++)
        {
            results[i].toggleSelected.isOn = false;
        }
        
       
    }

    public void NextPage()
    {
        currentPage++;
        for(int i = 0;i < results.Count;i++)
        {
            results[i].toggleSelected.isOn = false;

        }

    }
    public void PreviousPage()
    {
        currentPage--;
        for(int i = 0;i < results.Count;i++)
        {
            results[i].toggleSelected.isOn = false;

        }

    }
    
    void Start()
    {
        switchSearchText.text = "Current search System: match any string";

        currentPage = 1;
    }

    public void SwitchSearchSystem()
    {
        switch(switchSearchInt)
        {
            case 0:
                switchSearchInt = 1;
                switchSearchText.text = "Current search System: match the initials";
                searchButton();
                break;
            case 1:
                switchSearchInt = 0;
                switchSearchText.text = "Current search System: match any string";


                searchButton();


                break;
        }
    }

    public void onToggleClick()
    {
        if(length > 0 && switchSearchInt == 0)
        {

            searchButton();
        }
    }

    public void searchButton()
    {
        currentPage = 1;
        searchedTagsPercent.Clear();
        for(int i = 0; i < results.Count; i++)
        {
            if(results[i].toggleSelected.gameObject.activeSelf)
            {
                results[i].toggleSelected.isOn = false;
            }
           
            
        }

      

        searchedTags.AddRange(l.t.data.tags);

        for(int i = 0; i < s.selectedResult.tags.Count; i++)
        {
            searchedTags.Remove(searchedTags.Find(xc => xc.tagIndexNo == s.selectedResult.tags[i].tagIndexNo));
           
        }

        for(int i = 0;i < selectedTags.Count;i++)
        {
            searchedTags.Remove(selectedTags[i]);
        }

        
        List<char> characters = new List<char>();

        foreach(var m in charArray)
        {
            if(m != ' ')
            {
                characters.Add(m);
            }
        }

        if(switchSearchInt == 0)
        {
           
                if(MatchAnyLettersToggle.isOn)
                {
                     
                    for(int i = 0;i < searchedTags.Count;i++)
                    {
                        if(searchedTags[i].tagname.Contains(inputName.text))
                        {
                         searchTag searchTag =
                                 new searchTag(searchedTags[i], Methods.percentC(length, searchedTags[i].tagname.Length));
                             searchedTagsPercent.Add(searchTag);
                        }
                    }
                }
                else
                {
                   
                    if(length >= 2)
                    {
                   
                    for(int i = 0; i < searchedTags.Count; i++)
                        {
                            if(searchedTags[i].tagname.Contains(inputName.text))
                            {
                                searchTag searchTag =
                                new searchTag(searchedTags[i], Methods.percentC(length, searchedTags[i].tagname.Length));
                                searchedTagsPercent.Add(searchTag);


                            }
                        }

                        
                        
                    }
                    else if(length < 2)
                    {

                    
                        for(int i = 0; i < searchedTags.Count; i++)
                        {
                             List<char> itemsChar = (from char xx in searchedTags[i].tagname select xx).ToList();
                             var percentCounter = 0;
                            
                             if(itemsChar.Count >= characters.Count)
                             {
                                    

                                for(int i1 = 0; i1 < characters.Count; i1++)
                                {
                                  if(i1 == 0 && itemsChar[i1] == characters[i1])
                                  {
                                    percentCounter++;
                                  }


                                    if(percentCounter == i1)
                                    {
                                            if(itemsChar[i1] == characters[i1])
                                            {
                                                percentCounter++;
                                            }
                                    }


                                    if(i1 == characters.Count - 1)
                                    {
                                        if(percentCounter == characters.Count)
                                        {
                                            if(percentCounter > 0)
                                            {
                                                if(Methods.percentC(percentCounter, itemsChar.Count) > 0)
                                                {


                                                    var searchTag = new searchTag(searchedTags[i], Methods.percentC(percentCounter, itemsChar.Count));

                                                     searchedTagsPercent.Add(searchTag);
                                                }



                                            }
                                        }
                                    }
                                    
                                }
                             }


                        }
                    }



                }


            
            
        }
        else if(switchSearchInt == 1)
        {


            for(int i = 0;i < searchedTags.Count;i++)
            {
                List<char> itemsChar = (from char xx in searchedTags[i].tagname select xx).ToList();
                var percentCounter = 0;

                if(itemsChar.Count >= characters.Count)
                {


                    for(int i1 = 0;i1 < characters.Count;i1++)
                    {
                        if(i1 == 0 && itemsChar[i1] == characters[i1])
                        {
                            percentCounter++;
                        }


                        if(percentCounter == i1)
                        {
                            if(itemsChar[i1] == characters[i1])
                            {
                                percentCounter++;
                            }
                        }


                        if(i1 == characters.Count - 1)
                        {
                            if(percentCounter == characters.Count)
                            {
                                if(percentCounter > 0)
                                {
                                    if(Methods.percentC(percentCounter, itemsChar.Count) > 0)
                                    {

                                        
                                        searchTag searchTag = new searchTag(searchedTags[i], Methods.percentC(percentCounter, itemsChar.Count));

                                        searchedTagsPercent.Add(searchTag);
                                    }



                                }
                            }
                        }

                    }
                }


            }


        }


        
        searchedTags.Clear();
       
        var comparer = new ComparerByTagPercent();
        searchedTagsPercent.Sort(comparer);
    }

    // Update is called once per frame
    void Update()
    {
        length = inputName.text.Length;

        if(length != 0)
        {
            if(length == 1)
            {
                if(searchedTagsPercent.Count == 0)
                {
                    oldSearched = inputName.text + inputName.text.ToCharArray(0, length)[0];
                }
            }


            isHappened = false;
            if(oldSearched != inputName.text)
            {

                

                for(int i = 0; i < 25; i++)
                {
                    if(i < length)
                    {
                        charArray[i] = inputName.text.ToCharArray(0, length)[i];
                    }
                    else
                    {
                        charArray[i] = ' ';
                    }

                    if(i == 24)
                    {
                        searchedTagsPercent.Clear();
                        searchButton();
                        oldSearched = inputName.text;
                    }
                }
            }
        }
        else
        {
            if(!isHappened)
            {
                for(int i = 0; i < 25; i++)
                {
                    charArray[i] = ' ';
                    if(i == 24)
                    {
                        isHappened = true;
                    }
                }
            }
            searchedTagsPercent.Clear();
        }


        double r = ((currentPage + 1) * 6) -6;
        double itemCountNext = searchedTagsPercent.Count / r;

        if(itemCountNext > 1)
        {
            nextPageButton.interactable = true;
        }
        else
        {
            nextPageButton.interactable = false;
        }

        if(currentPage != 1)
        {
            previousPageButton.interactable = true;
        }
        else
        {
            previousPageButton.interactable = false;
        }


        if(searchedTagsPercent.Count > 0)
        {
            CurrentPageText.text = currentPage + "";
            ResultCountText.text = searchedTagsPercent.Count + " " + "Result founded.";

        }
        else if(searchedTagsPercent.Count == 0 && length > 0)
        {
            CurrentPageText.text = ":3";
            ResultCountText.text = "Kek.";
        }
        else if(length == 0)
        {
            CurrentPageText.text = "Hey.";
            ResultCountText.text = "Wants search?.";
        }

        bool isOn = false;
        if(!isOn)
        {
            foreach(var m in results)
            {
                if(m.toggleSelected.gameObject.activeSelf)
                {
                    if(m.toggleSelected.isOn)
                    {
                        isOn = true;
                    }

                }
            }
        }
       

        if(isOn)
        {
            ApplySelectedButton.interactable = true;
        }
        else
        {
            ApplySelectedButton.interactable = !true;
        }


    }
}

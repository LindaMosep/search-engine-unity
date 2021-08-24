using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;
using UnityEngine.Events;


public class searchEngine: MonoBehaviour
{
    public lists l;

    public List<item> searchedItems = new List<item>();
    public List<searchItem> searchedItemsPercent = new List<searchItem>();
    public InputField inputName;

    public char[] charArray = new char[25];
    public int length;
    public bool isHappened;
    public string oldSearched;
    public item test;

   
    


   
    public List<char> alphabet;
    public List<char> numbers;
    public List<string> RandomNames;

    public int currentPage;
    public Text CurrentPageText;
    public Button nextPageButton;
    public Button previousPageButton;
    public Text ResultCountText;
  

    public List<tag> oneOfThemTags = new List<tag>();
    public List<tag> searchedTags = new List<tag>();

    public int switchSearchInt;
    public Text switchSearchText;

    public Toggle MatchAnyLettersToggle;


    public item selectedResult;
    public item selectedResultOld;
    public GameObject searchResultParentObj;
    public bool resultChanged;

    public InputField sResultNameInputF;
    public InputField sResultDescInputF;
    public InputField sResultValueInputF;
    public Text sResultTagCountText;
    public Button ApplyButton;
    public Button RevertButton;
    public Button ClearItemButton;

    public GameObject tagPrefab;
    public GameObject tagPrefab2;
    public Transform tagsScrollView;
    public Transform tagsScrollViewTrash;
    public bool tagBacksToScrollView;
    public Button resortTagListButton;

    public List<tag> selectedResultTrashTag = new List<tag>();
    public searchEngineTag s;
    public GameObject tagSearchScreenObj;
    public Toggle AutoResortToggle;
    public Button AddNewTagButton;


    public void ApplyChanges()
    {


        int m = l.t.data.items.FindIndex(xc => xc == selectedResultOld);

        int count = 0;

        if(selectedResultTrashTag.Count > 0)
        {
            foreach(var xc in selectedResultTrashTag)
            {
                if(selectedResult.tags.Find(mm => mm == xc) != null)
                {
                    selectedResult.tags.Remove(xc);
                }

            }

        }
        foreach(var xc in selectedResultTrashTag)
        {
            if(s.selectedTags.Find(xd => xd.tagIndexNo == xc.tagIndexNo) != null)
            {
                count++;
            }
        }
        for(int i = 0;i < s.selectedTags.Count - count;i++)
        {
            foreach(var xcc in selectedResultTrashTag)
            {
                
                s.selectedTags.Remove(s.selectedTags.Find(mm => mm.tagIndexNo == xcc.tagIndexNo));
            }
           
        }
        selectedResult.tags.AddRange(s.selectedTags);

        l.t.data.items[m].tags = selectedResult.tags;
       
       
        l.t.data.items[m].name =  sResultNameInputF.text;
        l.t.data.items[m].desc =  sResultDescInputF.text;
        l.t.data.items[m].value = int.Parse(sResultValueInputF.text);
       
        sResultNameInputF.text = "";
        sResultDescInputF.text = "";
        sResultValueInputF.text = "";

        


       

        for(int i = 0;i < tagsScrollViewTrash.childCount;i++)
        {
            Destroy(tagsScrollViewTrash.GetChild(i).gameObject);
        }

        for(int i = 0;i < tagsScrollView.childCount;i++)
        {
            Destroy(tagsScrollView.GetChild(i).gameObject);
        }

        


        selectedResultTrashTag.Clear();
        s.selectedTags.Clear();

        selectedResult.name = "";

        s.inputName.text = "";
        searchButton();

      




    }


    public void RevertChanges()
    {
        resultChanged = true;

        


    }

    public void OpenTagScreen()
    {
        tagSearchScreenObj.SetActive(true);

    }

    public void CloseTagScreen()
    {

        if(tagsScrollView.childCount > 0)
        {
            for(int i = 0;i < tagsScrollView.childCount;i++)
            {
                Destroy(tagsScrollView.GetChild(i).gameObject);

            }

        }


        if(tagsScrollViewTrash.childCount > 0)
        {
            for(int i = 0;i < tagsScrollViewTrash.childCount;i++)
            {
                SearchUiTag item = tagsScrollViewTrash.GetChild(i).GetChild(1).GetComponent<SearchUiTag>();

                if(!s.selectedTags.Contains(item.thisTag))
                {
                    if(!selectedResult.tags.Contains(selectedResult.tags.Find(xcc => xcc.tagIndexNo == item.thisTag.tagIndexNo)))
                    {
                        Destroy(tagsScrollViewTrash.GetChild(i).gameObject);
                        selectedResultTrashTag.Remove(item.thisTag);
                    }

                }
            }
        }
       

        if(selectedResult.tags.Count > 0)
        {
            for(int i = 0;i < selectedResult.tags.Count;i++)
            {
                GameObject obj = Instantiate(tagPrefab, tagsScrollView);
                obj.name = selectedResult.tags[i].tagname;
                SearchUiTag objChild = obj.transform.GetChild(1).GetComponent<SearchUiTag>();
                objChild.thisTag = selectedResult.tags[i];
            }
        }
        
        if(s.selectedTags.Count > 0)
        {
            for(int i = 0;i < s.selectedTags.Count;i++)
            {
                GameObject obj = Instantiate(tagPrefab2, tagsScrollView);
                obj.name = s.selectedTags[i].tagname;
                SearchUiTag objChild = obj.transform.GetChild(1).GetComponent<SearchUiTag>();
                objChild.thisTag = s.selectedTags[i];



            }
        }
       
        tagSearchScreenObj.SetActive(false);
    }



    void Start()
    {
        switchSearchText.text = "Current search System: match any string";


        string alphabet1 = "abcdefghijklmnopqrstuvwxyz";
        string numbers1 = "1234567890";
        
        for(int i = 0;i < 26;i++)
        {
            alphabet.Add(alphabet1.ToCharArray(0, 26)[i]);

        }

        for(int i = 0;i < numbers1.Length;i++)
        {
            numbers.Add(numbers1.ToCharArray(0, numbers1.Length)[i]);

        }
        currentPage = 1;
        


        
    }

    public void onToggleClick()
    {
        if(length == 1 && switchSearchInt == 0)
        {
           
            searchButton();
        }
    }

    public void ClearItem()
    {
        selectedResult.name ="";
        selectedResult.desc = "";
        selectedResult.value = -1;
        sResultNameInputF.text = "";
        sResultDescInputF.text = "";
        sResultValueInputF.text = "";
        selectedResult.tags = null;
        selectedResultTrashTag.Clear();
        for(int i = 0; i < tagsScrollView.childCount; i++)
        {
            Destroy(tagsScrollView.GetChild(i).gameObject);
        }
    }

    public void ResortTagList()
    {

        for(int i = 0;i < tagsScrollView.childCount;i++)
        {
            Destroy(tagsScrollView.GetChild(i).gameObject);

        }

        int count = 0;

        
        foreach(var xc in selectedResultTrashTag)
        {
            if(s.selectedTags.Find(xd => xd.tagIndexNo == xc.tagIndexNo) != null)
            {
                count++;
            }
        }

        
        for(int i = 0;i < selectedResult.tags.Count - selectedResultTrashTag.Count - count;i++)
        {
            bool istrue = false;
         
            foreach(var m in selectedResultTrashTag)
            {
                if(selectedResult.tags[i] == m)
                {
                    istrue = true;
                }
            }

            

            

            if(!istrue)
            {
                GameObject obj = Instantiate(tagPrefab, tagsScrollView);
                obj.name = selectedResult.tags[i].tagname;
                SearchUiTag objChild = obj.transform.GetChild(1).GetComponent<SearchUiTag>();
                objChild.thisTag = selectedResult.tags[i];
            }
           
        }

        for(int i = 0; i < s.selectedTags.Count - count; i++)
        {
            bool istrue = false;

            foreach(var m in selectedResultTrashTag)
            {
                if(s.selectedTags[i].tagIndexNo == m.tagIndexNo)
                {
                    istrue = true;
                }
            }





            if(!istrue)
            {
                GameObject obj = Instantiate(tagPrefab2, tagsScrollView);
                obj.name = s.selectedTags[i].tagname;
                SearchUiTag objChild = obj.transform.GetChild(1).GetComponent<SearchUiTag>();
                objChild.thisTag = s.selectedTags[i];
            }
        }

        
        tagBacksToScrollView = false;
    }
    // public void shit()
    // {
    //   
    //     int zekeriya = 0;
    //     char[] charArrayLel = new char[5];
    //
    //     charArrayLel[0] = 'a';
    //     charArrayLel[1] = 'b';
    //     charArrayLel[2] = 'c';
    //     charArrayLel[3] = 'd';
    //     charArrayLel[4] = 'e';
    //
    //
    //         for(int i = 0;i < 100000;i++)
    //         {
    //             
    //             string randomname = alphabet[Random.Range(0, 25)].ToString() + alphabet[Random.Range(0, 25)] + alphabet[Random.Range(0, 25)] + alphabet[Random.Range(0, 25)] +
    //              alphabet[Random.Range(0, 25)] + "";
    //
    //             RandomNames.Add(randomname);
    //
    //         }
    //
    //         for(int i = 0;i < 100000;i++)
    //         {
    //             List<char> lol = new List<char>();
    //             foreach(char m in RandomNames[i])
    //             {
    //                 lol.Add(m);
    //
    //             }
    //
    //
    //             for(int meme = 0;meme < 5;meme++)
    //             {
    //                 if(meme == 0)
    //                 {
    //                     if(lol[meme] == charArrayLel[meme])
    //                     {
    //                         zekeriya++;
    //                     }
    //                 }
    //
    //                 if(zekeriya == meme)
    //                 {
    //                     if(lol[meme] == charArrayLel[meme])
    //                     {
    //                         zekeriya++;
    //                     }
    //                 }
    //
    //                 if(meme == 4)
    //                 {
    //                     if(zekeriya < 5)
    //                     {
    //                         zekeriya = 0;
    //                     }
    //                     else if(zekeriya == 5)
    //                     {
    //                         Debug.Log(RandomNames[i] + " " + zekeriya);
    //                         ceg++;
    //                         zekeriya = 0;
    //                     }
    //                 }
    //
    //
    //
    //                 //  string ayarverdim = "     ";
    //                 // 
    //                 //  if(meme == 4)
    //                 //  {
    //                 //      if(zekeriya != 0)
    //                 //      {
    //                 //         for(int ojo = 0; ojo < zekeriya; ojo++)
    //                 //          {
    //                 //              ayarverdim.ToCharArray(0, zekeriya)[ojo] = lol[ojo];
    //                 //          }
    //                 //      }
    //                 //
    //                 //      Debug.Log(RandomNames[i] + " " + ayarverdim);
    //                 //  }
    //             }
    //
    //         }
    //
    //
    //
    //
    //     RandomNames.Clear();
    //     momo++;
    //     
    // }


  
    

    public void searchWithTag()
    {
   
        if(oneOfThemTags.Count != 0)
        {
           for(int i = 0; i < oneOfThemTags.Count; i++)
            {
               
                
             
            }
        }

       
    }

    public void SwitchSearchSystem()
    {
        switch(switchSearchInt)
        {
            case 0: switchSearchInt = 1;
                switchSearchText.text = "Current search System: match the initials";
                searchButton();
                break;
            case 1: switchSearchInt = 0;
                switchSearchText.text = "Current search System: match any string";

                
                    searchButton();
                
               
                break;
        }
    }

    public searchEngineValueSettings sSettings;
    public void searchButton()
    {
        
            currentPage = 1;
            searchedItemsPercent.Clear();

       
        List<item> tempList = new List<item>();
        if(searchedTags.Count > 0)
        {
           
            for(int i = 0;i < l.t.data.items.Count;i++)
            {
                bool truefalse = true;
                foreach(var m in searchedTags)
                {
                    if(!l.t.data.items[i].tags.Contains(m))
                    {
                        truefalse = false;
                    }

                }

                if(truefalse)
                {
                    tempList.Add(l.t.data.items[i]);
                }
            }
            
        }


        if(sSettings.minimumValue == -1)
        {
            searchedItems.AddRange(l.t.data.items);
            
          
        }
        else
        {
            searchedItems.AddRange(l.t.data.items.FindAll(xc => xc.value >= sSettings.minimumValue && xc.value <= sSettings.maximumValue));

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
                    for(int i = 0;i < searchedItems.Count;i++)
                    {
                        if(searchedItems[i].name.Contains(inputName.text))
                        {
                            searchItem searchItem = new searchItem(searchedItems[i],  (Methods.percentC(length, searchedItems[i].name.Length)));
                            searchedItemsPercent.Add(searchItem);
                        }
                    }
                }
                else
                {
                    if(length >= 2)
                    {


                        for(int i = 0;i < searchedItems.Count;i++)
                        {
                            if(searchedItems[i].name.Contains(inputName.text))
                            {
                                searchItem searchItem = new searchItem(searchedItems[i],  (Methods.percentC(length, searchedItems[i].name.Length)));
                                searchedItemsPercent.Add(searchItem);
                            }
                        }

                    }
                    else if(length < 2)
                    {
                        for(int i = 0;i < searchedItems.Count;i++)
                        {
                            List<char> itemsChar = (from char xx in searchedItems[i].name select xx).ToList();
                            var percentCounter = 0;
                            List<int> numbers = new List<int>();
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
                                                if(((Methods.percentC(percentCounter, itemsChar.Count)) > 0))
                                                {
                                                    searchItem searchItem = new searchItem(searchedItems[i],  (Methods.percentC(percentCounter, itemsChar.Count)));
                                                    searchedItemsPercent.Add(searchItem);
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
                for(int i = 0;i < searchedItems.Count;i++)
                {
                    List<char> itemsChar = (from char xx in searchedItems[i].name select xx).ToList();
                    var percentCounter = 0;
                    List<int> numbers = new List<int>();
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
                                        if(((Methods.percentC(percentCounter, itemsChar.Count)) > 0))
                                        {
                                            searchItem searchItem = new searchItem(searchedItems[i],  (Methods.percentC(percentCounter, itemsChar.Count)));
                                            searchedItemsPercent.Add(searchItem);
                                        }
                                    }
                                }







                            }




                        }
                    }



                }
            }

          







            searchedItems.Clear();
            var comparer = new ComparerByItemPercent();



            searchedItemsPercent.Sort(comparer);
        

      
    
       
        
    }

    public void NextPage()
    {
        currentPage++;
    }

    public void PreviousPage()
    {
        currentPage--;
    }


    // Update is called once per frame
    void Update()
    {


        length = inputName.text.Length;



        if(length != 0)
        {
            

            isHappened = false;

            if(length == 1)
            {
                if(searchedItemsPercent.Count == 0)
                {
                    oldSearched = inputName.text + inputName.text.ToCharArray(0, length)[0];
                }
            }
            if(oldSearched != inputName.text)
            {
                for(int i = 0;i < 25;i++)
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
                     
                        searchedItemsPercent.Clear();
                        searchButton();
                        oldSearched = inputName.text;

                    }




                }

            }
            else
            {

            }



        }
        else
        {
            if(!isHappened)
            {
                for(int i = 0;i < 25;i++)
                {
                    charArray[i] = ' ';
                    if(i == 24)
                    {
                        isHappened = true;
                    }
                }
                searchedItemsPercent.Clear();
            }

        }

        double r = ((currentPage + 1) * 10) -10;
        double itemCountNext = searchedItemsPercent.Count / r;
      
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

        if(searchedItemsPercent.Count > 0)
        {
            CurrentPageText.text = currentPage + "";
            ResultCountText.text = searchedItemsPercent.Count + " " + "Result founded.";

        }
        else if(searchedItemsPercent.Count == 0 && length > 0)
        {
            CurrentPageText.text = ":(";
            ResultCountText.text = "Sadge.";
        }
        else if(length == 0)
        {
            CurrentPageText.text = "Hi.";
            ResultCountText.text = "Type something.";
        }







        if(selectedResult.name != "")
        {

            AutoResortToggle.interactable = true;
            AddNewTagButton.interactable = true;
            ClearItemButton.interactable = true;

            if(sResultNameInputF.text == selectedResult.name && sResultDescInputF.text == selectedResult.desc && 
                sResultValueInputF.text == selectedResult.value.ToString() && selectedResultTrashTag.Count == 0 && s.selectedTags.Count == 0)
            {
                RevertButton.interactable = false;
               
            }
           else if(sResultNameInputF.text != selectedResult.name || sResultDescInputF.text != selectedResult.desc ||
                     sResultValueInputF.text != selectedResult.value.ToString() ||selectedResultTrashTag.Count > 0 || s.selectedTags.Count > 0) 
            {
                RevertButton.interactable = true;
            }

            if(sResultNameInputF.text == selectedResult.name && sResultDescInputF.text == selectedResult.desc &&
                 sResultValueInputF.text == selectedResult.value.ToString() && selectedResultTrashTag.Count == 0 && s.selectedTags.Count == 0)
            {
                ApplyButton.interactable = false;
            }
            else if(sResultNameInputF.text != selectedResult.name || sResultDescInputF.text != selectedResult.desc ||
                     sResultValueInputF.text != selectedResult.value.ToString() ||selectedResultTrashTag.Count > 0 ||s.selectedTags.Count > 0)
            {
                if(sResultNameInputF.text.Length != 0 && sResultDescInputF.text.Length != 0 && sResultValueInputF.text.Length != 0)
                {
                    if(!sResultNameInputF.text.StartsWith(" ") && !sResultDescInputF.text.StartsWith(" ")&& !sResultValueInputF.text.StartsWith("-") && sResultValueInputF.text != "0")
                    {
                        ApplyButton.interactable = true;

                    }
                    else
                    {
                        ApplyButton.interactable = false;
                    }
                   
                }
                else
                {
                    ApplyButton.interactable = false;
                }
               
            }

        



                if(resultChanged)
                {

                    selectedResultTrashTag.Clear();
                    s.selectedTags.Clear();
                for(int i = 0; i < tagsScrollViewTrash.childCount; i++)
                {
                    Destroy(tagsScrollViewTrash.GetChild(i).gameObject);
                }
                    for(int i = 0; i < tagsScrollView.childCount; i++)
                    {
                        Destroy(tagsScrollView.GetChild(i).gameObject);
                            
                    }

                    for(int i = 0; i < s.selectedResultContent.childCount; i++)
                    {
                        Destroy(s.selectedResultContent.GetChild(i).gameObject);
                    }

                    for(int i = 0; i < selectedResult.tags.Count; i++)
                    {
                        GameObject obj = Instantiate(tagPrefab, tagsScrollView);
                        obj.name = selectedResult.tags[i].tagname;
                        SearchUiTag objChild = obj.transform.GetChild(1).GetComponent<SearchUiTag>();
                        objChild.thisTag = selectedResult.tags[i];
                    }

                


                    sResultNameInputF.interactable = true;
                   sResultDescInputF.interactable = true;
                   sResultValueInputF.interactable = true;
       
                   sResultNameInputF.text = selectedResult.name;
                   sResultDescInputF.text = selectedResult.desc;
                   sResultValueInputF.text = selectedResult.value.ToString();
    

                   if(selectedResult.tags.Count == 0)
                   {
                       sResultTagCountText.text = "There is no Tag";
                   }
                   else if(selectedResult.tags.Count == 1)
                   {
                       sResultTagCountText.text = "Tag Count:" + selectedResult.tags.Count.ToString();
                   }
                   else if(selectedResult.tags.Count > 1)
                   {
                       sResultTagCountText.text = "Tags Count:" + selectedResult.tags.Count.ToString();
                   }

                   resultChanged = false;


                }

                if(tagBacksToScrollView)
                {
                    resortTagListButton.interactable = true;
                    if(AutoResortToggle.isOn)
                    {
                        ResortTagList();
                    }
              
                   

                }
                else
                {
                    resortTagListButton.interactable = !true;
                }

                
            


        }
        else if(selectedResult.name == "")
        {
            AutoResortToggle.interactable = false;
            AddNewTagButton.interactable = false;
            sResultNameInputF.interactable = false;
            sResultDescInputF.interactable = false;
            sResultValueInputF.interactable = false;
            ApplyButton.interactable = false;
            RevertButton.interactable = false;
            ClearItemButton.interactable = false;
            resortTagListButton.interactable = false;
            sResultTagCountText.text = "There is no selected result.";
        }


   



    }

    

}

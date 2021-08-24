using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class uiResult : MonoBehaviour, IPointerClickHandler
{
    public searchEngine s;
    public Text SearchResultText;
    public Text MismatchResultText;
    public Transform parent;
    public searchItem result;
    public Image parentImage;


    [SerializeField] public int index;


    // Start is called before the first frame update
    void Start()
    {
        parent = gameObject.transform.parent.GetComponent<Transform>();
        SearchResultText = parent.GetChild(0).GetComponent<Text>();
        MismatchResultText = parent.GetChild(1).GetComponent<Text>();
        parentImage = parent.GetComponent<Image>();
        MismatchResultText.resizeTextForBestFit = false;
        SearchResultText.resizeTextForBestFit = false;
        SearchResultText.fontSize = 50;


        result = null;
    }

    // Update is called once per frame
    void Update()
    {
       
      
            if(s.currentPage == 1)
            { 
                if(s.searchedItemsPercent.Count > index)
                {
                    result = s.searchedItemsPercent[index]; 
                }
                else
                {
                    result = null;
                }
        
            }
            else if(s.currentPage != 1)
            {
                if(s.searchedItemsPercent.Count > (index  + (s.currentPage * 10)) - 10)
                {
                    result = s.searchedItemsPercent[(index  + ( s.currentPage * 10)) - 10];
                    // Ustanın elinde muştaa, bu şarkı iki puştaa. Tek cümlemlee kan kuscann', mezarına tüküren vuslat...
                    
                }
                else
                {
                    result = null;
                }
                


            } 



        if(result == null)
        {
            parentImage.color = Color.clear;
            SearchResultText.text = "";
            MismatchResultText.text = "";
        }
        else
        {
            SearchResultText.text = result.item.name;

            Methods.SpaceText(MismatchResultText, result.item.name, s.inputName.text);

            MismatchResultText.fontSize = SearchResultText.fontSize;
            parentImage.color = Color.white;
        }

     


    }

  

    public void OnPointerClick(PointerEventData eventData)
    {
       
        if(result != null)
        {
            
            s.selectedResult.name = result.item.name;
            s.selectedResult.desc = result.item.desc;
            s.selectedResult.value = result.item.value;
            s.selectedResult.tags = result.item.tags;
            s.selectedResultOld = result.item;

            s.resultChanged = true;
           
        }
    }
}

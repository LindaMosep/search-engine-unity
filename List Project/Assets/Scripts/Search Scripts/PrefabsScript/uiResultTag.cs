using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class uiResultTag : MonoBehaviour
{
    
    public searchEngineTag s;
    public searchTag result;
    public int index;
   
    public Text SearchResultText;
    public Text MismatchResultText;
    public Transform parent;
    public Image parentImage;

    public Toggle toggleSelected;



    // Start is called before the first frame update
    void Start()
    {
        parent = gameObject.transform.parent.GetComponent<Transform>();
        SearchResultText = parent.GetChild(0).GetComponent<Text>();
        MismatchResultText = parent.GetChild(1).GetComponent<Text>();
        toggleSelected = parent.GetChild(2).GetComponent<Toggle>();
        toggleSelected.isOn = false;
        parentImage = parent.GetComponent<Image>();
        MismatchResultText.resizeTextForBestFit = false;
        SearchResultText.resizeTextForBestFit = false;
        SearchResultText.fontSize = 50;
        
       
        
    }

   

    // Update is called once per frame
    void Update()
    {
       


        if(s.currentPage == 1)
        {
            if(s.searchedTagsPercent.Count > index)
            {
                result = s.searchedTagsPercent[index];
            }
            else
            {
                result = null;
            }

        }
        else if(s.currentPage != 1)
        {
            if(s.searchedTagsPercent.Count > (index  + (s.currentPage * 6)) - 6)
            {
                result = s.searchedTagsPercent[(index  + (s.currentPage * 6)) - 6];
                

            }
            else
            {
                result = null;
            }   



        }



        if(result == null)
        {
            toggleSelected.gameObject.SetActive(false);
            parentImage.color = Color.clear;
            SearchResultText.text = "";
            MismatchResultText.text = "";

        }
        else
        {
            toggleSelected.gameObject.SetActive(true);
            SearchResultText.text = result.tag.tagname;

            Methods.SpaceText(MismatchResultText, result.tag.tagname, s.inputName.text);
            parentImage.color = Color.clear;
            MismatchResultText.fontSize = SearchResultText.fontSize;
           
           


        }

    }

  

   
}

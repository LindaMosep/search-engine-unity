using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class SearchUiTag : MonoBehaviour,IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    public tag thisTag;
   public  Text tagInfoText;
   public  Transform parent;

   public Transform scrollViewTag;
   public  Transform scrollViewTrash;

   public  Image fillImage;
   public  float onPointerDownTime;
   public  bool isOnPointerDown;

   public  bool isClicked;
   public bool isDoubleClicked;
   public float isClickedTime;

    public GameObject tagInfoObj;
  

    public searchEngine s;
    

    public void OnPointerClick(PointerEventData eventData)
    {
       
      
        if(!isClicked)
        {
            
            isClicked = true;
        }
        else
        {

            isDoubleClicked = true;
            isClicked = false;
            isClickedTime = 0;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isOnPointerDown = true;  
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isOnPointerDown = false;
    }

    void Start()
    {
        parent = gameObject.transform.parent;
        s = GameObject.FindWithTag("SearchEngineTag").GetComponent<searchEngine>();
        tagInfoText = parent.GetChild(0).GetComponent<Text>();
        scrollViewTag = GameObject.FindWithTag("ContentNormalSearch").GetComponent<Transform>();
        scrollViewTrash = GameObject.FindWithTag("ContentTrashSearch").GetComponent<Transform>();

       

        
    
    }

    
    void Update()
    {
        tagInfoText.text = "Tag name: " + thisTag.tagname + "\n" + "Tag desc: " + thisTag.tagdesc + "\n" + "Tag's Tag Count: " + thisTag.tagEnum.Count;

        if(isOnPointerDown)
        {
            onPointerDownTime += Time.deltaTime;
        }
        else
        {
            onPointerDownTime = 0;
        }

        if(isClicked)
        {
            isClickedTime += Time.deltaTime;
        }


        if(isClickedTime > 0.3f)
        {
            isClicked = false;
            isClickedTime = 0;
        }

        if(isDoubleClicked)
        {
            if(parent.parent == scrollViewTag)
            {
                parent.SetParent(scrollViewTrash);
                s.selectedResultTrashTag.Add(thisTag);
                
            }
            else if(parent.parent == scrollViewTrash)
            {
                parent.SetParent(scrollViewTag);
                s.selectedResultTrashTag.Remove(thisTag);
                if(!s.tagBacksToScrollView)
                {
                    s.tagBacksToScrollView = true;
                }
            }


            isClicked = false;
            isDoubleClicked = false;

        }
    }
}

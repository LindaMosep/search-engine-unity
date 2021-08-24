using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;


public class uiTagResultsPrefab : MonoBehaviour, IPointerClickHandler
{
    public tag thisTag;
    bool isClicked;
    bool isDoubleClicked;
    float timer;
    public Transform parent;
    public Text infoText;
    public searchEngineTag s;

    public void Start()
    {
        parent = gameObject.transform.parent;
        infoText = parent.GetChild(0).GetComponent<Text>();
        s = GameObject.Find("SearchEngineTag").GetComponent<searchEngineTag>();
    }

    public void Update()
    {

        infoText.text = "Tag name: " + thisTag.tagname + "\n" + "Tag desc: " + thisTag.tagdesc + "\n" + "Tag's Tag Count:" + thisTag.tagEnum.Count;

        if(isClicked)
        {
            timer += Time.deltaTime;
        }else
        {
            timer = 0;
        }

        if(timer >= 0.3f)
        {
            isClicked = false;
        }

        if(isDoubleClicked)
        {
            
            s.selectedTags.Remove(thisTag);
            Destroy(parent.gameObject);
            int page = s.currentPage;
            s.searchButton();
            s.currentPage = page;
        }
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if(!isClicked)
        {
            isClicked = true;
        }
        else
        {
            isDoubleClicked = true;
            timer = 0;
        }
    }
}

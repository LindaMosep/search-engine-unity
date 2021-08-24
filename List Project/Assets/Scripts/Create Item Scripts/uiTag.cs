using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class uiTag : MonoBehaviour,IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    public tag Tag;

    public GameObject Parent;
    public lists l;
    public Image fillImage;
    
    
    public int number;
    bool isCliked;
    float timer;
    bool isDoubleClicked;
    public Transform contentSelected;
    public Transform contentList;
    public bool isOnPointerDown;
    public float onPointerDownTime;
    public Transform destroyList;
   
    // Start is called before the first frame update
    void Start()
    {

        l = GameObject.FindGameObjectWithTag("Lists").GetComponent<lists>();
        contentSelected = GameObject.FindGameObjectWithTag("ContentSelected").GetComponent<Transform>();
        contentList = GameObject.FindGameObjectWithTag("ContentList").GetComponent<Transform>();
        destroyList = GameObject.FindGameObjectWithTag("DestroySelected").GetComponent<Transform>();
    }

    public void Update()
    {

      //  for(int i = 0; i < l.t.data.tags.Count; i++)
      //  {
      //      if(l.t.data.tags[i] == Tag)
      //      {
      //          number++;
      //      }
      //
      //      if(i == l.t.data.tags.Count - 1)
      //      {
      //          if(number == 0)
      //          {
      //              
      //              Destroy(this.gameObject);
      //          }
      //          else
      //          {
      //              number = 0;
      //          }
      //      }
      //
      //      
      //  }
      //

        if(l.DestroyThis != null & l.DestroyThis == Tag)
        {
            Destroy(gameObject);
        }
      
        if(isCliked == true)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer -= timer;
        }

        if(timer > 0.3f)
        {
            isCliked = false;
        }
   

        if(isDoubleClicked == true)
        {

            if(Parent.transform.parent.tag != "DestroySelected")
            {
                if(Parent.transform.parent.tag == "ContentSelected")
                {
                    l.listedTags.Add(Tag);
                    Parent.transform.SetParent(contentList);
                }
                else
                {
                    l.listedTags.Remove(Tag);
                    Parent.transform.SetParent(contentSelected);
                }

              

            }
            

            

            
            Debug.Log("bla");
            isDoubleClicked = false;
        }

        if(isOnPointerDown)
        {
            onPointerDownTime += Time.deltaTime;   
        }
        else
        {
            onPointerDownTime = 0;
        }

        if(onPointerDownTime >= 1.5f)
        {
            if(Parent.transform.parent.tag != "DestroySelected")
            {
                if(Parent.transform.parent.tag == "ContentList")
                {
                    l.listedTags.Remove(Tag);
                    l.destroyListedTags.Add(Tag);
                    Parent.transform.SetParent(destroyList);
                }
                else
                {

                    l.destroyListedTags.Add(Tag);
                    Parent.transform.SetParent(destroyList);
                }
            }
            else
            {
                l.destroyListedTags.Remove(Tag);
                Parent.transform.SetParent(contentSelected);
            }

            isOnPointerDown = false;
        }

        if(onPointerDownTime > 0.3)
        {
            Methods.FillMethods((float)onPointerDownTime - 0.3f, 1.2f, fillImage);
        }
        else
        {
            fillImage.fillAmount = 0;
        }
       
       
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(isCliked == false)
        {
            isCliked = true;
        }
        else
        {
            isDoubleClicked = true;
            timer -= timer;
        }
       
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isOnPointerDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
       if(isOnPointerDown & onPointerDownTime < 1.5f)
        {
            isOnPointerDown = false;
        }
    }
}

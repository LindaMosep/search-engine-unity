using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class twoUiResultScript : MonoBehaviour, IPointerClickHandler
{
    public searchEngineValueSettings s;
    public searchTag result;
    public int index;

    public Text SearchResultText;
    public Text MismatchResultText;
    public Transform parent;
    public Image parentImage;



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



    }



    // Update is called once per frame
    void Update()
    {



     

    }

   

    public void OnPointerClick(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}

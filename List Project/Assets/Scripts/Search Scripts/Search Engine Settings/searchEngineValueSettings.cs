using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class searchEngineValueSettings : MonoBehaviour
{

    public lists l;
    public searchEngine searchE;

    public Transform searchEngineSettingsT;
    private Transform canvas;
    private Transform panel;
    




    public Slider minSlider;
    public Slider maxSlider;
    public Button ApplyValueChangesButton;
    public Button RevertValueChangesButton;
    public bool valueSettingsChanged;
    public Text minimumValueOldText;
    public Text maximumValueOldText;
    public Button settingsPanelOpenButton;

    bool isNotEmpty;
    public Text valueText;
    public InputField minInput;
    public InputField maxInput;
    public int minimumValue;
    public int maximumValue;
    int minimumValueTemp;
    int maximumValueTemp;

    List<int> maxMinList;


    void Start()
    {
        //switchSearchText.text = "Current search System: match any string";
        //
        maxMinList = l.t.data.items.Select(x => x.value).ToList();
        StartCoroutine(Get(2));
        canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Transform>();
        panel = GameObject.FindGameObjectWithTag("SearchPanel").GetComponent<Transform>();
        minimumValue = -1;
        maximumValue = -1;
        minimumValueTemp = -1;
        maximumValueTemp = -1;

        if(searchEngineSettingsT.GetSiblingIndex() == 0)
        {
            settingsPanelOpenButton.interactable = true;
        }
        else
        {
            settingsPanelOpenButton.interactable = false;
        }

        var tag = l.t.data.tags[3];
        int contains = 0;

       
        foreach(var m in l.t.data.items[0].tags)
        {
            if(m.tagname == tag.tagname && m.tagIndexNo == tag.tagIndexNo && m.tagdesc == tag.tagdesc)
            {
                Debug.Log("XD");
            }
        }
        Debug.Log(contains);


    }




    public void closeSearchSettings()
    {
        searchEngineSettingsT.SetParent(canvas);
        searchEngineSettingsT.SetSiblingIndex(0);
        settingsPanelOpenButton.interactable = true;
    }

    public void OpenSearchSettings()
    {
        searchEngineSettingsT.SetParent(panel);
        searchEngineSettingsT.SetSiblingIndex(10);
        settingsPanelOpenButton.interactable = false;
    }

 
    public void ApplyValueSettings()
    {
        valueSettingsChanged = false;
        minimumValue = minimumValueTemp;
        maximumValue = maximumValueTemp;
        if(isNotEmpty)
        {
            searchE.searchButton();
        }
       
    }

    public void RevertValueSettings()
    {
        minimumValueTemp = minimumValue;
        maximumValueTemp = maximumValue;
        minSlider.value = minimumValue;
        maxSlider.value = maximumValue;
        valueSettingsChanged = false;
    }

    // Update is called once per frame
    void Update()
    {
        
         minSlider.maxValue =  maxMinList.Max(); minSlider.minValue = maxMinList.Min(); maxSlider.maxValue = minSlider.maxValue;

        if(maxSlider.value <= minSlider.value)
        {
            maxSlider.value = minSlider.value;

        }

        if(minSlider.value != 0 || maxSlider.value != 0)
        {
            valueText.text = "≥" + " Value " + "≤";
            minInput.gameObject.SetActive(true);
            maxInput.gameObject.SetActive(true);
            minimumValueTemp = (int)minSlider.value;
            maximumValueTemp = (int)maxSlider.value;
          
       
            if(!minInput.isFocused)
            {
                minInput.text = minSlider.value.ToString();

            }
            else
            {
                if(minInput.text.ToString() != "")
                {
                    if(!minInput.text.Contains("-") || !minInput.text.Contains("-"))
                    {
                        if(int.Parse(minInput.text) <= minSlider.maxValue)
                        {
                            minSlider.value = int.Parse(minInput.text);
                        }
                        else
                        {
                            minInput.text = minSlider.maxValue.ToString();
                        }

                        if(int.Parse(minInput.text) >= minSlider.minValue)
                        {
                            minSlider.value = int.Parse(minInput.text);
                        }
                        else
                        {
                            minInput.text = minSlider.minValue.ToString();
                        }

                    }
                    else
                    {
                        minInput.text = 0.ToString();
                    }
                }




            }

            if(!maxInput.isFocused)
            {
                maxInput.text = maxSlider.value.ToString();
                if(int.Parse(maxInput.text) >= minSlider.value)
                {
                    maxSlider.value = int.Parse(maxInput.text);
                }
                else
                {
                    maxInput.text = minSlider.value.ToString();
                }

            }
            else
            {
                if(maxInput.text.ToString() != "")
                {
                    if(!maxInput.text.Contains("-") || !maxInput.text.Contains("-"))
                    {
                        if(int.Parse(maxInput.text) <= maxSlider.maxValue)
                        {
                            maxSlider.value = int.Parse(maxInput.text);
                        }
                        else
                        {
                            maxInput.text = maxSlider.maxValue.ToString();
                        }



                    }
                    else
                    {
                        maxInput.text = minSlider.value.ToString();
                    }
                }




            }
        }
        else
        {
            valueText.text = "Value search mode: dont include";
            minInput.gameObject.SetActive(false);
            maxInput.gameObject.SetActive(false);
            minimumValue = -1;
            maximumValue = -1;
        }
        FillMethods(maxSlider.value, minSlider.maxValue, fillImage);




        int m = 0;
        for(int i = 0; i < searchE.charArray.ToList().Count; i++)
        {
            if(searchE.charArray[i] != ' ')
            {
                m++;
            }
            //bayıkken de ayık bakar gözlerim
        }

        if(m != 0)
        {
            isNotEmpty = true;
        }
        else
        {
            isNotEmpty = false;
        }
        
        if(minimumValueTemp == minimumValue && maximumValueTemp == maximumValue)
        {
            valueSettingsChanged = false;
        }


        if(minimumValueTemp != minimumValue ||maximumValueTemp != maximumValue)
        {
            valueSettingsChanged = true;
        }





        Color xd = new Color();
        if(valueSettingsChanged)
        {
            ApplyValueChangesButton.interactable = true;
            RevertValueChangesButton.interactable = true;
   

        }
        else
        {
            ApplyValueChangesButton.interactable = false;
            RevertValueChangesButton.interactable = false;

           
           
        }
        if(minimumValue == minimumValueTemp)
        {
            minimumValueOldText.color = Color.black;
        }
        else
        {
            if(ColorUtility.TryParseHtmlString("#810000", out xd))
            { minimumValueOldText.color = xd; }
        }

        if(maximumValue == maximumValueTemp)
        {
            maximumValueOldText.color = Color.black;
        }
        else
        {
            if(ColorUtility.TryParseHtmlString("#810000", out xd))
            { maximumValueOldText.color = xd; }
        }




        if(minimumValue != -1)
        {
            minimumValueOldText.text = minimumValue.ToString();
            maximumValueOldText.text = maximumValue.ToString();
        }
        else
        {
            minimumValueOldText.text = "Exclude";
            maximumValueOldText.text = "Search";
        }
       

        
       
    }
  

    public static void FillMethods(float coin, float limit, Image x)
    {
        if(coin / limit < 0.01)
            x.fillAmount = 0;
        else if(coin / limit > limit)
            x.fillAmount = 1;
        x.fillAmount = (float)(coin / limit);

    }

    public Image fillImage;

    public IEnumerator Get(float waitTime)
    {
        while(true)
        {
            yield return new WaitForSeconds(waitTime);

            if(searchEngineSettingsT.parent == panel)
            {
                maxMinList = l.t.data.items.Select(x => x.value).ToList();
               
            }
            
            
        }
            
        
       

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Tooltip : MonoBehaviour
{
    public Text TooltipText;
    public TMP_Text TooltipTextTMP;
    public GameObject tooltipObj;

    public void LetsTooltip(item item)
    {
        string itemdesc = "Desc "+ item.desc;
        string itemvalue = "Value " +item.value;
        string itemindex = "Index " +item.indexNo;


        string tooltip = itemdesc + "\n" + itemvalue + "\n" + itemindex;
        TooltipText.text = tooltip;
        TooltipTextTMP.text = tooltip;
        tooltipObj.SetActive(true);
    }
}

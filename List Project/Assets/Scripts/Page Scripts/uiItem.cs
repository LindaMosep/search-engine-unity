using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class uiItem : MonoBehaviour, IPointerClickHandler
{
    public item item;
    public Text NameText;
    public TMP_Text NameTextTMP;
    public Image itemImage;
    public Image RedImage;
    public Sprite NoImage;
    [SerializeField]  int ItemNumber;
    public lists l;
   





    public void Update()
    {
        if(l.currentPage != 1)
        {
            if(l.sortedItems.Find(item => item.indexNo == (((l.currentPage * 8) + ItemNumber) - 1) - 8) != null)
            {
                item = l.sortedItems.Find(item => item.indexNo == (((l.currentPage * 8) + ItemNumber) - 1 - 8));
            }
            else
            {
                item = null;
            }
            
        }
        else
        {
            item = l.sortedItems.Find(item => item.indexNo == ItemNumber - 1);
        }
       

        if(item != null)
        {
            NameText.text = item.name;

            itemImage.color = Color.white;
            if(item.ItemSprite != null)
            {
                itemImage.sprite = item.ItemSprite;
            }
            else
            {
                itemImage.sprite = NoImage;
            }
            RedImage.color = Color.red;
        }
        else
        {
            NameText.text = "";
            RedImage.color = Color.clear;
            itemImage.color = Color.clear;
        }



   
        NameTextTMP.text = NameText.text;
    }



    public void OnPointerClick(PointerEventData eventData)
    {
        
        if(item != null)
        {
           
            l.tooltip.LetsTooltip(item);
        }
        else
        {
            
            l.tooltip.tooltipObj.SetActive(false);
        }

    }
}

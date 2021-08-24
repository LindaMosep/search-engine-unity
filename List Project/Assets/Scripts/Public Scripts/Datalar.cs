using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using BreakInfinity;
using static BreakInfinity.BigDouble;
using System;


[Serializable]
public class Datalar
{

    public List<tag> tags = new List<tag>();
    public List<item> items = new List<item>();
    public List<tagEnum> tagEnums = new List<tagEnum>();


    public int test;
    public void FullReset()
    {
        tags.Clear();
        items.Clear();
        tagEnums.Clear();



    }
   
   
   
   
   
}

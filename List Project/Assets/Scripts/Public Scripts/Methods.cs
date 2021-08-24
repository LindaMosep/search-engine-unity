using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BreakInfinity;
using static BreakInfinity.BigDouble;

public class Methods : MonoBehaviour
{
    public static void FillMethods(BigDouble start, BigDouble limit, Image x)
    {
        if(start / limit < 0.01)
            x.fillAmount = 0;
        else if(start / limit > limit)
            x.fillAmount = 1;
        x.fillAmount = (float)(start / limit).ToDouble();

    }

    public static int percentC(int percent, int charactersLength)
    {
        return (percent * 100) / charactersLength;
    }

    public static void SpaceText(Text t, string indexof, string input)
    {

        switch(indexof.IndexOf(input, 0, indexof.Length))
        {
            case 0:
                t.text = "" + input;
                break;
            case 1:
                t.text = " " + input;
                break;
            case 2:
                t.text = "  " + input;
                break;
            case 3:
                t.text = "   " + input;
                break;
            case 4:
                t.text = "    " +input;
                break;
            case 5:
                t.text = "     " + input;
                break;
            case 6:
                t.text = "      " + input;
                break;
            case 7:
                t.text = "       " + input;
                break;
            case 8:
                t.text = "        " + input;
                break;
            case 9:
                t.text = "         " + input;
                break;
            case 10:
                t.text = "          " + input;
                break;
            case 11:
                t.text = "           " + input;
                break;
            case 12:
                t.text = "            " + input;
                break;
            case 13:
                t.text = "             " + input;
                break;
            case 14:
                t.text = "              " + input;
                break;
            case 15:
                t.text = "               " + input;
                break;
            case 16:
                t.text = "                " + input;
                break;
            case 17:
                t.text = "                 " + input;
                break;
            case 18:
                t.text = "                  " + input;
                break;
            case 19:
                t.text = "                   " + input;
                break;
            case 20:
                t.text = "                    " + input;
                break;





        }


    }


}

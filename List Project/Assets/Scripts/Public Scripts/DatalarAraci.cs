using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DatalarAraci : MonoBehaviour
{

    public Datalar data;
    public float time;
    public bool isPaused;

    public void Awake()
    {

     

        SaveSystem.LoadPlayer(ref data);
  
       
    }

   
    public void Start()
    {
       
    }


    // Update is called once per frame
   public void LateUpdate()
    {

        time += Time.deltaTime;

        if(time >= 60)
        {
            SaveSystem.SavePlayer(data);
            time = 0;
        }
       
       

    }

    public void OnApplicationQuit()
    {
        SaveSystem.SavePlayer(data);
    }

    public void OnApplicationPause(bool pause)
    {
        isPaused = pause;

    }

    void OnApplicationFocus(bool hasFocus)
    {
        isPaused = !hasFocus;
    }

}

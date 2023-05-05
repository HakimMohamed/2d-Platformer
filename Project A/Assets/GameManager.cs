using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GameManager : MonoBehaviour
{
   

    public static event EventHandler OnSpacePressed;
    public static event EventHandler OnSleeping;
    private void Awake()
    {

    }

   
    void Start()
    {
        OnSleeping?.Invoke(this, EventArgs.Empty);
    }

   

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnSpacePressed?.Invoke(this, EventArgs.Empty);
        }

        

    }

   
    
    
}


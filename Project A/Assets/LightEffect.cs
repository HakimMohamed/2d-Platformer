using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LightEffect : MonoBehaviour
{
    
    private void Start()
    {
        CheckPointSave.OnPlayerSaved += CheckPointSave_OnPlayerSaved;
    }

    private void CheckPointSave_OnPlayerSaved(object sender, System.EventArgs e)
    {
        GetComponent<Animator>().SetTrigger("AuraEffect");
    }
}

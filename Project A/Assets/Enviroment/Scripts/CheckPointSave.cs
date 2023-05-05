using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CheckPointSave : MonoBehaviour
{
    Transform Player;
    bool isSaved = false;
    Animator anim;
    [SerializeField]GameObject SaveLight;
    public static event EventHandler OnPlayerSaved;
    void Start()
    {
        Player = GameObject.Find("Player").transform;
        anim = GetComponent<Animator>();
        OnPlayerSaved += LightEffectOnPlayerSaved;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < Player.position.x&&!isSaved)
        {
            isSaved = true;
            anim.SetTrigger("Saved");
            OnPlayerSaved?.Invoke(this,EventArgs.Empty);
        }
    }
    private void LightEffectOnPlayerSaved(object sender, EventArgs e)
    {
        SaveLight.SetActive(isSaved);
        OnPlayerSaved -= LightEffectOnPlayerSaved;

    }
}

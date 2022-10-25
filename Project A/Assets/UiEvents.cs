using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiEvents : MonoBehaviour
{
    [SerializeField] GameObject SleepPanel;
    [SerializeField] GameObject SpaceBar;
    [SerializeField] GameObject Bones;

    void Start()
    {
        GameManager.OnSpacePressed += GameManager_OnSpacePressed;
        GameManager.OnSleeping += GameManager_OnSleeping;
        xpBones.OnnewBone += XpBones_OnnewBone;
    }

    private void XpBones_OnnewBone(object sender, System.EventArgs e)
    {
        Bones.GetComponent<Animator>().SetTrigger("newBone");
    }

    private void GameManager_OnSleeping(object sender, System.EventArgs e)
    {
        SpaceBar.SetActive(false);
        SleepPanel.SetActive(true);
        EnableSpaceBar();
        Debug.Log("Sleep");
    }

    private void GameManager_OnSpacePressed(object sender, System.EventArgs e)
    {
        SleepPanel.GetComponent<Animator>().SetTrigger("GetUp");
        SpaceBar.SetActive(false);
        
        SpaceBar.GetComponent<UiDestroySelf>().DestroyUi();
        Debug.Log("SpacePressed");
        GameManager.OnSpacePressed -= GameManager_OnSpacePressed;

    }

    private void EnableSpaceBar()
    {
        SpaceBar.SetActive(true);
    }
}

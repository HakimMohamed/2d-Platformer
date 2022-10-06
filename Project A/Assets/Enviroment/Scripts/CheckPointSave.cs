using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointSave : MonoBehaviour
{
    [SerializeField]private int CheckPoint_number = 0;
    Transform Player;
    bool isSaved = false;
    Animator anim;
    [SerializeField]GameObject SaveLight;
    [SerializeField] GameObject LightPanel;
    //[SerializeField] GameObject Saved_UI;
    void Start()
    {
        Player = GameObject.Find("Player").transform;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < Player.position.x&&!isSaved)
        {
            isSaved = true;
            anim.SetTrigger("Saved");
            //Saved_UI.SetActive(true);
        }
        SaveLight.SetActive(isSaved);
    }
    public void LightEffect()
    {
        LightPanel.GetComponent<Animator>().SetTrigger("AuraEffect");

    }
}

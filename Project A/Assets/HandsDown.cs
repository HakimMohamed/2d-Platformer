using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsDown : MonoBehaviour
{
    [Header("Components")]
    private Animator anim;

    [SerializeField]private GameObject Handvfx;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Handvfx.SetActive(true);
            
        }
    }

    public void DisableHand()
    {
        Handvfx.SetActive(false);

    }
}

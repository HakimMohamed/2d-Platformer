using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pressKey : MonoBehaviour
{
    Animator anim;
    [SerializeField] private KeyCode keycode;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool keypress = Input.GetKey(keycode);
        
        anim.SetBool("keypress", keypress);
        

    }
}

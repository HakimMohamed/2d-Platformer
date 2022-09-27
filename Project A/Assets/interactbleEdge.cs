using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactbleEdge : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (Input.GetKey(KeyCode.S))
        {
            GetComponent<BoxCollider2D>().enabled = false;
            Debug.Log("???");
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        GetComponent<Collider2D>().enabled = true;


    }
}

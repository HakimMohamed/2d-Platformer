using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{

    float Enemy_Health = 0;
    public Image img;
    
    void Start()
    {
        Enemy_Health = 100f;
        img.fillAmount = 1f;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Enemy_Health -= 10f;
            img.fillAmount -= 0.1f;
        }

    }


}

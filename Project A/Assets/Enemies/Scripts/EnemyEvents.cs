using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyEvents : MonoBehaviour
{


    MeeleEnemy meeleEnemy;
    
    void Start()
    {
        meeleEnemy = GetComponent<MeeleEnemy>();
        meeleEnemy.OnEnemyDamaged += MeeleEnemy_OnEnemyDamaged;
    }

    private void MeeleEnemy_OnEnemyDamaged(object sender, EventArgs e)
    {
        Debug.Log("EnemyDamaged"+ meeleEnemy.Health);
    }

    // Update is called once per frame
    void Update()
    {



    }




}

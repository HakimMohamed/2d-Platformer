using System;
using System.Collections;
using UnityEngine;


public static class Extensions
{
        
        
    public static IEnumerator addForceToPlayer(Rigidbody2D rb,int dir,float dashPower)
    {
        Playermovement.isUsingVelocity = true;
        rb.velocity = new Vector2(dir * dashPower, 0);
        yield return new WaitForSeconds(.5f);
        Playermovement.isUsingVelocity = false;

    }
    
}



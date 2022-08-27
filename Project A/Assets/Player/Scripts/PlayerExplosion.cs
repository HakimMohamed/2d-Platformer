using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExplosion : MonoBehaviour
{
    private Transform Explosion;
    void Start()
    {
        Explosion = GameAssets.instance.ExplosionVFX;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Instantiate(Explosion, transform.position, Quaternion.identity);
        }
    }
}

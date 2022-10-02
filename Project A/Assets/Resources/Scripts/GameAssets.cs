using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets _instace;
    public static GameAssets instance {
        get {
            if (_instace == null) _instace = (Instantiate(Resources.Load("GameAssets")) as GameObject).GetComponent<GameAssets>();
            return _instace;
            }
        
    }

    public Transform HandsDown;
    public Transform DarkGate;
    public Transform Onigiri;

    [Header("Enemy")]
    public Transform XpBones;
    public Transform DeathBone;
    public Transform enemy_Bullet;
    public Transform enemy_bulletExplosion1;
    public Transform enemy_bulletExplosion2;
    public Transform BloodVFX;
    public Transform ThunderHit;

}

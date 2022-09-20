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

    public Transform BloodVFX;
    public Transform ExplosionVFX;
    public Transform HandsDown;
    public Transform DarkGate;
    public Transform Onigiri;
    
}

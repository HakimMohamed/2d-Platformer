using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlash : MonoBehaviour
{
    [SerializeField] GameObject verSlash;
    [SerializeField] GameObject horSlash;
    [SerializeField] GameObject DownSlash;

    public void verSlashController(int isEnabled)
    {
        verSlash.SetActive(true? isEnabled == 1:isEnabled==-1);
    }
    
    public void HorSlashController(int isEnabled)
    {
        horSlash.SetActive(true ? isEnabled == 1 : isEnabled == -1);
    }
    public void DownSlashController(int isEnabled)
    {
        DownSlash.SetActive(true ? isEnabled == 1 : isEnabled == -1);

    }
}

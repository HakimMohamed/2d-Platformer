using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerExp : MonoBehaviour
{
    public int numOfbones;
    SpriteRenderer sp;
    [SerializeField] Color expColor;
    [SerializeField] TextMeshProUGUI xpBonenumberOfBones;

    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        xpBonenumberOfBones.text = numOfbones.ToString();
    }
    public IEnumerator changePlayersColor()
    {
        sp.color = expColor;
        yield return new WaitForSeconds(1f);
        sp.color = Color.white;
    }
    
}

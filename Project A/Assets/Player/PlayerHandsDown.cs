using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandsDown : MonoBehaviour
{
    [Header("Components")]
    private Animator anim;

    private Transform Handvfx;
    private CrosshairCursor cursorPos;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        Handvfx = GameAssets.instance.HandsDown;
        cursorPos = GameObject.Find("HandsDownCrosshair").GetComponent<CrosshairCursor>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
           Transform handvfx =  Instantiate(Handvfx, cursorPos.mouseCursorPos, Quaternion.identity);
            handvfx.transform.localScale = transform.localScale;
            Transform attackpos = handvfx.Find("HandsDownattackPos").transform;
            //attackpos.position = new Vector2(attackpos.position.x+ 2.29f, attackpos.position.y + -0.86f);

        }
    }

    public void DisableHand()
    {
        Destroy(Handvfx);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandsDown : MonoBehaviour
{
    [Header("Components")]
    private Animator anim;

    private Transform Handvfx;
    private Transform DarkGate;
    private CrosshairCursor cursorPos;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        Handvfx = GameAssets.instance.HandsDown;
        cursorPos = GameObject.Find("HandsDownCrosshair").GetComponent<CrosshairCursor>();
        DarkGate = GameAssets.instance.DarkGate;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Transform darkgate = Instantiate(DarkGate, cursorPos.mouseCursorPos, Quaternion.identity);
            Invoke("SpawnHand", 1f);
            darkgate.transform.localScale = transform.localScale;


        }
    }
   public void SpawnHand()
    {
        Transform handvfx = Instantiate(Handvfx, new Vector2(cursorPos.mouseCursorPos.x + 3.54f, cursorPos.mouseCursorPos.y + 0.39f), Quaternion.identity);

        handvfx.transform.localScale = transform.localScale;
    }
    public void DisableHand()
    {
        Destroy(Handvfx);
    }
}

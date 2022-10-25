using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairCursor : MonoBehaviour
{
    public Vector2 mouseCursorPos;
    private void Awake()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        mouseCursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float cursorMaxRange = mouseCursorPos.y;
        cursorMaxRange = Mathf.Clamp(cursorMaxRange, -2.8f, -2.8f);

        mouseCursorPos = new Vector2(mouseCursorPos.x, cursorMaxRange);
        transform.position = mouseCursorPos;
    }
}

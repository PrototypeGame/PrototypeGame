﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum MouseState
{
    MOVE, ATTACK, ITEM
}

public class MouseCursor : MonoBehaviour
{
    public Texture2D cursor;
    public Texture2D[] cursorSprites;

    public bool fixPointIsCenter;
    public Vector2 fixPointAdjust;

    private Vector2 hotspot;

    private void Awake()
    {
        StartCoroutine("SetCursor", cursorSprites[(int)MouseState.MOVE]);
    }

    private void Update()
    {
        CheckCursorPoint();
    }

    private IEnumerator SetCursor(Texture2D setCur)
    {
        cursor = setCur;

        yield return new WaitForEndOfFrame();

        if (fixPointIsCenter)
        {
            hotspot.x = cursor.width / 2;
            hotspot.y = cursor.height / 2;
        }
        else
        {
            hotspot = fixPointAdjust;
        }

        Cursor.SetCursor(cursor, hotspot, CursorMode.Auto);

        yield break;
    }

    private void CheckCursorPoint()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1000))
        {
            Collider col = hit.collider;

            if (col.CompareTag("Floor"))
            {
                StartCoroutine("SetCursor", cursorSprites[(int)MouseState.MOVE]);
            }
            else if (col.CompareTag("Enemy"))
            {
                StartCoroutine("SetCursor", cursorSprites[(int)MouseState.ATTACK]);
            }
            else if (col.CompareTag("Item"))
            {
                StartCoroutine("SetCursor", cursorSprites[(int)MouseState.ITEM]);
            }
        }
    }
}
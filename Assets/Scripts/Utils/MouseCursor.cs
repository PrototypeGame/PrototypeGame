using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum MouseState
{

}

public class MouseCursor : MonoBehaviour
{
    public Texture2D cursor;

    public bool fixPointIsCenter;
    public Vector2 fixPointAdjust;

    private Vector2 hotspot;

    private void Awake()
    {
        StartCoroutine("SetCursor");
    }

    private IEnumerator SetCursor()
    {
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
}

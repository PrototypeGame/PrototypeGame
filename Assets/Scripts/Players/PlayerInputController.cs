using UnityEngine;
using System.Collections;

public class PlayerInputController
{
    public static bool CheckInputSignal(KeyCode[] inputKeys)
    {
        foreach (var item in inputKeys)
        {
            if (Input.GetKey(item))
                return true;
        }
        return false;
    }

    public static float HorizontalInputValue()
    {
        return Input.GetAxisRaw("Horizontal");
    }

    public static float VerticalInputValue()
    {
        return Input.GetAxisRaw("Vertical");
    }
}

using UnityEngine;
using System.Collections;

public class PlayerInputController
{
    public static bool CheckInputSignal(GameKeyPreset[] inputKeys)
    {
        foreach (var item in inputKeys)
        {
            if (GameKey.GetKey(item))
                return true;
        }
        return false;
    }

    public static float HorizontalInputValue()
    {
        if (!GameKey.GetKey(GameKeyPreset.LeftArrow) && GameKey.GetKey(GameKeyPreset.RightArrow))
        {
            return 1.0f;
        }
        if (GameKey.GetKey(GameKeyPreset.LeftArrow) && GameKey.GetKey(GameKeyPreset.RightArrow))
        {
            return 0.0f;
        }
        if (GameKey.GetKey(GameKeyPreset.LeftArrow) && !GameKey.GetKey(GameKeyPreset.RightArrow))
        {
            return -1.0f;
        }

        return 0.0f;
    }

    public static float VerticalInputValue()
    {
        if (!GameKey.GetKey(GameKeyPreset.DownArrow) && GameKey.GetKey(GameKeyPreset.UpArrow))
        {
            return 1.0f;
        }
        if (GameKey.GetKey(GameKeyPreset.DownArrow) && GameKey.GetKey(GameKeyPreset.UpArrow))
        {
            return 0.0f;
        }
        if (GameKey.GetKey(GameKeyPreset.DownArrow) && !GameKey.GetKey(GameKeyPreset.UpArrow))
        {
            return -1.0f;
        }

        return 0.0f;
    }
}

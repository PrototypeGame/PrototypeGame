using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyData
{
    public KeyCode initKey;
    public KeyCode customKey;

    public KeyData(KeyCode key)
    {
        this.initKey = key;
        this.customKey = key;
    }
}

public enum GameKeyPreset
{
    LeftArrow, RightArrow, UpArrow, DownArrow, Dash,
    NormalAttack, Skill_1, Skill_2, Skill_3, Skill_Ultimate
}

public class GameKey : MonoBehaviour
{
    private static Dictionary<GameKeyPreset, KeyData> GameKeys;

    GameKey()
    {
        GameKeys = new Dictionary<GameKeyPreset, KeyData>();

        InitKeySetting();
    }

    private void InitKeySetting()
    {
        GameKeys[GameKeyPreset.LeftArrow] = new KeyData(KeyCode.LeftArrow);
        GameKeys[GameKeyPreset.RightArrow] = new KeyData(KeyCode.RightArrow);
        GameKeys[GameKeyPreset.DownArrow] = new KeyData(KeyCode.DownArrow);
        GameKeys[GameKeyPreset.UpArrow] = new KeyData(KeyCode.UpArrow);

        GameKeys[GameKeyPreset.Dash] = new KeyData(KeyCode.Space);

        GameKeys[GameKeyPreset.NormalAttack] = new KeyData(KeyCode.A);

        GameKeys[GameKeyPreset.Skill_1] = new KeyData(KeyCode.Q);
        GameKeys[GameKeyPreset.Skill_2] = new KeyData(KeyCode.W);
        GameKeys[GameKeyPreset.Skill_3] = new KeyData(KeyCode.E);
        GameKeys[GameKeyPreset.Skill_Ultimate] = new KeyData(KeyCode.R);
    }

    public static bool GetKeyDown(GameKeyPreset key)
    {
        return Input.GetKeyDown(GameKeys[key].customKey);
    }

    public static bool GetKeyUp(GameKeyPreset key)
    {
        return Input.GetKeyUp(GameKeys[key].customKey);
    }

    public static bool GetKey(GameKeyPreset key)
    {
        return Input.GetKey(GameKeys[key].customKey);
    }

    public void ChangeCustomKey(GameKeyPreset targetKey, KeyCode changeKey)
    {
        GameKeys[targetKey].customKey = changeKey;
    }

    public void ResetToInitKey(GameKeyPreset targetKey)
    {
        GameKeys[targetKey].customKey = GameKeys[targetKey].initKey;
    }
}

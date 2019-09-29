﻿using System.Collections;
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
    NONE, 
    LeftArrow, RightArrow, UpArrow, DownArrow, Dash,
    NormalAttack, Skill_1, Skill_2, Skill_3, Skill_Ultimate, 
    ITEM_1, ITEM_2, ITEM_3, ITEM_4, ITEM_5, ITEM_6
}

public class GameKey : MonoBehaviour
{
    public static Dictionary<GameKeyPreset, KeyData> GameKeys;

    private void Awake()
    {
        GameKeys = new Dictionary<GameKeyPreset, KeyData>();

        InitKeySetting();
    }

    private void InitKeySetting()
    {
        GameKeys[GameKeyPreset.NONE] = new KeyData(KeyCode.None);

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

        GameKeys[GameKeyPreset.ITEM_1] = new KeyData(KeyCode.Alpha1);
        GameKeys[GameKeyPreset.ITEM_2] = new KeyData(KeyCode.Alpha2);
        GameKeys[GameKeyPreset.ITEM_3] = new KeyData(KeyCode.Alpha3);
        GameKeys[GameKeyPreset.ITEM_4] = new KeyData(KeyCode.Alpha4);
        GameKeys[GameKeyPreset.ITEM_5] = new KeyData(KeyCode.Alpha5);
        GameKeys[GameKeyPreset.ITEM_6] = new KeyData(KeyCode.Alpha6);
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
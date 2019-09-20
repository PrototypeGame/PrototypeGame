using UnityEngine;
using System;
using System.Collections;

public enum SetStat
{
    PLUS, MINUS, SET
}

[Serializable]
public class PlayerData
{
    [Header("Movement Status")]
    public float moveSpeed;

    [Header("Basic Status")]
    public float strength;
    public float dexterity;
    public float intellect;
    
    [Header("Variable Status")]
    public float health;
    //public float healthRecycle;
    [Space(10)]
    public float magicPoint;
    //public float magicPointRecycle;
    [Space(10)]
    public float attackMinPower;
    public float attackMaxPower;
    public float criticalDamage;
    public float attackSpeed;
    [Space(10)]
    public float armor;
    [Space(10)]
    public float avoidRate;

    public PlayerData SetStrength(float setPoint, SetStat setter)
    {
        switch (setter)
        {
            case SetStat.PLUS:
                strength += setPoint;
                break;
            case SetStat.MINUS:
                strength -= setPoint;
                break;
            case SetStat.SET:
                strength = setPoint;
                break;
            default:
                break;
        }
        return this;
    }
    public PlayerData SetDexterity(float setPoint, SetStat setter)
    {
        switch (setter)
        {
            case SetStat.PLUS:
                dexterity += setPoint;
                break;
            case SetStat.MINUS:
                dexterity -= setPoint;
                break;
            case SetStat.SET:
                dexterity = setPoint;
                break;
            default:
                break;
        }
        return this;
    }
    public PlayerData SetIntellect(float setPoint, SetStat setter)
    {
        switch (setter)
        {
            case SetStat.PLUS:
                intellect += setPoint;
                break;
            case SetStat.MINUS:
                intellect -= setPoint;
                break;
            case SetStat.SET:
                intellect = setPoint;
                break;
            default:
                break;
        }
        return this;
    }

    //public float SetHealth()
    //{
    //    health = 100 + (25 * strength) + 
    //}
}

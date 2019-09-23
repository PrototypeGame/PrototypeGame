using UnityEngine;
using Random = UnityEngine.Random;
using System;
using System.Collections;

[Serializable]
public class PlayerData : StatusBase
{
    public StatusBase skillStatFix;
    public StatusBase itemStatFix;

    public PlayerData SetStrength(float setPoint)
    {
        Strength = setPoint;
        return this;
    }
    public PlayerData AddStrength(float addPoint)
    {
        Strength += addPoint;
        return this;
    }
    public PlayerData SetDexterity(float setPoint)
    {
        Dexterity = setPoint;
        return this;
    }
    public PlayerData AddDexterity(float addPoint)
    {
        Dexterity += addPoint;
        return this;
    }
    public PlayerData SetIntellect(float setPoint)
    {
        Intellect = setPoint;
        return this;
    }
    public PlayerData AddIntellect(float addPoint)
    {
        Intellect += addPoint;
        return this;
    }

    public PlayerData SetMoveSpeed()
    {
        MoveSpeed = (BasicMoveSpeed * (1.0f + skillStatFix.MoveSpeed));
        return this;
    }

    public PlayerData SetHealth()
    {
        Health = (100 + (25 * Strength) + skillStatFix.Health) * (1.0f + (skillStatFix.Health / 100.0f));
        return this;
    }

    public PlayerData SetMagicPoint()
    {
        MagicPoint = ((15.0f * Intellect) + skillStatFix.MagicPoint) * (1.0f + (skillStatFix.MagicPoint / 100.0f));
        return this;
    }

    public PlayerData SetAttackMinPower()
    {
        AttackMinPower = ((BasicAttackPower * 1.35f) + skillStatFix.AttackMinPower) * (1.0f + (skillStatFix.AttackMinPower / 100.0f));
        return this;
    }

    public PlayerData SetAttackMaxPower()
    {
        AttackMaxPower = ((BasicAttackPower * 2.25f) + skillStatFix.AttackMaxPower) * (1.0f + (skillStatFix.AttackMaxPower / 100.0f));
        return this;
    }

    public PlayerData SetFinalAttackDamage()
    {
        FinalAttackDamage = Random.Range(AttackMinPower, AttackMaxPower);
        return this;
    }

    public PlayerData SetAttackSpeed()
    {
        AttackSpeed = (BasicAttackSpeed / (1.0f + (0.02f * Dexterity) + skillStatFix.AttackSpeed));
        return this;
    }

    public PlayerData SetCriticalDamage()
    {
        CriticalDamage = (FinalAttackDamage * Random.Range(1.5f, 2.0f));
        return this;
    }

    public PlayerData SetArmor()
    {
        Armor = (-2.0f + (0.3f * Dexterity) + skillStatFix.Armor) * (1.0f + (skillStatFix.Armor / 100.0f));
        return this;
    }

    public PlayerData SetAvoidRate()
    {
        AvoidRate = (((0.25f * Dexterity) + skillStatFix.AvoidRate) * 0.01f);
        return this;
    }
}

using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class PlayerStatusData
{
    // Variables
    [SerializeField] private float strength;
    [SerializeField] private float dexterity;
    [SerializeField] private float intellect;

    [SerializeField] private float moveSpeedByJob;
    [SerializeField] private float moveSpeed;

    [SerializeField] private float mainCharAttackPower;
    [SerializeField] private float attackMinPower;
    [SerializeField] private float attackMaxPower;

    [SerializeField] private float mainCharAttackSpeed;
    [SerializeField] private float attackSpeed;

    [SerializeField] private float health;
    [SerializeField] private float magicPoint;

    [SerializeField] private float armor;
    [SerializeField] private float avoidRate;

    // Properties
    /// <summary> 힘 </summary>
    public float Strength
    {
        get => strength;
        set => strength = value;
    }
    /// <summary> 민첩성 </summary>
    public float Dexterity
    {
        get => dexterity;
        set => dexterity = value;
    }
    /// <summary> 지능 </summary>
    public float Intellect
    {
        get => intellect;
        set => intellect = value;
    }

    /// <summary> 직업별 이동속도 </summary>
    public float MoveSpeedByJob
    {
        get => moveSpeedByJob;
        set => moveSpeedByJob = value;
    }
    /// <summary> 이동속도 </summary>
    public float MoveSpeed
    {
        get => moveSpeed;
        set => moveSpeed = value;
    }

    /// <summary> 캐릭터 메인 속성 공격력 </summary>
    public float MainCharAttackPower
    {
        get => mainCharAttackPower;
        set => mainCharAttackPower = value;
    }
    /// <summary> 최소 공격력 </summary>
    public float AttackMinPower
    {
        get => attackMinPower;
        set => attackMinPower = value;
    }
    /// <summary> 최대 공격력 </summary>
    public float AttackMaxPower
    {
        get => attackMaxPower;
        set => attackMaxPower = value;
    }
    /// <summary> 최종 공격력 [Min: attackMinPower (inclusive) / Max: attackMaxPower (inclusive)] </summary>
    public float FinalAttackDamage
    {
        get => Random.Range(AttackMinPower, AttackMaxPower);
    }
    /// <summary> 치명타 피해 </summary>
    public float CriticalDamage
    {
        get => FinalAttackDamage * Random.Range(1.5f, 2.0f);
    }

    /// <summary> 캐릭터 공격 속도 </summary>
    public float MainCharAttackSpeed
    {
        get => mainCharAttackSpeed;
        set => mainCharAttackSpeed = value;
    }
    /// <summary> 공격 속도 </summary>
    public float AttackSpeed
    {
        get => attackSpeed;
        set => attackSpeed = value;
    }

    /// <summary> 체력 </summary>
    public float Health
    {
        get => health;
        set => health = value;
    }
    /// <summary> 마나 </summary>
    public float MagicPoint
    {
        get => magicPoint;
        set => magicPoint = value;
    }

    /// <summary> 방어력 </summary>
    public float Armor
    {
        get => armor;
        set => armor = value;
    }
    /// <summary> 회피 확률 </summary>
    public float AvoidRate
    {
        get => avoidRate;
        set => avoidRate = value;
    }

    public StatusFixer skillStatFix;

    public PlayerStatusData SetMoveSpeed()
    {
        MoveSpeed = (MoveSpeedByJob * (1.0f + skillStatFix.MoveSpeed));
        return this;
    }

    public PlayerStatusData SetHealth()
    {
        Health = (100 + (25 * Strength) + skillStatFix.Health) * (1.0f + (skillStatFix.Health / 100.0f));
        return this;
    }

    public PlayerStatusData SetMagicPoint()
    {
        MagicPoint = ((15.0f * Intellect) + skillStatFix.MagicPoint) * (1.0f + (skillStatFix.MagicPoint / 100.0f));
        return this;
    }

    public PlayerStatusData SetAttackMinPower()
    {
        AttackMinPower = ((MainCharAttackPower * 1.35f) + skillStatFix.AttackMinPower) * (1.0f + (skillStatFix.AttackMinPower / 100.0f));
        return this;
    }

    public PlayerStatusData SetAttackMaxPower()
    {
        AttackMaxPower = ((MainCharAttackPower * 2.25f) + skillStatFix.AttackMaxPower) * (1.0f + (skillStatFix.AttackMaxPower / 100.0f));
        return this;
    }

    public PlayerStatusData SetAttackSpeed()
    {
        AttackSpeed = (MainCharAttackSpeed / (1.0f + (0.02f * Dexterity) + skillStatFix.AttackSpeed));
        return this;
    }

    public PlayerStatusData SetArmor()
    {
        Armor = (-2.0f + (0.3f * Dexterity) + skillStatFix.Armor) * (1.0f + (skillStatFix.Armor / 100.0f));
        return this;
    }

    public PlayerStatusData SetAvoidRate()
    {
        AvoidRate = (((0.25f * Dexterity) + skillStatFix.AvoidRate) * 0.01f);
        return this;
    }
}

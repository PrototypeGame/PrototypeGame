using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class StatusBase
{
    // Variables

    [Header("Basic Status")]
    [SerializeField]
    private float strength;
    [SerializeField]
    private float dexterity;
    [SerializeField]
    private float intellect;

    [Header("Job Basic Status")]
    [SerializeField]
    private float basicAttackPower;
    [SerializeField]
    private float basicAttackSpeed;
    [SerializeField]
    private float basicMoveSpeed;

    [Header("Movement Status")]
    [SerializeField]
    private float moveSpeed;

    [Header("Variable Status")]
    [SerializeField]
    private float health;
    [Space(10)]
    [SerializeField]
    private float magicPoint;
    [Space(10)]
    [SerializeField]
    private float attackMinPower;
    [SerializeField]
    private float attackMaxPower;
    // attackMinPower <= Value <= attackMaxPower
    [SerializeField]
    private float finalAttackDamage;
    [SerializeField]
    private float attackSpeed;
    [SerializeField]
    private float criticalDamage;
    [Space(10)]
    [SerializeField]
    private float armor;
    [Space(10)]
    [SerializeField]
    private float avoidRate;

    // Properties
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }

    public float Strength { get => strength; set => strength = value; }
    public float Dexterity { get => dexterity; set => dexterity = value; }
    public float Intellect { get => intellect; set => intellect = value; }

    public float BasicAttackPower { get => basicAttackPower; set => basicAttackPower = value; }
    public float BasicAttackSpeed { get => basicAttackSpeed; set => basicAttackSpeed = value; }
    public float BasicMoveSpeed { get => basicMoveSpeed; set => basicMoveSpeed = value; }

    public float Health { get => health; set => health = value; }

    public float MagicPoint { get => magicPoint; set => magicPoint = value; }

    public float AttackMinPower { get => attackMinPower; set => attackMinPower = value; }
    public float AttackMaxPower { get => attackMaxPower; set => attackMaxPower = value; }
    public float FinalAttackDamage { get => finalAttackDamage; set => finalAttackDamage = value; }
    public float AttackSpeed { get => attackSpeed; set => attackSpeed = value; }
    public float CriticalDamage { get => criticalDamage; set => criticalDamage = value; }

    public float Armor { get => armor; set => armor = value; }

    public float AvoidRate { get => avoidRate; set => avoidRate = value; }
}

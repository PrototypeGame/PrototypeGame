using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class InGameUIManager : MonoBehaviour
{
    public Text bossName;
    public Text hpText;
    public Image bossHpBar;

    public Text timeLimitText;
    public Image timeLimitBar;

    private Boss.BossMonsterBase boss;
    private float bossHPPer;

    private float timePer;

    private void Awake()
    {
        boss = FindObjectOfType<Boss.BossMonsterBase>();
        bossHPPer = 1.0f / boss.maxHP;
        timePer = 1.0f / GameManager.timeRemaining;
    }

    private void Update()
    {
        bossHpBar.fillAmount = bossHPPer * boss.curHP;
        hpText.text = $"{boss.curHP} / {boss.maxHP}";

        timeLimitBar.fillAmount = timePer * GameManager.timeRemaining;
        timeLimitText.text = $"{Math.Truncate(GameManager.timeRemaining / 60)} : {Math.Truncate(GameManager.timeRemaining % 60)}";
    }
}

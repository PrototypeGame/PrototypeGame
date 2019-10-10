using UnityEngine;
using UnityEngine.UI;
using System;

public class InGameUIManager : MonoBehaviour
{
    public Text bossName;
    //public Text hpText;
    public Image bossHpBar;

    public Image playerHpBar;

    public Text timeLimitText;
    public Image timeLimitBar;

    private float bossHPPer;
    private float playerHPPer;

    private float timePer;

    private void Awake()
    {
        GameManager.boss = FindObjectOfType<Boss.GolemBehavior>();
        bossHPPer = 1.0f / GameManager.boss.stats.HP;
        playerHPPer = 1.0f / GameManager.player.statusManager.Health;
        timePer = 1.0f / GameManager.timeRemaining;
    }

    private void Update()
    {
        bossHpBar.fillAmount = bossHPPer * GameManager.boss.curHP;
        playerHpBar.fillAmount = playerHPPer * GameManager.player.statusManager.Health;

        timeLimitBar.fillAmount = timePer * GameManager.timeRemaining;
        int a = (int)Math.Truncate(GameManager.timeRemaining % 60);
        if (a < 10)
            timeLimitText.text = $"{Math.Truncate(GameManager.timeRemaining / 60)} : 0{Math.Truncate(GameManager.timeRemaining % 60)}";
        else
            timeLimitText.text = $"{Math.Truncate(GameManager.timeRemaining / 60)} : {Math.Truncate(GameManager.timeRemaining % 60)}";
    }
}

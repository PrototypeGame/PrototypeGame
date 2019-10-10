using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    enum GameState
    {
        Ready,
        Intro,
        Play,
        Victory,
        Defeat
    };

    private static GameManager _instance = null;

    public static GameManager Instance
    {
        get
        {
            if (_instance != null)
                return _instance;
            _instance = FindObjectOfType<GameManager>();

            return _instance;
        }
    }

    [Tooltip("초 단위 입력")]
    public float miniute;
    public float second;

    public Camera subCam;
    public static float timeRemaining;

    private static GameState state;
    public static Boss.GolemBehavior boss;
    public static PlayerFSMManager player;

    void Awake()
    {
        if (_instance == null)
            _instance = this;

        timeRemaining = (miniute * 60) + (second);
        state = GameState.Ready;
        boss = FindObjectOfType<Boss.GolemBehavior>();
        player = FindObjectOfType<PlayerFSMManager>();
        subCam.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case GameState.Intro:
                GameIntro();
                break;
            case GameState.Play:
                GamePlay();
                break;
            case GameState.Victory:
                Victory();
                break;
            case GameState.Defeat:
                Defeat();
                break;
        }
    }

    public static void OnGameRedy()
    {
        Instance.subCam.enabled = true;
        Instance.StartCoroutine(Instance.InroCut());
    }

    public IEnumerator InroCut()
    {
        boss.BossStart();
        yield return new WaitForSeconds(1.4f);
        Instance.subCam.GetComponent<FollowCamera>().shake();
        yield return new WaitForSeconds(1.4f);
        Instance.subCam.enabled = false;
        state = GameState.Intro;
    }

    void GameIntro()
    {
        state = GameState.Play;
    }

    void GamePlay()
    {
        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0.0f || player.OnDead())
        {
            state = GameState.Defeat;
        }

        else if (boss.OnDead())
        {
            state = GameState.Victory;
        }
    }

    void Victory()
    {
        Instance.subCam.enabled = true;
    }

    void Defeat()
    {

    }
}

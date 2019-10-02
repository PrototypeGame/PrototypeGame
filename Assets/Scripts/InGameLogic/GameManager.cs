using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    enum GameState
    {
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
    public static float timeRemaining;

    private GameState state;

    void Awake()
    {
        if (_instance == null)
            _instance = this;

        timeRemaining = (miniute * 60) + (second);
        state = GameState.Intro;
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
                timeRemaining -= Time.deltaTime;
                break;
            case GameState.Victory:
                break;
            case GameState.Defeat:
                break;
        }
    }

    void GameIntro()
    {
        state = GameState.Play;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerableCharacterState
{
    IDLE, MOVE, NORMALATTACK, SKILLATTACK, DAMAGE, DEAD
}

public class PlayerFSMManager : MonoBehaviour
{
    // Player Status Data
    public PlayerStatusManager status;

    // FSM Manage Variables
    private Dictionary<PlayerableCharacterState, PlayerFSMState> playerStates;

    public PlayerableCharacterState startState;
    [SerializeField]
    private PlayerableCharacterState currentState;
    //[SerializeField]
    private PlayerFSMState currentFSMAction;

    // Player Component Variables
    public Transform transf;
    public Rigidbody rigid;

    // Player Custom Class Variables
    [HideInInspector]
    public PlayerAnimatorController animCtrl;
    public PlayerTimerManager timeManager;

    // Key Inputs
    public GameKeyPreset[] moveKeys = new GameKeyPreset[4];
    public GameKeyPreset[] attackKeys = new GameKeyPreset[1];
    public GameKeyPreset[] skillKeys = new GameKeyPreset[4];

    private void Awake()
    {
        status = GetComponent<PlayerStatusManager>();
        status?.InitAllStatus();

        playerStates = new Dictionary<PlayerableCharacterState, PlayerFSMState>();
        CurrentState = startState;

        InitPlayerStates();

        transf = GetComponent<Transform>();
        rigid = GetComponent<Rigidbody>();

        animCtrl = GetComponentInChildren<PlayerAnimatorController>();
        timeManager = GetComponent<PlayerTimerManager>();

        InitKeys();
    }

    private void Start()
    {
        // Awake에 넣으면 PlayerAnimatorController에서 Animator를 불러오는 시기와 겹쳐 정상적으로 작동하지 않음
        SetPlayerState(startState);
    }

    private void Update()
    {
        CurrentFSMAction.FSMUpdate();

        timeManager.TimerUpdate();
    }

    private void FixedUpdate()
    {
        CurrentFSMAction.FSMFixedUpdate();
    }

    private void InitPlayerStates()
    {
        playerStates[PlayerableCharacterState.IDLE] = GetComponent<PlayerIDLE>();
        playerStates[PlayerableCharacterState.MOVE] = GetComponent<PlayerMOVE>();
        playerStates[PlayerableCharacterState.NORMALATTACK] = GetComponent<PlayerNORMALATTACK>();
        playerStates[PlayerableCharacterState.SKILLATTACK] = GetComponent<PlayerSKILLATTACK>();
        playerStates[PlayerableCharacterState.DAMAGE] = GetComponent<PlayerDAMAGE>();
        playerStates[PlayerableCharacterState.DEAD] = GetComponent<PlayerDEAD>();
    }

    private void InitKeys()
    {
        // Move Key
        moveKeys[0] = GameKeyPreset.LeftArrow;
        moveKeys[1] = GameKeyPreset.RightArrow;
        moveKeys[2] = GameKeyPreset.DownArrow;
        moveKeys[3] = GameKeyPreset.UpArrow;

        // Attack Key
        attackKeys[0] = GameKeyPreset.NormalAttack;

        // Skill Key
        skillKeys[0] = GameKeyPreset.Skill_1;
        skillKeys[1] = GameKeyPreset.Skill_2;
        skillKeys[2] = GameKeyPreset.Skill_3;
        skillKeys[3] = GameKeyPreset.Skill_Ultimate;
    }

    public PlayerFSMState CurrentFSMAction
    {
        get => currentFSMAction;
        set => currentFSMAction = value;
    }
    public PlayerableCharacterState CurrentState
    {
        get => currentState;
        set => currentState = value;
    }

    public void SetPlayerState(PlayerableCharacterState stat)
    {
        CurrentState = stat;
        CurrentFSMAction = playerStates[stat];

        foreach (var item in playerStates)
        {
            item.Value.enabled = false;
        }

        CurrentFSMAction.enabled = true;
        CurrentFSMAction.FSMStart();
    }
}

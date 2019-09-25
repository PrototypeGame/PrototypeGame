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
    public KeyCode[] moveKeys = new KeyCode[4];
    public KeyCode[] attackKeys = new KeyCode[1];
    public KeyCode[] skillKeys = new KeyCode[4];

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
        moveKeys[0] = KeyCode.LeftArrow;
        moveKeys[1] = KeyCode.RightArrow;
        moveKeys[2] = KeyCode.DownArrow;
        moveKeys[3] = KeyCode.UpArrow;

        // Attack Key
        attackKeys[0] = KeyCode.A;

        // Skill Key
        skillKeys[0] = KeyCode.Q;
        skillKeys[1] = KeyCode.W;
        skillKeys[2] = KeyCode.E;
        skillKeys[3] = KeyCode.R;
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

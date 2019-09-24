using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerableCharacterState
{
    IDLE, MOVE, NORMALATTACK, SKILLATTACK, DAMAGE, DEAD
}

public class PlayerFSMManager : MonoBehaviour
{
    // FSM Manage Variables
    private Dictionary<PlayerableCharacterState, PlayerFSMState> playerStates;

    public PlayerableCharacterState startState;
    [SerializeField]
    private PlayerableCharacterState currentState;
    [SerializeField]
    private PlayerFSMState currentFSMAction;

    // Player Component Variables
    private Transform transf;
    private Rigidbody rigid;

    // Player Custom Class Variables
    [HideInInspector]
    public PlayerAnimatorController animCtrl;

    private void Awake()
    {
        playerStates = new Dictionary<PlayerableCharacterState, PlayerFSMState>();
        CurrentState = startState;

        InitPlayerStates();

        transf = GetComponent<Transform>();
        rigid = GetComponent<Rigidbody>();

        animCtrl = GetComponentInChildren<PlayerAnimatorController>();
    }

    private void Start()
    {
        // Awake에 넣으면 PlayerAnimatorController에서 Animator를 불러오는 시기와 겹쳐 정상적으로 작동하지 않음
        SetPlayerState(startState);
    }

    private void Update()
    {
        CurrentFSMAction.FSMUpdate();
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

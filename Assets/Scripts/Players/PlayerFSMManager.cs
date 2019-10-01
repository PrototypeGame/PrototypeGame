using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayableCharacterState
{
    IDLE, MOVE, DASH, NORMALATTACK, SKILLATTACK, DAMAGE, DEAD
}

public class PlayerFSMManager : MonoBehaviour
{

    public Damageable damage;
    // Player Status Data
    public PlayerStatusManager status;

    // FSM Manage Variables
    private Dictionary<PlayableCharacterState, PlayerFSMState> playerStates = new Dictionary<PlayableCharacterState, PlayerFSMState>();

    public PlayableCharacterState startState;
    public PlayableCharacterState currentState;
    public PlayerFSMState currentFSMAction;

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
    public GameKeyPreset[] itemUseKeys = new GameKeyPreset[6];


    private void Awake()
    {
        status = GetComponent<PlayerStatusManager>();
        InitPlayerDefaultStatus();

        startState = PlayableCharacterState.IDLE;

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
        currentFSMAction?.FSMUpdate();

        timeManager.TimerUpdate();

        // Item Use Check
        if (PlayerInputController.CheckInputSignal(itemUseKeys))
        {
            ItemUse(PlayerInputController.ReturnInputKey(itemUseKeys));
        }
    }

    private void FixedUpdate()
    {
        currentFSMAction?.FSMFixedUpdate();
    }

    private void InitPlayerDefaultStatus()
    {
        status.Strength = 10.0f;
        status.Dexterity = 7.0f;
        status.Intellect = 7.0f;

        status.MoveSpeedByJob = 5.0f;
        status.MainCharAttackPower = 10.0f;
        status.MainCharAttackSpeed = 5.0f;

        status.Health = 100.0f;
        status.MagicPoint = 40.0f;

        status?.InitAllStatus();
    }

    private void InitPlayerStates()
    {
        playerStates[PlayableCharacterState.IDLE] = GetComponent<PlayerIDLE>();
        playerStates[PlayableCharacterState.MOVE] = GetComponent<PlayerMOVE>();
        playerStates[PlayableCharacterState.DASH] = GetComponent<PlayerDASH>();
        playerStates[PlayableCharacterState.NORMALATTACK] = GetComponent<PlayerNORMALATTACK>();
        playerStates[PlayableCharacterState.SKILLATTACK] = GetComponent<PlayerSKILLATTACK>();
        playerStates[PlayableCharacterState.DAMAGE] = GetComponent<PlayerDAMAGE>();
        playerStates[PlayableCharacterState.DEAD] = GetComponent<PlayerDEAD>();
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

        // Item Key
        itemUseKeys[0] = GameKeyPreset.ITEM_1;
        itemUseKeys[1] = GameKeyPreset.ITEM_2;
        itemUseKeys[2] = GameKeyPreset.ITEM_3;
        itemUseKeys[3] = GameKeyPreset.ITEM_4;
        itemUseKeys[4] = GameKeyPreset.ITEM_5;
        itemUseKeys[5] = GameKeyPreset.ITEM_6;

        Debug.Log("----- 키 설정 -----");
        foreach (var item in moveKeys)
        {
            Debug.Log(item + " - 정상등록됨");
        }
        foreach (var item in attackKeys)
        {
            Debug.Log(item + " - 정상등록됨");
        }
        foreach (var item in skillKeys)
        {
            Debug.Log(item + " - 정상등록됨");
        }
        Debug.Log("----- 키 체크완료 -----");
    }

    public void SetCurrentFSMAction(PlayerFSMState value, bool enable)
    {
        currentFSMAction = value;
        currentFSMAction.enabled = enable;
    }

    public void SetPlayerState(PlayableCharacterState stat)
    {
        foreach (var item in playerStates)
        {
            item.Value.enabled = false;
        }
        currentState = stat;
        SetCurrentFSMAction(playerStates[stat], true);
        currentFSMAction.FSMStart();
    }

    public void ItemUse(GameKeyPreset itemKey)
    {
        switch (itemKey)
        {
            case GameKeyPreset.ITEM_1:
                if (!TimerUtil.IsOnCoolTime(timeManager.itemUseTimer[0]))
                {
                    Debug.Log("Item 1 사용됨");
                    TimerUtil.TimerReset(timeManager.itemUseTimer[0]);
                } 
                break;
            case GameKeyPreset.ITEM_2:
                if (!TimerUtil.IsOnCoolTime(timeManager.itemUseTimer[1]))
                {
                    Debug.Log("Item 2 사용됨");
                    TimerUtil.TimerReset(timeManager.itemUseTimer[1]);
                }
                break;
            case GameKeyPreset.ITEM_3:
                if (!TimerUtil.IsOnCoolTime(timeManager.itemUseTimer[2]))
                {
                    Debug.Log("Item 3 사용됨");
                    TimerUtil.TimerReset(timeManager.itemUseTimer[2]);
                }
                break;
            case GameKeyPreset.ITEM_4:
                if (!TimerUtil.IsOnCoolTime(timeManager.itemUseTimer[3]))
                {
                    Debug.Log("Item 4 사용됨");
                    TimerUtil.TimerReset(timeManager.itemUseTimer[3]);
                }
                break;
            case GameKeyPreset.ITEM_5:
                if (!TimerUtil.IsOnCoolTime(timeManager.itemUseTimer[4]))
                {
                    Debug.Log("Item 5 사용됨");
                    TimerUtil.TimerReset(timeManager.itemUseTimer[4]);
                }
                break;
            case GameKeyPreset.ITEM_6:
                if (!TimerUtil.IsOnCoolTime(timeManager.itemUseTimer[5]))
                {
                    Debug.Log("Item 6 사용됨");
                    TimerUtil.TimerReset(timeManager.itemUseTimer[5]);
                }
                break;
            default:
                break;
        }
    }
}

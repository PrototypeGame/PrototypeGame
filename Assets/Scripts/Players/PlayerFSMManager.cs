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

    // Manager Scripts
    public PlayerTimerManager timeManager;
    public PlayerStatusManager statusManager;
    public PlayerSkillManager skillManager;
    public PlayerInputManager inputManager;

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
    
    private void Awake()
    {
        statusManager = GetComponent<PlayerStatusManager>();
        timeManager = GetComponent<PlayerTimerManager>();
        skillManager = GetComponent<PlayerSkillManager>();
        // InputManager GameObject
        inputManager = FindObjectOfType<PlayerInputManager>();

        InitPlayerDefaultStatus();
        InitPlayerDefaultTimer();
        InitPlayerDefaultSkill();
        InitPlayerDefaultKey();

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
        currentFSMAction?.FSMUpdate();

        timeManager.TimerUpdate();

        // Item Use Check
        if (InputControlUtil.CheckInputSignal(inputManager.itemUseKeys))
        {
            ItemUse(InputControlUtil.ReturnInputKey(inputManager.itemUseKeys));
        }
    }

    private void FixedUpdate()
    {
        currentFSMAction?.FSMFixedUpdate();
    }

    private void InitPlayerDefaultStatus()
    {
        statusManager.Strength = 10.0f;
        statusManager.Dexterity = 7.0f;
        statusManager.Intellect = 7.0f;

        statusManager.MoveSpeedByJob = 5.0f;
        statusManager.MainCharAttackPower = 10.0f;
        statusManager.MainCharAttackSpeed = 5.0f;

        statusManager.Health = 100.0f;
        statusManager.MagicPoint = 40.0f;

        statusManager?.InitAllStatus();
    }

    private void InitPlayerDefaultTimer()
    {
        timeManager.normalAttackTimer.coolTime = 1.0f;

        timeManager.skillAttackTimers[0].coolTime = 1.0f;
        timeManager.skillAttackTimers[1].coolTime = 1.0f;
        timeManager.skillAttackTimers[2].coolTime = 1.0f;
        timeManager.skillAttackTimers[3].coolTime = 1.0f;

        timeManager.dashTimer.coolTime = 2.0f;

        // TODO: 추후에 ITEM MANAGE CLASS에서 정보 불러오기
        timeManager.itemUseTimer[0].coolTime = 1.0f;
        timeManager.itemUseTimer[1].coolTime = 1.0f;
        timeManager.itemUseTimer[2].coolTime = 1.0f;
        timeManager.itemUseTimer[3].coolTime = 1.0f;
        timeManager.itemUseTimer[4].coolTime = 1.0f;
        timeManager.itemUseTimer[5].coolTime = 1.0f;
    }

    private void InitPlayerDefaultSkill()
    {

    }

    private void InitPlayerDefaultKey()
    {
        // Move Key
        inputManager.moveKeys[0] = GameKeyPreset.LeftArrow;
        inputManager.moveKeys[1] = GameKeyPreset.RightArrow;
        inputManager.moveKeys[2] = GameKeyPreset.DownArrow;
        inputManager.moveKeys[3] = GameKeyPreset.UpArrow;

        // Attack Key
        inputManager.attackKeys[0] = GameKeyPreset.NormalAttack;

        // Skill Key
        inputManager.skillKeys[0] = GameKeyPreset.Skill_1;
        inputManager.skillKeys[1] = GameKeyPreset.Skill_2;
        inputManager.skillKeys[2] = GameKeyPreset.Skill_3;
        inputManager.skillKeys[3] = GameKeyPreset.Skill_Ultimate;

        // Item Key
        inputManager.itemUseKeys[0] = GameKeyPreset.ITEM_1;
        inputManager.itemUseKeys[1] = GameKeyPreset.ITEM_2;
        inputManager.itemUseKeys[2] = GameKeyPreset.ITEM_3;
        inputManager.itemUseKeys[3] = GameKeyPreset.ITEM_4;
        inputManager.itemUseKeys[4] = GameKeyPreset.ITEM_5;
        inputManager.itemUseKeys[5] = GameKeyPreset.ITEM_6;
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

        startState = PlayableCharacterState.IDLE;
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

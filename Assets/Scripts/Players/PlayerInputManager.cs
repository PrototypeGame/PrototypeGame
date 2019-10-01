using UnityEngine;
using System.Collections;

public class PlayerInputManager : MonoBehaviour
{
    public GameKeyPreset[] moveKeys;
    public GameKeyPreset[] attackKeys;
    public GameKeyPreset[] skillKeys;
    public GameKeyPreset[] itemUseKeys;

    PlayerInputManager()
    {
        moveKeys = new GameKeyPreset[4];
        attackKeys = new GameKeyPreset[1];
        skillKeys = new GameKeyPreset[4];
        itemUseKeys = new GameKeyPreset[6];
    }

    public void DebugKeySetCheck()
    {
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
        foreach (var item in itemUseKeys)
        {
            Debug.Log(item + " - 정상등록됨");
        }
        Debug.Log("----- 키 체크완료 -----");
    }
}

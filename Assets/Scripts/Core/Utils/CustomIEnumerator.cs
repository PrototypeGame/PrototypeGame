using UnityEngine;
using System.Collections;

public class WaitForKeyInput : IEnumerator
{
    private GameKeyPreset inputKey;
    public WaitForKeyInput(GameKeyPreset key) { inputKey = key; }

    // Wait 동안 처리할 작업
    public object Current { get => null; }
    public bool MoveNext() => !GameKey.GetKeyDown(inputKey);
    public void Reset() { /* TODO: Reset? */ }
}

using UnityEngine;
using GameSystem.InnerSystem;

/// <summary>
/// 带初始化方法的Behaviour，（禁用awake）
/// </summary>
public abstract class InitBehaviour : MonoBehaviour {
    protected virtual void Awake()
    {
        GameMessageManager.gameInit += Init;
    }

    private void OnDestroy()
    {
        GameMessageManager.gameInit -= Init;
    }
    protected abstract void Init();
}

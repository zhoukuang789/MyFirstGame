using UnityEngine;
using System.Collections;

/// <summary>
/// 可以显示血条ui的一个对象
/// </summary>
public class ConstructureHealth : MonoBehaviour
{
    public int hpMax;
    public int hpCurrent;

    /// <summary>
    /// RegisterHpBar应该在想要显示时调用
    /// 这里写在Start里面并不表示要这样调用
    /// </summary>
    public void Start()
    {
        RegisterHpBar();
    }

    public void RegisterHpBar()
    {
        HpBarSystem.instance.Register(this);
    }

    /// <summary>
    /// 单位死亡后，或者过场动画，任务结束时隐藏血条
    /// </summary>
    public void UnRegisterHpBar()
    {
        HpBarSystem.instance.UnRegister(this);
    }

    public Vector3 GetWorldPosition()
    {
        return transform.position;
    }
}

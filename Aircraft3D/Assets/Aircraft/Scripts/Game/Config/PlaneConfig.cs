using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class PlaneConfig : ScriptableObject
{
    public string id;

    public string title;
    public string desc;

    public float mass;

    [Range(0.1f, 5f)]
    public float fireInterval;

    public PlaneMoveConfig move;
}

[System.Serializable]
public class PlaneMoveConfig
{
    [Tooltip("飞机初始速度")]
    public float initialSpeed = 300f;
    [Tooltip("飞机最大飞行速度")]
    public float maxSpeed = 500f;
    public float minSpeed = 200f;           // 飞机最小飞行速度
    public float stayFlySpeed = 220f;       // 飞机维持升力速度
    public float acceleration = 40f;        // 按住W时的飞机加速度
    public float deceleration = 20f;        // 按住S时的飞机减速度
    public float naturalAcceleration = 15f; // 不按任何按键时的飞机加速度
    public float naturalDeceleration = 10f; // 不按任何按键时的飞机减速度

    public AnimationCurve pitchRateCurve;   // 飞机俯仰速率曲线
    public float pitchScaleFactor = 45f;    // 飞机俯仰比例系数
    public AnimationCurve rollRateCurve;    // 飞机滚转速率曲线
    public float rollScaleFactor = 90f;     // 飞机滚转比例系数
    public AnimationCurve yawRateCurve;     // 飞机偏航速率曲线
    public float yawScaleFactor = 20f;      // 飞机偏航比例系数
}
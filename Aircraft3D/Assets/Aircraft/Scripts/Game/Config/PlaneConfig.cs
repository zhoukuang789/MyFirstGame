using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class PlaneConfig : ScriptableObject {
    public string id;

    public string title;
    public string desc;

    public float mass;

    [Range(0.1f, 5f)]
    public float fireInterval;

    public PlaneMovementConfig movement;
}

[System.Serializable]
public class PlaneMovementConfig {
    [Tooltip("飞机初始速度")]
    public float initialSpeed = 62f;

    public AnimationCurve pitchRateCurve; // 飞机俯仰速率曲线
    public float pitchScaleFactor = 45f; // 飞机俯仰比例系数
    public AnimationCurve rollRateCurve; // 飞机滚转速率曲线
    public float rollScaleFactor = 90f; // 飞机滚转比例系数
    public AnimationCurve yawRateCurve; // 飞机偏航速率曲线
    public float yawScaleFactor = 20f; // 飞机偏航比例系数

    [Tooltip("升力系数曲线")]
    public AnimationCurve liftCoefficientCurve;

    [Tooltip("阻力系数曲线")]
    public AnimationCurve dragCoefficientCurve;

    [Tooltip("翼面俯视面积")]
    public float wingArea;

    [Tooltip("飞机重量")]
    public float mass;

    [Tooltip("发动机最大功率")]
    public float maxPower;

    [Tooltip("发动机当前功率")]
    public float powerOne;
}
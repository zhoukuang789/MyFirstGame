using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class PlaneConfig : ScriptableObject {
    public string id;

    public string title;
    public string desc;
    [Range(0.1f, 5f)]
    public float fireInterval;

    public PlaneMovementConfig movement;

    public PlaneFightConfig fight;
}

[System.Serializable]
public class PlaneMovementConfig {
    [Tooltip("飞机初始速度")]
    public float initThrust;

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

    public AnimationCurve pitchMomentCoefficientCurve;

    [Tooltip("翼面俯视面积")]
    public float wingArea;

    [Tooltip("飞机重量")]
    public float mass;

    [Tooltip("发动机最大功率")]
    public float maxPower;
    
    [Tooltip("发动机最大功率")]
    public float minPower;

    public float addPowerSpeed;
    public float reducePowerSpeed;
}

[System.Serializable]
public class PlaneFightConfig {
    
    [Tooltip("总生命值")]
    public float totalHealth;

    [Tooltip("各个部位的生命值")]
    public List<PartHealth> partHeaths;

    [Tooltip("伤害")]
    public float damage;

    [Tooltip("子弹射程")]
    public float bulletRange;
    
}

[System.Serializable]
public class PartHealth {
    public PlaneHitCollider.PlaneHitPart part;
    public float health;
    public float armor;
}
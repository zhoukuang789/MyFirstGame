using System;
using UnityEngine;

namespace Plane.Movement {
    
    [CreateAssetMenu(fileName = "PlaneMovementConfig", menuName = "PlaneMovementConfig", order = 0)]
    public class PlaneMovementConfig : ScriptableObject {
        
        /// <summary>
        /// 飞机重量
        /// </summary>
        public float mass;
        
        /// <summary>
        /// 加速推力大小曲线，按住W加速时通过该曲线施加推力，横坐标是杆量，范围是0~1，纵坐标是推力比例，范围是0~1
        /// </summary>
        [Header("推力相关的参数")]
        public AnimationCurve accelerateTrustSizeCurve;

        /// <summary>
        /// 最大加速推力
        /// </summary>
        public float maxAccelerateTrustSize;

        /// <summary>
        /// 减速推力大小曲线，按住S减速时通过该曲线施加反向的推力，横坐标是杆量，范围是0~1，纵坐标是推力比例，范围是0~1
        /// </summary>
        public AnimationCurve decelerateTrustSizeCurve;

        /// <summary>
        /// 最大减速推力大小
        /// </summary>
        public float maxDecelerateTrustSize;

        /// <summary>
        /// 最大速度(在forward方向上)
        /// </summary>
        public float maxSpeedOnForward;

        /// <summary>
        /// 自动巡航速度(在forward方向上)
        /// </summary>
        public float autoSpeedOnForward;

        /// <summary>
        /// 自动推力
        /// </summary>
        public float autoTrust;

        
        

        /// <summary>
        /// 零升迎角
        /// </summary>
        [Header("与升力相关的参数")]
        public float angleOfAttack0 = -3f;
        
        /// <summary>
        /// 机翼面积
        /// </summary>
        public float wingArea = 7f;

        /// <summary>
        /// 标准的升力系数曲线，横坐标为 迎角，范围0~90，纵坐标为 升力系数，范围0~1
        /// </summary>
        public AnimationCurve liftCoefficientCurve;

        /// <summary>
        /// 最大升力系数
        /// </summary>
        public float maxLiftCoefficient;

        
        
        
        /// <summary>
        /// 标准的阻力系数曲线，横坐标为迎角，范围0~90，纵坐标为 阻力系数，范围0~1
        /// </summary>
        [Header("与阻力相关的参数")]
        public AnimationCurve dragCoefficientCurve;

        /// <summary>
        /// 最大阻力系数
        /// </summary>
        public float maxDragCoefficient;


        
        
        /// <summary>
        /// 尾翼大小
        /// </summary>
        [Header("与偏航相关的参数")]
        public float tailArea = 1f;

        /// <summary>
        /// 标准的侧力系数曲线，横坐标为侧滑，范围0~90，纵坐标为 侧力系数，范围0~1
        /// </summary>
        public AnimationCurve sideForceCoefficientCurve;

        /// <summary>
        /// 最大侧力系数
        /// </summary>
        public float maxSideForceCoefficient = 0.5f;
        
        

        /// <summary>
        /// 俯仰速率曲线，横坐标表示当前速度/最大速度，范围0~1，纵坐标表示速率，范围0~1
        /// </summary>
        [Header("与机动相关的参数")]
        public AnimationCurve pitchRateCurve;
        
        /// <summary>
        /// 最大俯仰速率
        /// </summary>
        public float maxPitchRate;

        /// <summary>
        /// 滚转速率曲线
        /// </summary>
        public AnimationCurve rollRateCurve;
        
        /// <summary>
        /// 最大滚转速率
        /// </summary>
        public float maxRollRate;

        /// <summary>
        /// 偏航速率曲线
        /// </summary>
        public AnimationCurve yawRateCurve;
        /// <summary>
        /// 最大偏航速率
        /// </summary>
        public float maxYawRate;
        
        
    }
}
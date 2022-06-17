using UnityEngine;

namespace Plane {
    [CreateAssetMenu(fileName = "PlaneConfig", menuName = "PlaneConfig", order = 0)]
    public class PlaneConfig : ScriptableObject {
        
        /// <summary>
        /// 飞机在推力方向上的最大速度
        /// </summary>
        [Header("与推力相关的参数")]
        public float maxSpeedOnForward;
        
        /// <summary>
        /// 飞机在推力方向上的最小速度
        /// </summary>
        public float minSpeedOnForward;

        /// <summary>
        /// 飞机在推力方向上的自动平衡速度
        /// </summary>
        public float autoSpeedOnForward;

        /// <summary>
        /// 加速推力曲线，横坐标是杆量，范围是0-1，纵坐标是增加推力的比例，范围是0-1
        /// </summary>
        public AnimationCurve addTrustCurve;
        
        /// <summary>
        /// 最大加速推力
        /// </summary>
        public float maxAddTrust;

        /// <summary>
        /// 减速推力曲线，
        /// </summary>
        public AnimationCurve reduceTrustCurve;

        /// <summary>
        /// 最大减速推力
        /// </summary>
        public float maxReduceTrust;
        

        /// <summary>
        /// 零升迎角
        /// </summary>
        [Header("气动力参数")]
        public float angleOfAttack0;

        
        /// <summary>
        /// 机翼面积
        /// </summary>
        [Header("与升力相关的参数")]
        public float wingArea;
        
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
        public float tailArea;

        
        /// <summary>
        /// 标准的侧力系数曲线，横坐标为侧滑，范围0~90，纵坐标为 侧力系数，范围0~1
        /// </summary>
        public AnimationCurve sideForceCoefficientCurve;

        /// <summary>
        /// 最大侧力系数
        /// </summary>
        public float maxSideForceCoefficient;

        /// <summary>
        /// 俯仰灵敏度曲线，横坐标表示当前速度/最大速度（在推力轴上的速度），范围0~1，纵坐标表示灵敏度，范围0~1
        /// </summary>
        [Header("与机动相关的参数")]
        public AnimationCurve pitchSensitivityCurve;

        /// <summary>
        /// 最大俯仰灵敏度
        /// </summary>
        public float maxPitchSensitivity;

        /// <summary>
        /// 滚转灵敏度曲线，横坐标表示当前速度/最大速度（在推力轴上的速度），范围0~1，纵坐标表示灵敏度，范围0~1
        /// </summary>
        public AnimationCurve rollSensitivityCurve;

        /// <summary>
        /// 最大滚转灵敏度
        /// </summary>
        public float maxRollSensitivity;
        
        /// <summary>
        /// 偏航灵敏度曲线，横坐标表示当前速度/最大速度（在推力轴上的速度），范围0~1，纵坐标表示灵敏度，范围0~1
        /// </summary>
        public AnimationCurve yawSensitivityCurve;

        /// <summary>
        /// 最大偏航灵敏度
        /// </summary>
        public float maxYawSensitivity;
    }
}
using System;
using UnityEngine;

namespace Plane {
    /// <summary>
    /// 这个类负责计算飞机对象的内禀属性，如升力、阻力、速度等等
    /// </summary>
    public class PlaneMoveBehaviour : MonoBehaviour {
        /// <summary>
        /// rigidbody
        /// </summary>
        private Rigidbody rb;

        public Rigidbody GetRigidbody() {
            return rb;
        }


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
        /// 根据杆量获取对应的加速推力大小
        /// </summary>
        /// <param name="volume"></param>
        /// <returns></returns>
        public float GetAccelerateTrustSize(float volume) {
            volume = Mathf.Clamp01(volume);
            return accelerateTrustSizeCurve.Evaluate(volume) * maxAccelerateTrustSize;
        }

        /// <summary>
        /// 减速推力大小曲线，按住S减速时通过该曲线施加反向的推力，横坐标是杆量，范围是0~1，纵坐标是推力比例，范围是0~1
        /// </summary>
        public AnimationCurve decelerateTrustSizeCurve;

        /// <summary>
        /// 最大减速推力大小
        /// </summary>
        public float maxDecelerateTrustSize;

        /// <summary>
        /// 根据杆量获取对应的减速推力大小
        /// </summary>
        /// <param name="volume"></param>
        /// <returns></returns>
        public float GetDecelerateTrustSize(float volume) {
            volume = Mathf.Clamp01(volume);
            return decelerateTrustSizeCurve.Evaluate(volume) * maxDecelerateTrustSize;
        }

        /// <summary>
        /// 最大速度
        /// </summary>
        public float maxSpeed;

        public float GetMaxSpeed() {
            return maxSpeed;
        }

        /// <summary>
        /// 获取当前速度
        /// </summary>
        /// <returns></returns>
        public float GetCurrentSpeed() {
            return rb.velocity.magnitude;
        }

        /// <summary>
        /// 自动巡航速度
        /// </summary>
        public float autoSpeed;

        public float GetAutoSpeed() {
            return autoSpeed;
        }

        public float autoTrust;


        /// <summary>
        /// 零升迎角
        /// </summary>
        [Header("与升力相关的参数")]
        public float angleOfAttack0 = -3f;

        [SerializeField]
        private float angleOfAttack;


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
        public float maxLiftCoefficient = 1.5f;

        /// <summary>
        /// 升力
        /// </summary>
        [SerializeField]
        private Vector3 lift;

        /// <summary>
        /// 升力大小
        /// </summary>
        [SerializeField]
        private float liftSize;

        /// <summary>
        /// 标准的阻力系数曲线，横坐标为迎角，范围0~90，纵坐标为 阻力系数，范围0~1
        /// </summary>
        [Header("与阻力相关的参数")]
        public AnimationCurve dragCoefficientCurve;

        /// <summary>
        /// 最大阻力系数
        /// </summary>
        public float maxDragCoefficient = 1.2f;


        /// <summary>
        /// 阻力
        /// </summary>
        [SerializeField]
        private Vector3 drag;

        /// <summary>
        /// 阻力大小
        /// </summary>
        [SerializeField]
        private float dragSize;


        /// <summary>
        /// 尾翼大小
        /// </summary>
        [Header("与偏航相关的参数")]
        public float tailArea = 10f;

        /// <summary>
        /// 侧滑角
        /// </summary>
        [SerializeField]
        private float angleOfSideslip;

        /// <summary>
        /// 标准的侧力系数曲线，横坐标为侧滑，范围0~90，纵坐标为 侧力系数，范围0~1
        /// </summary>
        public AnimationCurve sideForceCoefficientCurve;

        /// <summary>
        /// 最大侧力系数
        /// </summary>
        public float maxSideForceCoefficient = 0.5f;


        /// <summary>
        /// 侧力
        /// </summary>
        [SerializeField]
        private Vector3 sideForce;

        /// <summary>
        /// 侧力大小
        /// </summary>
        [SerializeField]
        private float sideForceSize;

        /// <summary>
        /// 俯仰速率曲线，横坐标表示当前速度/最大速度，范围0~1，纵坐标表示速率，范围0~1
        /// </summary>
        [Header("与机动相关的参数")]
        public AnimationCurve pitchRateCurve;
        /// <summary>
        /// 最大俯仰速率
        /// </summary>
        public float maxPitchRate = 36f;
        /// <summary>
        /// 获取当前速度下的俯仰速率
        /// </summary>
        /// <param name="volume">-1-1</param>
        /// <returns></returns>
        public float GetPitchRate(float volume) {
            if (volume >= -1f && volume < 0) {
                return -GetPitchRate(-volume);
            }
            return pitchRateCurve.Evaluate(Mathf.Clamp01(GetCurrentSpeed() / maxSpeed)) * maxPitchRate *
                   Mathf.Clamp01(volume) * Time.deltaTime;
        }

        /// <summary>
        /// 滚转速率曲线
        /// </summary>
        public AnimationCurve rollRateCurve;
        /// <summary>
        /// 最大滚转速率
        /// </summary>
        public float maxRollRate = 36f;
        /// <summary>
        /// 获取当前速度下的俯仰速率
        /// </summary>
        /// <param name="volume">-1-1</param>
        /// <returns></returns>
        public float GetRollRate(float volume) {
            if (volume >= -1f && volume < 0) {
                return -GetRollRate(-volume);
            }
            return rollRateCurve.Evaluate(Mathf.Clamp01(GetCurrentSpeed() / maxSpeed)) * maxRollRate *
                   Mathf.Clamp01(volume) * Time.deltaTime;
        }

        /// <summary>
        /// 偏航速率曲线
        /// </summary>
        public AnimationCurve yawRateCurve;
        /// <summary>
        /// 最大偏航速率
        /// </summary>
        public float maxYawRate = 6f;
        /// <summary>
        /// 获取当前速度下的偏航速率
        /// </summary>
        /// <param name="volume"></param>
        /// <returns></returns>
        public float GetYawRate(float volume) {
            return yawRateCurve.Evaluate(Mathf.Clamp01(GetCurrentSpeed() / maxSpeed)) * maxYawRate *
                   Mathf.Clamp01(volume) * Time.deltaTime;
        }
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        private void Start() {
            rb = GetComponent<Rigidbody>();
        }

        private void Update() {
            // 计算迎角
            angleOfAttack = AerodynamicCalculation.CalculateAngleOfAttack(transform, rb.velocity);
            // 侧滑角
            angleOfSideslip = AerodynamicCalculation.CalculateAngleOfSideslip(transform, rb.velocity);

            // 计算升力
            // 1.计算升力系数
            float liftCoefficient = AerodynamicCalculation.CalculateLiftCoefficient(liftCoefficientCurve,
                maxLiftCoefficient,
                angleOfAttack, angleOfAttack0);
            // 2.计算升力
            lift = AerodynamicCalculation.CalculateLift(transform, rb.velocity, wingArea, liftCoefficient);
            liftSize = lift.magnitude;

            // 计算阻力
            // 1.计算阻力系数
            float dragCoefficient = AerodynamicCalculation.CalculateDragCoefficient(dragCoefficientCurve,
                maxDragCoefficient,
                angleOfAttack, angleOfAttack0);
            // 计算阻力
            drag = AerodynamicCalculation.CalculateDrag(transform, rb.velocity, wingArea, dragCoefficient);
            dragSize = drag.magnitude;

            // 计算侧力
            // 1.侧力系数
            float sideForceCoefficient = AerodynamicCalculation.CalculateSideForceCoefficient(sideForceCoefficientCurve,
                maxSideForceCoefficient, angleOfSideslip);
            // 侧力
            sideForce = AerodynamicCalculation.CalculateSideForce(transform, rb.velocity, tailArea,
                sideForceCoefficient);
            sideForceSize = sideForce.magnitude;
        }

        private void FixedUpdate() {
            // 施加力
            rb.AddForce(lift + drag + sideForce);

            // 推力自适应
            if (GetCurrentSpeed() < autoSpeed)
                rb.AddForce(transform.forward * autoTrust);
            if (GetCurrentSpeed() > autoSpeed)
                rb.AddForce(-transform.forward * autoTrust);
        }

        /// <summary>
        /// 向量 from 在向量 to 上的投影向量
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        private Vector3 ProjectOnVector(Vector3 from, Vector3 to) {
            //to的单位向量
            Vector3 unitNormalized = to.normalized;
            //投影向量的长度
            float length = Vector3.Dot(from, to) / to.magnitude;
            return length * unitNormalized;
        }

        /// <summary>
        /// 飞机沿着axis轴旋转angle度
        /// </summary>
        /// <param name="angle"></param>
        /// <param name="axis"></param>
        public void Rotate(float angle, Vector3 axis) {
            Quaternion quaternion = Quaternion.AngleAxis(angle, axis);
            transform.rotation = quaternion * transform.rotation;
        }
    }
}
using System;
using UnityEngine;

namespace Airplane.Movement {
    
    /// <summary>
    /// 负责飞机的运动
    /// </summary>
    public class PlaneMovementBehaviour : MonoBehaviour {
        
        // --------------------------field ------------------------------------------

        /// <summary>
        /// rigidbody
        /// </summary>
        private Rigidbody rb;


        // ---------------------------推力相关的参数----------------------------------
        
        /// <summary>
        /// 加速推力大小曲线，按住W加速时通过该曲线施加推力，横坐标是杆量，范围是0~1，纵坐标是推力比例，范围是0~1
        /// </summary>
        private AnimationCurve accelerateTrustSizeCurve;

        /// <summary>
        /// 最大加速推力
        /// </summary>
        private float maxAccelerateTrustSize;

        /// <summary>
        /// 减速推力大小曲线，按住S减速时通过该曲线施加反向的推力，横坐标是杆量，范围是0~1，纵坐标是推力比例，范围是0~1
        /// </summary>
        private AnimationCurve decelerateTrustSizeCurve;

        /// <summary>
        /// 最大减速推力大小
        /// </summary>
        private float maxDecelerateTrustSize;
        
        /// <summary>
        /// 最大速度(在forward方向上)
        /// </summary>
        private float maxSpeedOnForward;

        /// <summary>
        /// 自动巡航速度(在forward方向上)
        /// </summary>
        private float autoSpeedOnForward;

        /// <summary>
        /// 自动推力
        /// </summary>
        private float autoTrust;
        

        /// <summary>
        /// 飞机当前速度(在forward方向上)，分方向，与forward同侧为正，相反为负
        /// </summary>
        private float currentSpeedOnForward;

        
        
        // ---------------------------升力相关的参数----------------------------------

        /// <summary>
        /// 零升迎角
        /// </summary>
        private float angleOfAttack0;
        
        /// <summary>
        /// 机翼面积
        /// </summary>
        private float wingArea;

        /// <summary>
        /// 标准的升力系数曲线，横坐标为 迎角，范围0~90，纵坐标为 升力系数，范围0~1
        /// </summary>
        private AnimationCurve liftCoefficientCurve;

        /// <summary>
        /// 最大升力系数
        /// </summary>
        private float maxLiftCoefficient;
        

        /// <summary>
        /// 当前迎角
        /// </summary>
        private float angleOfAttack;

        /// <summary>
        /// 当前升力
        /// </summary>
        private Vector3 lift;

        /// <summary>
        /// 当前升力大小
        /// </summary>
        private float liftSize;
        
        
        
        // ---------------------------阻力相关的参数----------------------------------
        
        /// <summary>
        /// 标准的阻力系数曲线，横坐标为迎角，范围0~90，纵坐标为 阻力系数，范围0~1
        /// </summary>
        private AnimationCurve dragCoefficientCurve;

        /// <summary>
        /// 最大阻力系数
        /// </summary>
        private float maxDragCoefficient;


        /// <summary>
        /// 当前阻力
        /// </summary>
        private Vector3 drag;

        /// <summary>
        /// 当前阻力大小
        /// </summary>
        private float dragSize;
        
        
        
        // ---------------------------阻力相关的参数----------------------------------

        /// <summary>
        /// 尾翼大小
        /// </summary>
        private float tailArea;

        /// <summary>
        /// 标准的侧力系数曲线，横坐标为侧滑，范围0~90，纵坐标为 侧力系数，范围0~1
        /// </summary>
        private AnimationCurve sideForceCoefficientCurve;

        /// <summary>
        /// 最大侧力系数
        /// </summary>
        private float maxSideForceCoefficient;


        /// <summary>
        /// 当前侧滑角
        /// </summary>
        private float angleOfSideslip;

        /// <summary>
        /// 当前侧力
        /// </summary>
        private Vector3 sideForce;

        /// <summary>
        /// 当前侧力大小
        /// </summary>
        private float sideForceSize;

        
        
        // ---------------------------机动相关的参数----------------------------------
        
        /// <summary>
        /// 俯仰速率曲线，横坐标表示当前速度/最大速度，范围0~1，纵坐标表示速率比例，范围0~1
        /// </summary>
        private AnimationCurve pitchRateCurve;
        
        /// <summary>
        /// 最大俯仰速率
        /// </summary>
        private float maxPitchRate;
        

        /// <summary>
        /// 滚转速率曲线，横坐标表示当前速度/最大速度，范围0~1，纵坐标表示速率比例，范围0~1
        /// </summary>
        private AnimationCurve rollRateCurve;
        
        /// <summary>
        /// 最大滚转速率
        /// </summary>
        private float maxRollRate;
        

        /// <summary>
        /// 偏航速率曲线，横坐标表示当前速度/最大速度，范围0~1，纵坐标表示速率比例，范围0~1
        /// </summary>
        private AnimationCurve yawRateCurve;
        /// <summary>
        /// 最大偏航速率
        /// </summary>
        private float maxYawRate;
        
        
        
        // ---------------------------mono method----------------------------------

        private void Awake() {
            rb = GetComponent<Rigidbody>();
            PlaneMovementConfig planeMovementConfig = GetComponent<PlaneBehaviour>().planeConfig.planeMovementConfig;

            rb.mass = planeMovementConfig.mass;
            
            // ---------------------------推力相关的参数----------------------------------
            accelerateTrustSizeCurve = planeMovementConfig.accelerateTrustSizeCurve;
            maxAccelerateTrustSize = planeMovementConfig.maxAccelerateTrustSize;
            decelerateTrustSizeCurve = planeMovementConfig.decelerateTrustSizeCurve;
            maxDecelerateTrustSize = planeMovementConfig.maxDecelerateTrustSize;
            maxSpeedOnForward = planeMovementConfig.maxSpeedOnForward;
            autoSpeedOnForward = planeMovementConfig.autoSpeedOnForward;
            autoTrust = planeMovementConfig.autoTrust;

            // ---------------------------升力相关的参数----------------------------------
            angleOfAttack0 = planeMovementConfig.angleOfAttack0;
            wingArea = planeMovementConfig.wingArea;
            liftCoefficientCurve = planeMovementConfig.liftCoefficientCurve;
            maxLiftCoefficient = planeMovementConfig.maxLiftCoefficient;

            // ---------------------------阻力相关的参数----------------------------------
            dragCoefficientCurve = planeMovementConfig.dragCoefficientCurve;
            maxDragCoefficient = planeMovementConfig.maxDragCoefficient;

            // ---------------------------阻力相关的参数----------------------------------
            tailArea = planeMovementConfig.tailArea;
            sideForceCoefficientCurve = planeMovementConfig.sideForceCoefficientCurve;
            maxSideForceCoefficient = planeMovementConfig.maxSideForceCoefficient;

            // ---------------------------机动相关的参数----------------------------------
                
            pitchRateCurve = planeMovementConfig.pitchRateCurve;
            maxPitchRate = planeMovementConfig.maxPitchRate;
            
            rollRateCurve = planeMovementConfig.rollRateCurve;
            maxRollRate = planeMovementConfig.maxRollRate;
            
            yawRateCurve = planeMovementConfig.yawRateCurve;
            maxYawRate = planeMovementConfig.maxYawRate;
        }

        private void Update() {
            // 计算速度
            Vector3 currentVelocityOnForward = ProjectOnVector(rb.velocity, transform.forward);
            currentSpeedOnForward = currentVelocityOnForward.magnitude;
            if (Vector3.Dot(currentVelocityOnForward, transform.forward) < 0f) {
                currentSpeedOnForward = -currentVelocityOnForward.magnitude;
            } 
            
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
            // 2.计算阻力
            drag = AerodynamicCalculation.CalculateDrag(transform, rb.velocity, wingArea, dragCoefficient);
            dragSize = drag.magnitude;

            // 计算侧力
            // 1.侧力系数
            float sideForceCoefficient = AerodynamicCalculation.CalculateSideForceCoefficient(sideForceCoefficientCurve,
                maxSideForceCoefficient, angleOfSideslip);
            // 2.侧力
            sideForce = AerodynamicCalculation.CalculateSideForce(transform, rb.velocity, tailArea,
                sideForceCoefficient);
            sideForceSize = sideForce.magnitude;
            
            // Debug.Log("AOA: " + angleOfAttack);
            // Debug.Log("LiftCoefficient: " + liftCoefficient);
            // Debug.Log("Lift：" + lift + " , " + liftSize);
            // Debug.Log("===================================");
        }
        
        
        private void FixedUpdate() {
            // 施加力
            rb.AddForce(lift + drag + sideForce);

            // 推力自适应
            if (currentSpeedOnForward < autoSpeedOnForward)
                rb.AddForce(transform.forward * autoTrust);
            if (currentSpeedOnForward > autoSpeedOnForward)
                rb.AddForce(-transform.forward * autoTrust);
        }
        
        // -------------------------------function---------------------------------------

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
        
        
        
        // ---------------------------Getter & Setter----------------------------------

        /// <summary>
        /// 获取rigidbody
        /// </summary>
        /// <returns></returns>
        public Rigidbody GetRigidbody() {
            return rb;
        }

        /// <summary>
        /// 获取当期速度（在forward方向上）
        /// </summary>
        /// <returns></returns>
        public float GetCurrentSpeedOnForward() {
            return currentSpeedOnForward;
        }

        /// <summary>
        /// 获取最大速度（在forward方向上）
        /// </summary>
        /// <returns></returns>
        public float GetMaxSpeedOnForward() {
            return maxSpeedOnForward;
        }
        
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
        /// 根据杆量获取对应的减速推力大小
        /// </summary>
        /// <param name="volume"></param>
        /// <returns></returns>
        public float GetDecelerateTrustSize(float volume) {
            volume = Mathf.Clamp01(volume);
            return decelerateTrustSizeCurve.Evaluate(volume) * maxDecelerateTrustSize;
        }
        
        /// <summary>
        /// 获取当前速度下的俯仰速率
        /// </summary>
        /// <param name="volume">-1-1</param>
        /// <returns></returns>
        public float GetPitchRate(float volume) {
            if (volume >= -1f && volume < 0) {
                return -GetPitchRate(-volume);
            }
            volume = Mathf.Clamp01(volume);
            return pitchRateCurve.Evaluate(Mathf.Clamp01(currentSpeedOnForward / maxSpeedOnForward)) * maxPitchRate *
                   volume * Time.deltaTime;
        }
        
        /// <summary>
        /// 获取当前速度下的偏航速率
        /// </summary>
        /// <param name="volume">-1~1</param>
        /// <returns></returns>
        public float GetYawRate(float volume) {
            if (volume >= -1f && volume < 0) {
                return -GetYawRate(-volume);
            }
            volume = Mathf.Clamp01(volume);
            return yawRateCurve.Evaluate(Mathf.Clamp01(currentSpeedOnForward / maxSpeedOnForward)) * maxYawRate *
                   volume * Time.deltaTime;
        }
        
        /// <summary>
        /// 获取当前速度下的滚转速率
        /// </summary>
        /// <param name="volume">-1~1</param>
        /// <returns></returns>
        public float GetRollRate(float volume) {
            if (volume >= -1f && volume < 0) {
                return -GetRollRate(-volume);
            }
            volume = Mathf.Clamp01(volume);
            return rollRateCurve.Evaluate(Mathf.Clamp01(currentSpeedOnForward / maxSpeedOnForward)) * maxRollRate *
                   volume * Time.deltaTime;
        }
        
    }
}
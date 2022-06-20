using UnityEngine;

namespace Plane.Movement {
    /// <summary>
    /// 气动力计算，包括计算密度、飞机迎角、飞机侧滑角、升力、阻力、侧滑力
    /// </summary>
    public static class AerodynamicCalculation {
        /// <summary>
        /// 计算空气密度：初始1.29，海拔每升高1000米，密度下降10%，此游戏定义飞机高度不超过1000米
        /// 即密度范围在1.29*90%~1.29之间
        /// y小于等于0时，密度=1.29；
        /// 0小于y小于等于1000时，密度=-0.000129 *（高度）+1.29
        /// y>1000,密度=1.161
        /// </summary>
        /// <param name="altitude">海拔高度</param>
        /// <returns>此高度下的空气密度</returns>
        private static float CalculateAirDensity(float altitude) {
            altitude = Mathf.Clamp(altitude, 0f, 1000f);
            return -0.000129f * altitude + 1.29f;
        }

        /// <summary>
        /// 计算飞机的迎角，
        /// 飞机的迎角是指飞机相对空气速度在飞机垂直截面上的分量与机翼前沿方向的夹角，
        /// 在这里抽象简化成，飞机速度在垂直截面上的分量和飞机机体向前方向的夹角。
        /// </summary>
        /// <param name="planeTransform">飞机的transform属性</param>
        /// <param name="planeVelocity">飞机的rb.velocity，在此函数内部自动计算速度在垂直截面的分量</param>
        /// <returns>飞机迎角，范围-180°~180°</returns>
        public static float CalculateAngleOfAttack(Transform planeTransform, Vector3 planeVelocity) {
            // 飞机速度在飞机垂直平面上的分量
            Vector3 velocityOnVertical = Vector3.ProjectOnPlane(planeVelocity, planeTransform.right);
            // 飞机迎角
            float angleOfAttack = Vector3.SignedAngle(planeTransform.forward, velocityOnVertical, planeTransform.right);
            return angleOfAttack;
        }

        /// <summary>
        /// 计算升力系数
        /// </summary>
        /// <param name="liftCoefficientCurve">标准升力系数曲线，横坐标为0~90°的范围的迎角，纵坐标为0~1的升力系数</param>
        /// <param name="maxLiftCoefficient">最大升力系数，即最高点升力系数的数值</param>
        /// <param name="α0">飞机迎角，范围是-180~180</param>
        /// <param name="α">零升迎角 α0，即飞机在此迎角下升力为0，一般是负数</param>
        /// <returns></returns>
        public static float CalculateLiftCoefficient(AnimationCurve liftCoefficientCurve, float maxLiftCoefficient,
            float α, float α0) {
            α = Mathf.Clamp(α, -180f, 180f);
            float αReal = α - α0;
            float liftCoefficient = 0;
            // α0 ≤ α ≤ 90+α0，通过标准升力系数曲线获取升力系数的值
            if (α >= α0 && α <= 90f + α0) {
                liftCoefficient = liftCoefficientCurve.Evaluate(αReal) * maxLiftCoefficient;
            }

            // 90+α0 ＜ α ≤ 180+α0
            if (α > 90f + α0 && α <= 180f + α0) {
                liftCoefficient =
                    -CalculateLiftCoefficient(liftCoefficientCurve, maxLiftCoefficient, 2f * (90f + α0) - α, α0);
            }

            // α > 180+α0
            if (α > 180f + α0) {
                liftCoefficient = CalculateLiftCoefficient(liftCoefficientCurve, maxLiftCoefficient, α - 180f, α0);
            }

            if (α < α0) {
                liftCoefficient = CalculateLiftCoefficient(liftCoefficientCurve, maxLiftCoefficient, α + 180f, α0);
            }

            return liftCoefficient;
        }

        /// <summary>
        /// 计算升力
        /// </summary>
        /// <param name="planeTransform">飞机transform</param>
        /// <param name="planeVelocity">飞机的rb.velocity</param>
        /// <param name="wingArea">机翼面积</param>
        /// <param name="liftCoefficient">升力系数</param>
        /// <returns></returns>
        public static Vector3 CalculateLift(Transform planeTransform, Vector3 planeVelocity, float wingArea,
            float liftCoefficient) {
            Vector3 planeTransformRight = planeTransform.right;
            // 空气密度
            float airDensity = CalculateAirDensity(planeTransform.position.y);
            // 速度在垂直截面的分量
            Vector3 velocityOnVertical = Vector3.ProjectOnPlane(planeVelocity, planeTransformRight);

            // L=1/2*ρ*V²*S*Cl
            float liftSize = 0.5f * airDensity * velocityOnVertical.sqrMagnitude * wingArea * liftCoefficient;
            // 方向在垂直截面上，垂直于速度方向
            Vector3 liftDirect = Vector3.Cross(velocityOnVertical, planeTransformRight).normalized;
            return liftDirect * liftSize;
        }

        /// <summary>
        /// 计算阻力系数
        /// </summary>
        /// <param name="dragCoefficientCurve">标准阻力系数曲线，横坐标为0~90°的范围的迎角，纵坐标为0~1的升力系数</param>
        /// <param name="maxDragCoefficient">最大升力阻力，即最高点升力系数的数值</param>
        /// <param name="α0">飞机迎角，范围是-180~180</param>
        /// <param name="α">零升迎角 α0，即飞机在此迎角下升力为0，一般是负数</param>
        /// <returns></returns>
        public static float CalculateDragCoefficient(AnimationCurve dragCoefficientCurve, float maxDragCoefficient,
            float α, float α0) {
            α = Mathf.Clamp(α, -180f, 180f);
            float αReal = α - α0;
            float dragCoefficient = 0;
            // α0 ≤ α ≤ 90+α0，通过标准升力系数曲线获取升力系数的值
            if (α >= α0 && α <= 90f + α0) {
                dragCoefficient = dragCoefficientCurve.Evaluate(αReal) * maxDragCoefficient;
            }

            // 90+α0 ＜ α ≤ 180+α0
            if (α > 90f + α0 && α <= 180f + α0) {
                dragCoefficient =
                    CalculateDragCoefficient(dragCoefficientCurve, maxDragCoefficient, 2f * (90f + α0) - α, α0);
            }

            // α > 180+α0
            if (α > 180f + α0) {
                dragCoefficient = CalculateDragCoefficient(dragCoefficientCurve, maxDragCoefficient, α - 180f, α0);
            }

            if (α < α0) {
                dragCoefficient = CalculateDragCoefficient(dragCoefficientCurve, maxDragCoefficient, α + 180f, α0);
            }

            return dragCoefficient;
        }

        /// <summary>
        /// 计算阻力
        /// </summary>
        /// <param name="planeTransform">飞机transform</param>
        /// <param name="planeVelocity">飞机的rb.velocity</param>
        /// <param name="wingArea">机翼面积</param>
        /// <param name="dragCoefficient">升力系数</param>
        /// <returns></returns>
        public static Vector3 CalculateDrag(Transform planeTransform, Vector3 planeVelocity, float wingArea,
            float dragCoefficient) {
            Vector3 planeTransformRight = planeTransform.right;
            // 空气密度
            float airDensity = CalculateAirDensity(planeTransform.position.y);
            // 速度在垂直截面的分量
            Vector3 velocityOnVertical = Vector3.ProjectOnPlane(planeVelocity, planeTransformRight);

            // D=1/2*ρ*V²*S*Cd
            float dragSize = 0.5f * airDensity * velocityOnVertical.sqrMagnitude * wingArea * dragCoefficient;
            // 方向在垂直截面上，与速度方向相反
            Vector3 dragDirect = -velocityOnVertical.normalized;
            return dragDirect * dragSize;
        }

        /// <summary>
        /// 计算飞机的侧滑角
        /// </summary>
        /// <param name="planeTransform">飞机的transform属性</param>
        /// <param name="planeVelocity">飞机的rb.velocity，在此函数内部自动计算速度在水平截面的分量</param>
        /// <returns>飞机侧滑角，范围-180°~180°</returns>
        public static float CalculateAngleOfSideslip(Transform planeTransform, Vector3 planeVelocity) {
            Vector3 planeTransformUp = planeTransform.up;
            // 飞机速度在飞机垂直平面上的分量
            Vector3 velocityOnHorizontal = Vector3.ProjectOnPlane(planeVelocity, planeTransformUp);
            // 飞机侧滑角
            float angleOfSideslip = Vector3.SignedAngle(planeTransform.forward, velocityOnHorizontal, planeTransformUp);
            return angleOfSideslip;
        }

        /// <summary>
        /// 计算侧力系数
        /// </summary>
        /// <param name="sideForceCoefficientCurve">标准侧力系数曲线，横坐标为0~90°的范围的侧滑角，纵坐标为0~1的侧力系数</param>
        /// <param name="maxSideForceCoefficient">最大侧力系数，即最高点侧力系数的数值</param>
        /// <param name="β">侧滑角，范围是-180~180</param>
        /// <returns></returns>
        public static float CalculateSideForceCoefficient(AnimationCurve sideForceCoefficientCurve,
            float maxSideForceCoefficient, float β) {
            float sideForceCoefficient = 0;
            // 0 ≤ β ≤ 90，通过标准侧力系数曲线获取侧力系数的值
            if (β >= 0 && β <= 90f) {
                sideForceCoefficient = sideForceCoefficientCurve.Evaluate(β) * maxSideForceCoefficient;
            }

            // 90 ＜ α ≤ 180
            if (β > 90f && β <= 180f) {
                sideForceCoefficient =
                    -CalculateSideForceCoefficient(sideForceCoefficientCurve, maxSideForceCoefficient, 2f * 90f - β);
            }

            if (β < 0) {
                sideForceCoefficient =
                    CalculateSideForceCoefficient(sideForceCoefficientCurve, maxSideForceCoefficient, β + 180f);
            }

            return sideForceCoefficient;
        }
        
        /// <summary>
        /// 计算侧力
        /// </summary>
        /// <param name="planeTransform">飞机transform</param>
        /// <param name="planeVelocity">飞机的rb.velocity</param>
        /// <param name="tailArea">尾翼面积</param>
        /// <param name="sideForceCoefficient">侧力系数</param>
        /// <returns></returns>
        public static Vector3 CalculateSideForce(Transform planeTransform, Vector3 planeVelocity, float tailArea,
            float sideForceCoefficient) {
            Vector3 planeTransformUp = planeTransform.up;
            // 空气密度
            float airDensity = CalculateAirDensity(planeTransform.position.y);
            // 速度在水平截面的分量
            Vector3 velocityOnHorizontal = Vector3.ProjectOnPlane(planeVelocity, planeTransformUp);

            // S=1/2*ρ*V²*S*Cs
            float sideForceSize = 0.5f * airDensity * velocityOnHorizontal.sqrMagnitude * tailArea * sideForceCoefficient;
            // 方向在水平截面上，垂直于速度方向
            Vector3 sideForceDirect = Vector3.Cross(velocityOnHorizontal, planeTransformUp).normalized;
            return sideForceDirect * sideForceSize;
        }
    }
}
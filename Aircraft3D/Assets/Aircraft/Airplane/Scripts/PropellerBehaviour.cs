using System;
using UnityEngine;

namespace Airplane {
    public class PropellerBehaviour : MonoBehaviour {
        private void Update() {
            Rotate(3600f * Time.deltaTime, transform.forward);
            com.SoundService.instance.Play("engine", 0.5f);
        }

        /// <summary>
        /// 着axis轴旋转angle度
        /// </summary>
        /// <param name="angle"></param>
        /// <param name="axis"></param>
        private void Rotate(float angle, Vector3 axis) {
            Quaternion quaternion = Quaternion.AngleAxis(angle, axis);
            transform.rotation = quaternion * transform.rotation;
        }
    }
}
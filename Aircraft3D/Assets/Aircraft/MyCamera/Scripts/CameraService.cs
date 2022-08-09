using System;
using GameManager;
using ProjectBase.SingletonBase;
using UnityEngine;

namespace MyCamera {
    public class CameraService : Singletonable<CameraService> {
        
        /// <summary>
        /// 摄像机固定在 cameraPosition 视角跟随 target
        /// </summary>
        /// <param name="cameraPosition"></param>
        /// <param name="target"></param>
        /// <param name="duration"></param>
        /// <param name="callback"></param>
        public void SpotTrack(Vector3 cameraPosition, Transform target, float duration, Action callback = null) {
            CameraBehaviour camera = Camera.main.GetComponent<CameraBehaviour>();
            Timer timer = TimerManager.instance.GetTimer();
            timer.Init(() => {
                camera.muzzleObj.SetActive(false);
                camera.ChangeTrackingMode(CameraTrackingMode.Spot, duration, target, cameraPosition);
            }, null, () => {
                camera.muzzleObj.SetActive(true);
                camera.ResumeToPlayer();
                if (callback != null) {
                    callback();
                }
            }, "test", 1, duration);
            timer.Start();
        }
    }
}
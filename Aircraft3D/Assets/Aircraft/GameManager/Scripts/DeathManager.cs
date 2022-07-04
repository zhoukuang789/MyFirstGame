using System;
using Airplane;
using Airplane.Health;
using UnityEngine;

namespace GameManager {
    public class DeathManager : MonoBehaviour {
        private void Awake() {
            PlaneHealthService.GetInstance().AddPlaneDeathEventListener(PlaneOnDeath);
        }

        private void OnDestroy() {
            PlaneHealthService.GetInstance().RemovePlaneDeathEventListener(PlaneOnDeath);
        }

        private void PlaneOnDeath(Airplane.PlaneBehaviour plane) {
            if (plane.camp == PlaneCamp.Enemy) {
                Record.RecordService.GetInstance().AddKillRecord(plane.type);
            }
        }
    }
}
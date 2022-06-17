using System;
using Record;
using UnityEngine;

namespace Plane {
    public class PlaneHealth : MonoBehaviour {
        
        private float health = 100f;
        
        private event Action dieEvent;

        public void AddDieEventListner(Action action) {
            dieEvent += action;
        }

        public void Die() {
            if (dieEvent!= null) dieEvent();
            RecordService.GetInstance().AddKillRecord("Bomber");
            Destroy(this);
            
        }
        
    }
}
using System;
using System.Collections;
using UnityEngine;

namespace Airplane.Bot {
    public class BotDecision : MonoBehaviour {
        
        //---------------------field ----------------------------
        private BotBehaviour bot;

        public Transform currentTarget;

        //--------------------- mono method ---------------------
        private void Awake() {
            bot = GetComponent<BotBehaviour>();
        }

        private void Start() {
            StartCoroutine(MakeDecision());
        }

        private IEnumerator MakeDecision() {
            while (true) {
                //做一次决策
                yield return new WaitForSeconds(0.5f);
            }
            // ReSharper disable once IteratorNeverReturns
        }
    }
}
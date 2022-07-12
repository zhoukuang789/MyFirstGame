using System;
using System.Collections.Generic;
using Airplane.Weapon;
using UnityEngine;

namespace Airplane.Bot {
    public class BomberFireDecision : MonoBehaviour {
        public GameObject bombTargets;

        private List<Transform> bombTargetList;

        private int cursor;

        public Transform currentTarget;

        private PlaneBehaviour plane;

        private void Awake() {
            plane = GetComponent<PlaneBehaviour>();
            bombTargets = GameObject.Find("BombTargets");
            if (bombTargets.transform.childCount != 0) {
                currentTarget = bombTargets.transform.GetChild(0);
            }
        }



        private void Update() {
            if (currentTarget == null) {
                if (bombTargets.transform.childCount != 0) {
                    currentTarget = bombTargets.transform.GetChild(0);
                } else {
                    GameObject.Find("GameManager").GetComponent<GameManager.GameManager>().FailMission1();
                }
                return;
            }
            Vector2 myLocation = new Vector2(transform.position.x, transform.position.z);
            Vector2 targetLocation = new Vector2(currentTarget.position.x, currentTarget.position.z);
            if (Vector2.Distance(myLocation, targetLocation) < 50f) {
                Debug.Log("BOmb");
                PlaneWeaponControllerService.GetInstance().SetPlane(plane).Bomb(currentTarget);
                
            }

        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlaneHealth : MonoBehaviour {
    public float health = 100f;
    public Rigidbody rb;
    public GameObject hitEffect;
    public GameObject deathEffect;
    private float _exploreCount;

    //public PlaneHealth planeHealth;

    public enum MyPart
    {
        leftwing,
        rightWing
    }

    public MyPart part;

    private void Start() {
        _exploreCount = 1;
    }

    private void Update() {
        if (health <= 0 && _exploreCount > 0) {
            _exploreCount--;
            Death();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerBullet") {
            Debug.Log("OnTriggerEnter");
            Debug.Log("对敌人造成伤害");
            Debug.Log("给出集中反馈，包含镜头震动");
            Debug.Log("删除子弹，播放击中特效");
            health -= other.gameObject.GetComponentInParent<Projectile>().damage;
            Camera.main.transform.DOShakePosition(0.8f, 1, 12, 90, false, true);
            var explosion = Instantiate(hitEffect, transform.position, transform.rotation);
            explosion.SetActive(true);
        }
        
    }
    
    public void Death() {
        Debug.Log("飞机死亡");
        var explosion = Instantiate(deathEffect, transform.position, transform.rotation);
        explosion.SetActive(true);
        GetComponent<PlaneMovement>().enabled = false;
        GetComponent<PlaneController>().enabled = false;
        GetComponent<PlaneWeapon>().enabled = false;
        rb.useGravity = false;
        Destroy(gameObject);
    }
}

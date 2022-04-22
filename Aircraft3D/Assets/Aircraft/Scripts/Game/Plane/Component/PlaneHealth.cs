using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlaneHealth : PlaneComponent
{
    public float health;
    public List<PartHealth> partHealths;
    public Rigidbody rb;
    public GameObject hitEffect;
    public GameObject deathEffect;

    public Transform leftWingTransform;
    private bool _isLeftWingSmoke = false;
    public Transform rightWingTransform;
    private bool _isRightWingSmoke = false;

    public GameObject smokeEffect;

    private bool _dead;

    private void Start() {
        health = plane.planeConfig.fight.totalHealth;
        List<PartHealth> partHeathsConfig = plane.planeConfig.fight.partHeaths;
        partHealths = new List<PartHealth>();
        foreach (PartHealth partHeathConfig in partHeathsConfig) {
            PartHealth partHealth = new PartHealth();
            partHealth.health = partHeathConfig.health;
            partHealth.part = partHeathConfig.part;
            partHealth.armor = partHeathConfig.armor;
            partHealths.Add(partHealth);
        }
        _dead = false;
    }
    
    protected override void OnUpdate()
    {
        foreach (PartHealth partHealth in partHealths) {
            if (partHealth.health < 0) {
                if (partHealth.part == PlaneHitCollider.PlaneHitPart.Leftwing && !_isLeftWingSmoke) {
                    GameObject smoke = Instantiate(smokeEffect, leftWingTransform.position, leftWingTransform.rotation);
                    smoke.SetActive(true);
                    _isLeftWingSmoke = true;
                }
            }
        }
    }

    public void ReceiveDamage(float damage, PlaneHitCollider.PlaneHitPart part = PlaneHitCollider.PlaneHitPart.Other)
    {
        if (_dead)
            return;

        

        if (damage>0)
        {
            var hitExp = Instantiate(hitEffect, transform.position, transform.rotation);
            hitExp.SetActive(true);

            var playerPlane = plane.GetComponent<PlayerPlane>();
            if (playerPlane != null)
                Camera.main.transform.DOShakePosition(0.8f, 1, 12, 90, false, true);
        }

        float trueDamage = 0;
        foreach (PartHealth partHealth in partHealths) {
            if (part == partHealth.part) {
                trueDamage = damage - partHealth.armor;
                partHealth.health = partHealth.health - trueDamage;
                Debug.Log(gameObject.name + "收到伤害" + trueDamage + " 部位为" + part);
            }
        }
        
        health -= trueDamage;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        _dead = true;

        Debug.Log(gameObject.name + "死亡");
        var explosion = Instantiate(deathEffect, transform.position, transform.rotation);
        explosion.SetActive(true);

        var playerPlane = plane.GetComponent<PlayerPlane>();
        if (playerPlane != null)
            Camera.main.transform.DOShakePosition(4f, 4, 12, 90, false, true);

        GetComponent<PlaneMovement>().enabled = false;
        GetComponent<PlaneController>().enabled = false;
        GetComponent<PlaneWeapon>().enabled = false;
        rb.useGravity = false;
        Destroy(gameObject);
    }
}

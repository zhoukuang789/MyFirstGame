using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlaneHealth : PlaneComponent
{
    public float health = 100f;
    public Rigidbody rb;
    public GameObject hitEffect;
    public GameObject deathEffect;

    private bool _dead;

    private void Start()
    {
        _dead = false;
    }

    public void ReceiveDamage(int damage, PlaneHitCollider.PlaneHitPart part = PlaneHitCollider.PlaneHitPart.Other)
    {
        if (_dead)
            return;

        Debug.Log(gameObject.name + "收到伤害" + damage + " 部位为" + part);

        if (damage>0)
        {
            var hitExp = Instantiate(hitEffect, transform.position, transform.rotation);
            hitExp.SetActive(true);

            var playerPlane = plane.GetComponent<PlayerPlane>();
            if (playerPlane != null)
                Camera.main.transform.DOShakePosition(0.8f, 1, 12, 90, false, true);
        }

        health -= damage;
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

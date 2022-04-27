using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlaneHealth : PlaneComponent
{
    public float health;
    public List<PartHealth> partHealths;
    public GameObject hitEffect;
    public GameObject deathEffect;


    public Transform leftWingTransform;
    protected GameObject _leftSmoke;
    protected bool _isLeftWingSmoke = false;
    public Transform rightWingTransform;
    protected GameObject _rightSmoke;
    protected bool _isRightWingSmoke = false;
    public Transform bodyTransform;
    private GameObject _bodySmoke;
    protected bool _isBodySmoke = false;

    public GameObject smokeEffect;

    protected bool _dead;

    private void Start()
    {
        health = plane.planeConfig.fight.totalHealth;
        List<PartHealth> partHeathsConfig = plane.planeConfig.fight.partHeaths;
        partHealths = new List<PartHealth>();
        foreach (PartHealth partHeathConfig in partHeathsConfig)
        {
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
        foreach (PartHealth partHealth in partHealths)
        {
            if (partHealth.health <= 0)
            {
                if (partHealth.part == PlaneHitCollider.PlaneHitPart.LeftWing && !_isLeftWingSmoke)
                {
                    _leftSmoke = Instantiate(smokeEffect, leftWingTransform.position, leftWingTransform.rotation * Quaternion.AngleAxis(90, -leftWingTransform.right));
                    _leftSmoke.SetActive(true);
                    _isLeftWingSmoke = true;
                }
                if (partHealth.part == PlaneHitCollider.PlaneHitPart.LeftWing && _isLeftWingSmoke && _leftSmoke != null)
                {
                    _leftSmoke.transform.position = leftWingTransform.position;
                    _leftSmoke.transform.rotation =
                        leftWingTransform.rotation * Quaternion.AngleAxis(90, -leftWingTransform.right);
                }

                if (partHealth.part == PlaneHitCollider.PlaneHitPart.RightWing && !_isRightWingSmoke)
                {
                    _rightSmoke = Instantiate(smokeEffect, rightWingTransform.position, rightWingTransform.rotation * Quaternion.AngleAxis(90, -rightWingTransform.right));
                    _rightSmoke.SetActive(true);
                    _isRightWingSmoke = true;
                }
                if (partHealth.part == PlaneHitCollider.PlaneHitPart.RightWing && _isRightWingSmoke && _rightSmoke != null)
                {
                    _rightSmoke.transform.position = rightWingTransform.position;
                    _rightSmoke.transform.rotation =
                        rightWingTransform.rotation * Quaternion.AngleAxis(90, -rightWingTransform.right);
                }

                if (partHealth.part == PlaneHitCollider.PlaneHitPart.Main && !_isBodySmoke)
                {
                    _bodySmoke = Instantiate(smokeEffect, bodyTransform.position, bodyTransform.rotation * Quaternion.AngleAxis(180, -bodyTransform.right));
                    _bodySmoke.SetActive(true);
                    _isBodySmoke = true;
                }
                if (partHealth.part == PlaneHitCollider.PlaneHitPart.Main && _isBodySmoke && _bodySmoke != null)
                {
                    _bodySmoke.transform.position = bodyTransform.position;
                    _bodySmoke.transform.rotation = bodyTransform.rotation * bodyTransform.rotation *
                                                    Quaternion.AngleAxis(180, -bodyTransform.right);
                }
            }
        }
    }

    public void ReceiveDamage(float damage, PlaneHitCollider.PlaneHitPart part = PlaneHitCollider.PlaneHitPart.Other)
    {
        if (_dead)
            return;



        if (damage > 0)
        {
            if (hitEffect != null)
            {
                var hitExp = Instantiate(hitEffect, transform.position, transform.rotation);
                hitExp.SetActive(true);
            }

            var playerPlane = plane.GetComponent<PlayerPlane>();
            if (playerPlane != null)
                Camera.main.transform.DOShakePosition(1.4f, 1.2f, 12, 90, false, true);
        }

        float trueDamage = 0;
        foreach (PartHealth partHealth in partHealths)
        {
            if (part == partHealth.part)
            {
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

    public virtual void Die()
    {
        _dead = true;

        DieFeedBack();

        GetComponent<PlaneMovement>().enabled = false;
        GetComponent<PlaneController>().enabled = false;
        GetComponent<PlaneWeapon>().enabled = false;
        Destroy(gameObject);
    }

    public virtual void DieFeedBack()
    {
        Debug.Log(gameObject.name + "死亡");
        var explosion = Instantiate(deathEffect, transform.position, transform.rotation);
        explosion.SetActive(true);

        var playerPlane = plane.GetComponent<PlayerPlane>();
        if (playerPlane != null)
            Camera.main.transform.DOShakePosition(5f, 5, 12, 90, false, true);
    }
}

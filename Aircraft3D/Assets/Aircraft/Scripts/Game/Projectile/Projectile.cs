using System;
using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
    public float speed = 150f;
    public float damage = 20f;
    public float maxLifetime = 5;

    
    float _dieTimestamp;

    void Start()
    {
        _dieTimestamp = Time.time + maxLifetime;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
        if (Time.time > _dieTimestamp)
        {
            DieSilent();
            return;
        }
    }

    void DieSilent()
    {
        Destroy(this.gameObject);
    }

    void DieUnsilent()
    {
        //feedback
        Destroy(this.gameObject);
    }
}

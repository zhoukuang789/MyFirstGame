using System;
using UnityEngine;
using System.Collections;

public class ProjectileBehaviour : MonoBehaviour
{
    public GameObject explosionPrefab;
    public float maxLifetime = 5;
    public float speed = 150f;
    public Rigidbody rb;

    float _dieTimestamp;

    void Start()
    {
        _dieTimestamp = Time.time + maxLifetime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.position += transform.forward * Time.deltaTime * speed;
        //transform.Translate(transform.forward * Time.deltaTime * speed);
        rb.MovePosition(transform.position + transform.forward * Time.fixedDeltaTime * speed * 1f);

        if (Time.time > _dieTimestamp)
        {
            DieSilent();
            return;
        }
    }

    protected void DieSilent()
    {
        Destroy(this.gameObject);
    }

    protected void DieUnsilent()
    {
        //feedback
        var explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
        explosion.SetActive(true);

        Destroy(this.gameObject);
    }
}

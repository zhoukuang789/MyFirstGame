using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public Rigidbody rb;

    [Space]
    public float speed = 100f;

    public Vector3 parentSpeed;

    public float maxLifetime = 5;

    protected float _dieTimestamp;

    protected void Start()
    {
        _dieTimestamp = Time.time + maxLifetime;
    }

    protected virtual void FixedUpdate()
    {
        //transform.position += transform.forward * Time.deltaTime * speed;
        //transform.Translate(transform.forward * Time.deltaTime * speed);
        rb.MovePosition(transform.position + (transform.forward * speed + parentSpeed) * Time.fixedDeltaTime);

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
}
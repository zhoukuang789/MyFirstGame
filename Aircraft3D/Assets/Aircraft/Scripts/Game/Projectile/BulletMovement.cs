using UnityEngine;

public class BulletMovement : MonoBehaviour {
    public Rigidbody rb;

    [Space]
    public float speed = 150f;
    
    public float maxLifetime = 5;

    float _dieTimestamp;

    void Start() {
        _dieTimestamp = Time.time + maxLifetime;
    }

    // Update is called once per frame
    void FixedUpdate() {
        //transform.position += transform.forward * Time.deltaTime * speed;
        //transform.Translate(transform.forward * Time.deltaTime * speed);
        rb.MovePosition(transform.position + transform.forward * Time.fixedDeltaTime * speed * 1f);

        if (Time.time > _dieTimestamp) {
            DieSilent();
            return;
        }
    }

    protected void DieSilent() {
        Destroy(this.gameObject);
    }
}
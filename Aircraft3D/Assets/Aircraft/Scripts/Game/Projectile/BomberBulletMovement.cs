using UnityEngine;

public class BomberBulletMovement : BulletMovement
{
    public Transform target;

    protected override void FixedUpdate()
    {
        var speedVec = target.position - transform.position;
        rb.MovePosition(transform.position + (speedVec.normalized * speed) * Time.fixedDeltaTime);

        if (Time.time > _dieTimestamp)
        {
            DieSilent();
            return;
        }
    }
}
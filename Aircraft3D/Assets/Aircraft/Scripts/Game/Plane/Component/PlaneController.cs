using System;
using UnityEngine;

public class PlaneController : PlaneComponent
{
    public Rigidbody rb;

    protected override void PostStart()
    {

    }

    protected override void OnUpdate()
    {
        ReadPlaneInput();
    }

    protected virtual void ReadPlaneInput()
    {
    }

    /**
     * 获取飞机实时速度
     */
    public float MySpeed
    {
        get { return rb.velocity.magnitude; }
    }

    /**
     * 俯仰操作
     */
    protected void DoPitch(float angle)
    {
        Rotate(angle, transform.right);
    }

    /**
     * 滚转操作
     */
    protected void DoRoll(float angle)
    {
        Rotate(angle, transform.forward);
    }

    /**
     * 偏航操作
     */
    protected void DoYaw(float angle)
    {
        Rotate(angle, -transform.up);
    }

    /**
     * 沿着axis旋转angle度
     */
    void Rotate(float angle, Vector3 axis)
    {
        Quaternion quaternion = Quaternion.AngleAxis(angle, axis);
        transform.rotation = quaternion * transform.rotation;
    }
}
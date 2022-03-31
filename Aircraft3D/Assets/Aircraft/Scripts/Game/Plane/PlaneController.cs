using System;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    public Rigidbody rb;
    private PlaneConfig _planeConfig;
    public PlaneEngine engine;

    private void Start()
    {
        _planeConfig = ConfigService.instance.planesConfig.PlayerPlaneConfig;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.P))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPaused = !UnityEditor.EditorApplication.isPaused;
#endif
        }
        if (Input.GetKey(KeyCode.Y))
        {
            CameraShake.instance.ShakeOnHit(1);
        }
        // 按住W增大发动机功率，直到功率<=最大功率
        if (Input.GetKey(KeyCode.W))
        {
            engine.AddPower();
            Debug.Log(rb.velocity);
        }

        // 按住W减小发动机功率，直到功率>=0
        if (Input.GetKey(KeyCode.S))
        {
            engine.ReducePower();
        }

        // 按下空格键或鼠标Y轴增大，飞机向上俯仰
        if (Input.GetKey(KeyCode.Space) || Input.GetAxis("Mouse Y") > 0)
        {
            DoPitch(-30 * Time.deltaTime);
        }

        // 按下Alt或鼠标Y轴减小，飞机向下俯仰
        if (Input.GetKey(KeyCode.AltGr) || Input.GetAxis("Mouse Y") < 0)
        {
            DoPitch(30 * Time.deltaTime);
        }

        // 鼠标X轴增大，飞机向右滚转
        if (Input.GetAxis("Mouse X") > 0)
        {
            DoRoll(-30 * Time.deltaTime);
        }

        // 鼠标X轴减小，飞机向左滚转
        if (Input.GetAxis("Mouse X") < 0)
        {
            DoRoll(30 * Time.deltaTime);
        }

        // 按下D键，飞机右偏航
        if (Input.GetKey(KeyCode.D))
        {
            DoYaw(30 * Time.deltaTime);
        }

        // 按下A键，飞机左偏航
        if (Input.GetKey(KeyCode.A))
        {
            DoYaw(-30 * Time.deltaTime);
        }
    }

    private void FixedUpdate()
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
    private void DoPitch(float angle)
    {
        Rotate(angle, transform.right);
    }

    /**
     * 滚转操作
     */
    private void DoRoll(float angle)
    {
        Rotate(angle, transform.forward);
    }

    /**
     * 偏航操作
     */
    private void DoYaw(float angle)
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
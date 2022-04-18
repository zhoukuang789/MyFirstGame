using System;
using UnityEngine;

public class PlayerPlaneController : PlaneController
{
    protected override void ReadPlaneInput()
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
            plane.engine.AddPower();
        }

        // 按住W减小发动机功率，直到功率>=0
        if (Input.GetKey(KeyCode.S))
        {
            plane.engine.ReducePower();
        }

        // 按下空格键或鼠标Y轴增大，飞机向上俯仰
        if (Input.GetKey(KeyCode.UpArrow))
        {
            DoPitch(-plane.planeConfig.movement.pitchScaleFactor * Time.deltaTime);
        }

        // 按下Alt或鼠标Y轴减小，飞机向下俯仰
        if (Input.GetKey(KeyCode.DownArrow))
        {
            DoPitch(plane.planeConfig.movement.pitchScaleFactor * Time.deltaTime);
        }

        // 鼠标X轴增大，飞机向右滚转
        if (Input.GetKey(KeyCode.RightArrow))
        {
            DoRoll(-plane.planeConfig.movement.rollScaleFactor * Time.deltaTime);
        }

        // 鼠标X轴减小，飞机向左滚转
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            DoRoll(plane.planeConfig.movement.rollScaleFactor * Time.deltaTime);
        }

        // 按下D键，飞机右偏航
        if (Input.GetKey(KeyCode.D))
        {
            DoYaw(-plane.planeConfig.movement.yawScaleFactor * Time.deltaTime);
            // rb.AddForce(transform.right * 500f);
        }

        // 按下A键，飞机左偏航
        if (Input.GetKey(KeyCode.A))
        {
            DoYaw(plane.planeConfig.movement.yawScaleFactor * Time.deltaTime);
            // rb.AddForce(transform.right * -500f);
        }
    }
}
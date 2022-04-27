using UnityEngine;
using System.Collections;

public class BotAiMoveSolution
{
    /// <summary>
    /// GetMoveYawPitchRollLevel
    /// </summary>
    /// <param name="self"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public static Vector3 GetMoveYawPitchRollLevel(Transform self, Transform target)
    {
        var res = new Vector3(0, 0, 0);
        if (target == null)
            return res;

        var cfg = ConfigService.instance.AiConfig;

        var toTargetVec = target.position - self.position;
        var dist = toTargetVec.magnitude;
        var currentForward = self.forward;
        var angleDelta = Vector3.Angle(toTargetVec, currentForward);

        //yaw
        var delta_horizontal = new Vector3(toTargetVec.x, 0, toTargetVec.z);
        var currentForward_horizontal = new Vector3(currentForward.x, 0, currentForward.z);
        var yawRes = Vector3.Cross(currentForward_horizontal, delta_horizontal.normalized);
        //Debug.Log("yaw " + yawRes);
        res.x = -yawRes.y;

        //pitch
        //var projectedHorizontalPlanePos = new Vector3(target.position.x, self.position.y, target.position.z);
        //var currentForward_vertical = projectedHorizontalPlanePos - self.position;
        //var pitchRes = Vector3.Cross(currentForward_vertical.normalized, toTargetVec.normalized);
        //Debug.Log("pitch " + pitchRes);
        //res.y = pitchRes.x;
        var angle1 = Vector3.Angle(toTargetVec, new Vector3(toTargetVec.x, 0, toTargetVec.z));
        var angle2 = Vector3.Angle(currentForward, new Vector3(currentForward.x, 0, currentForward.z));
        //Debug.Log("angle1 " + angle1);
        //Debug.Log("angle2 " + angle2);

        float deltaAngle = 0;
        if (toTargetVec.y >= 0 && currentForward.y >= 0)
        {
            //Debug.Log("m1");
            deltaAngle = -angle1 + angle2;
        }
        else if (toTargetVec.y <= 0 && currentForward.y <= 0)
        {
            //Debug.Log("m2");
            deltaAngle = angle1 - angle2;
        }
        else if (toTargetVec.y >= 0 && currentForward.y <= 0)
        {
            //Debug.Log("m3");
            deltaAngle = -angle1 - angle2;
        }
        else if (toTargetVec.y <= 0 && currentForward.y >= 0)
        {
            //Debug.Log("m4");
            deltaAngle = angle1 + angle2;
        }
        //Debug.Log("deltaAngle " + deltaAngle);

        res.y = Mathf.Sin(Mathf.Deg2Rad * deltaAngle);
       //Debug.Log(res.y);
        return res;
    }
}

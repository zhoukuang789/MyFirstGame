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
        var delta_horizontal = new Vector3(toTargetVec.x, 0, toTargetVec.z);
        var currentForward_horizontal = new Vector3(currentForward.x, 0, currentForward.z);
        var yawRes = Vector3.Cross(currentForward_horizontal, delta_horizontal.normalized);

        res.x = -yawRes.y;
        return res;
    }
}

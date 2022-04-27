using UnityEngine;
using System.Collections;

public class BotAiWeaponSolution
{

    public static bool ShouldOpenFire(Transform self, Transform target)
    {
        if (target == null)
        {
            return false;
        }

        var cfg = ConfigService.instance.AiConfig;

        var toTargetVec = target.position - self.position;
        var dist = toTargetVec.magnitude;
        if (dist > cfg.openFireRangeMax)
        {
            return false;
        }
        var forwardVec = self.forward;

        if (Vector3.Angle(toTargetVec, forwardVec) < cfg.openFireAngleMax)
        {
            return true;
        }

        return false;
    }
}

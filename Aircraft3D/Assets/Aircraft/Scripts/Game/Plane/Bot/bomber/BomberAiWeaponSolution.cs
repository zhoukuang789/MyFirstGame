using UnityEngine;
using System.Collections;

public class BomberAiWeaponSolution
{
    public static bool ShouldOpenFire(Transform self, Transform target)
    {
        if (target == null)
            return false;

        var cfg = ConfigService.instance.AiConfig;

        var toTargetVec = target.position - self.position;
        toTargetVec.y = 0;

        var dist = toTargetVec.magnitude;
        if (dist > cfg.bomberReleaseBombHorizontalDistance)
            return false;

        return true;
    }
}

using UnityEngine;
using com;

public class BomberAi : BotAi
{
    protected bool isReturn;

    public Transform backTarget;

    protected override void Start()
    {
        _aiCondition_fire = AiConditionFire.No;

        decisionCount = 0;

        _tickTimer = TickTime * Random.value;

        isReturn = false;
    }

    protected override void MakeDecision()
    {
        //Debug.Log(gameObject.name + " 决定: " + decisionCount);
        if (!isReturn)
        {
            MakeFireDecision();
            //if no bomb to release, turn back
            var weapon = plane.weapon as BomberWeapon;
            var hasBombsLeft = weapon.HasBombsLeft();
            if (!hasBombsLeft)
            {
                Debug.Log("Return!!");
                isReturn = true;
                target = backTarget;
            }
        }

        MakeMoveDecision();
        decisionCount++;
    }

    protected override void MakeFireDecision()
    {
        bool shouldOpenFire = BomberAiWeaponSolution.ShouldOpenFire(this.transform, target);
        _aiCondition_fire = shouldOpenFire ? AiConditionFire.Yes : AiConditionFire.No;
    }

    protected override void MakeMoveDecision()
    {
        _ratio_yawPitchRoll = BomberAiMoveSolution.GetMoveYawPitchRollLevel(this.transform, target);
        AiCondition2Ratio(ref _ratio_yawPitchRoll);
        //Debug.Log("_ratio_yawPitchRoll-> " + _ratio_yawPitchRoll);
    }
}
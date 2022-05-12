using UnityEngine;
using com;

public class BotAi : Ticker
{
    public BotPlane plane;

    public Transform target;

    protected int decisionCount;

    public enum AiConditionFire
    {
        No,
        Yes,
    }

    protected AiConditionFire _aiCondition_fire;
    protected Vector3 _ratio_yawPitchRoll;

    protected virtual void Start()
    {
        _aiCondition_fire = AiConditionFire.No;

        decisionCount = 0;

        _tickTimer = TickTime * Random.value;

        target = ReferenceService.instance.playerPlane.transform;
    }

    protected override void Tick()
    {
        MakeDecision();
    }

    protected void AiCondition2Ratio(ref Vector3 aiCondition)
    {
        RefineRatio(ref aiCondition.x);
        RefineRatio(ref aiCondition.y);
        RefineRatio(ref aiCondition.z);
    }

    void RefineRatio(ref float v)
    {
        var deltaValue = 0.4f;
        var threshold = 0.35f;
        var res = v * 1.5f;
        if (res > threshold)
        {
            res += deltaValue;
        }
        else if (res < -threshold)
        {
            res += deltaValue;
        }

        res = Mathf.Clamp(res, -1f, 1f);
    }

    protected override void Update()
    {
        plane.botController.AiControlYaw(_ratio_yawPitchRoll.x);
        plane.botController.AiControlPitch(_ratio_yawPitchRoll.y);
        plane.botController.AiControlRoll(_ratio_yawPitchRoll.z);

        if (_aiCondition_fire == AiConditionFire.Yes)
            plane.weapon.TryFire();

        base.Update();
    }

    protected virtual void MakeFireDecision()
    {
        bool shouldOpenFire = BotAiWeaponSolution.ShouldOpenFire(this.transform, target);
        //Debug.Log("OpenFire-> " + shouldOpenFire);
        _aiCondition_fire = shouldOpenFire ? AiConditionFire.Yes : AiConditionFire.No;
    }

    protected virtual void MakeDecision()
    {
        //Debug.Log(gameObject.name + " 决定: " + decisionCount);
        MakeFireDecision();
        MakeMoveDecision();
        decisionCount++;
    }

    protected virtual void MakeMoveDecision()
    {
        _ratio_yawPitchRoll = BotAiMoveSolution.GetMoveYawPitchRollLevel(this.transform, target);
        AiCondition2Ratio(ref _ratio_yawPitchRoll);
        //Debug.Log("_ratio_yawPitchRoll-> " + _ratio_yawPitchRoll);
    }
}
using UnityEngine;
using com;

public class BotAi : Ticker
{
    public BotPlane plane;

    private Transform _target;

    private int decisionCount;

    public enum AiConditionFire
    {
        No,
        Yes,
    }

    private AiConditionFire _aiCondition_fire;
    private Vector3 _ratio_yawPitchRoll;

    private void Start()
    {
        _target = ReferenceService.instance.playerPlane.transform;

        _aiCondition_fire = AiConditionFire.No;

        decisionCount = 0;

        _tickTimer = TickTime*Random.value;
    }

    protected override void Tick()
    {
        MakeDecision();
    }

    void AiCondition2Ratio(ref Vector3 aiCondition)
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

    void MakeFireDecision()
    {
        bool shouldOpenFire = BotAiWeaponSolution.ShouldOpenFire(this.transform, _target);
        //Debug.Log("OpenFire-> " + shouldOpenFire);
        _aiCondition_fire = shouldOpenFire ? AiConditionFire.Yes : AiConditionFire.No;
    }

    void MakeDecision()
    {
        //Debug.Log(gameObject.name + " 决定: " + decisionCount);
        MakeFireDecision();
        MakeMoveDecision();
        decisionCount++;
    }

    void MakeMoveDecision()
    {
        _ratio_yawPitchRoll = BotAiMoveSolution.GetMoveYawPitchRollLevel(this.transform, _target);
        AiCondition2Ratio(ref _ratio_yawPitchRoll);
        //Debug.Log("_ratio_yawPitchRoll-> " + _ratio_yawPitchRoll);
    }
}
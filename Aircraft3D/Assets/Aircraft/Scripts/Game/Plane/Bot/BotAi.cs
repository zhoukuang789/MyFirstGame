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
    private Vector3 _aiCondition_yawPitchRoll;

    private void Start()
    {
        _target = ReferenceService.instance.playerPlane.transform;

        _aiCondition_fire = AiConditionFire.No;

        decisionCount = 0;
    }

    protected override void Tick()
    {
        MakeDecision();
    }

    protected override void Update()
    {
        plane.botController.AiControlYaw(_aiCondition_yawPitchRoll.x);
        plane.botController.AiControlPitch(_aiCondition_yawPitchRoll.y);
        plane.botController.AiControlRoll(_aiCondition_yawPitchRoll.z);

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
        Debug.Log(gameObject.name + " 决定: " + decisionCount);
        MakeFireDecision();
        MakeMoveDecision();
        decisionCount++;
    }

    void MakeMoveDecision()
    {
        _aiCondition_yawPitchRoll = BotAiMoveSolution.GetMoveYawPitchRollLevel(this.transform, _target);
        Debug.Log("_aiCondition_yawPitchRoll-> " + _aiCondition_yawPitchRoll);
    }
}
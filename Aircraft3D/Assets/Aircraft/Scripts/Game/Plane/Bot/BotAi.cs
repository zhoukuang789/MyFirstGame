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
    private BotPlaneController.AIControlLevel _aiCondition_pitch;
    private BotPlaneController.AIControlLevel _aiCondition_yaw;
    private BotPlaneController.AIControlLevel _aiCondition_roll;


    private void Start()
    {
        _target = ReferenceService.instance.playerPlane.transform;

        _aiCondition_fire = AiConditionFire.No;
        _aiCondition_pitch = BotPlaneController.AIControlLevel.None;
        _aiCondition_yaw = BotPlaneController.AIControlLevel.None;
        _aiCondition_roll = BotPlaneController.AIControlLevel.None;

        decisionCount = 0;
    }

    protected override void Tick()
    {
        MakeDecision();
    }

    protected override void Update()
    {
        plane.botController.AiControlYaw(_aiCondition_yaw);

        if (_aiCondition_fire == AiConditionFire.Yes)
        {
            plane.weapon.TryFire();
        }


        base.Update();
    }

    void MakeDecision()
    {
        var delta = _target.position - transform.position;

        var currentForward = transform.forward;

        var delta_horizontal = new Vector3(delta.x, 0, delta.z);
        var currentForward_horizontal = new Vector3(currentForward.x, 0, currentForward.z);

        var yawRes = Vector3.Cross(currentForward_horizontal, delta_horizontal.normalized);

        if (yawRes.y > 0)
        {
            if (yawRes.y > 0.5f)
            {
                _aiCondition_yaw = BotPlaneController.AIControlLevel.Negative_100;
            }
            else
            {
                _aiCondition_yaw = BotPlaneController.AIControlLevel.Negative_50;
            }
        }
        else
        {
            if (yawRes.y < -0.5f)
            {
                _aiCondition_yaw = BotPlaneController.AIControlLevel.Positive_100;
            }
            else
            {
                _aiCondition_yaw = BotPlaneController.AIControlLevel.Positive_50;
            }
        }

        decisionCount++;
        Debug.Log(gameObject.name + " 决策:" + decisionCount);
        Debug.Log("yaw->" + _aiCondition_yaw);

        bool shouldOpenFire = BotAiWeaponSolution.ShouldOpenFire(this.transform, _target.position);
        Debug.Log("shouldOpenFire->" + shouldOpenFire);
        if (shouldOpenFire)
        {
            _aiCondition_fire = AiConditionFire.Yes;
        }
        else
        {
            _aiCondition_fire = AiConditionFire.No;
        }
    }
}

using UnityEngine;

public class PlaneMovement : PlaneComponent
{
    public Rigidbody rb;

    private Vector3 _combinedForce;
    private float _airDensity;
    private float _aoa;
    private Vector3 _lift;
    private Vector3 _drag;
    private Vector3 _thrust;
    public float liftMagnitude;
    public float dragMagnitude;
    public float thrustMagnitude;

    public LineRenderer lr_velo;
    public LineRenderer lr_drag;
    public LineRenderer lr_lift;
    public LineRenderer lr_thrust;
    public float lrLengthRatio = 0.4f;

    protected override void PostStart()
    {
        // 设置刚体质量
        rb.mass = plane.planeConfig.movement.mass;
        // 设置刚体初始速度
        rb.AddForce(transform.forward * plane.planeConfig.movement.initThrust);
    }

    protected override void OnUpdate()
    {
        // 1.空气密度
        _airDensity = AirMechanismAlgorithm.GetAirDensity(transform.position.y);

        // 飞机速度在垂直截面上的分量
        Vector3 velocityYZ = Vector3.ProjectOnPlane(rb.velocity, transform.right);

        // 计算迎角
        _aoa = AirMechanismAlgorithm.GetAoa(velocityYZ, transform.up, transform.forward);

        //升力
        _lift = AirMechanismAlgorithm.GetLift(_airDensity, velocityYZ, plane.planeConfig, _aoa, transform.right);
        liftMagnitude = _lift.magnitude;

        // 阻力
        _drag = AirMechanismAlgorithm.GetDrag(_airDensity, velocityYZ, plane.planeConfig, _aoa);
        dragMagnitude = _drag.magnitude;
        //推力
        _thrust = AirMechanismAlgorithm.GetThrust(rb.velocity, transform.forward, plane.engine.power);
        thrustMagnitude = _thrust.magnitude;

        // 侧力
        // 侧滑角：速度在XZ平面的投影与机头方向的夹角
        Vector3 velocityXZ = Vector3.ProjectOnPlane(rb.velocity, transform.up);
        float sideAngle = AirMechanismAlgorithm.GetSideAngle(velocityXZ, transform.right, transform.forward);
        Vector3 sideForce = transform.right * 0.5f * _airDensity * velocityXZ.sqrMagnitude * sideAngle;

        _combinedForce = _lift + _thrust + _drag + sideForce;
        //Debug.Log(gameObject.name);
        //Debug.Log(_lift);
        //Debug.Log(_thrust);
        //Debug.Log(_drag);
        //Debug.Log(sideForce);
        //Debug.Log(_combinedForce);
        if (lr_velo != null && lr_velo.gameObject.activeInHierarchy)
        {
            var pos = transform.position;
            lr_velo.SetPosition(0, pos);
            lr_velo.SetPosition(1, pos + rb.velocity * lrLengthRatio * 10);

            lr_drag.SetPosition(0, pos);
            lr_drag.SetPosition(1, pos + _drag * lrLengthRatio);

            lr_lift.SetPosition(0, pos);
            lr_lift.SetPosition(1, pos + _lift * lrLengthRatio);

            lr_thrust.SetPosition(0, pos);
            lr_thrust.SetPosition(1, pos + _thrust * lrLengthRatio);
        }

        rb.AddForce(_combinedForce * Time.fixedDeltaTime);
    }
}
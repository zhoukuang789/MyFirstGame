using UnityEngine;

public class PlaneMovement : MonoBehaviour
{
    public Rigidbody rb;
    public PlaneEngine engine;

    private PlaneConfig _planeConfig;
    private Vector3 _combinedForce;
    private float _airDensity;
    private float _aoa;
    private Vector3 _lift;
    private Vector3 _drag;
    private Vector3 _thrust;

    public LineRenderer lr_velo;
    public LineRenderer lr_drag;
    public LineRenderer lr_lift;
    public LineRenderer lr_thrust;
    public float lrLengthRatio = 0.4f;
    // Start is called before the first frame update
    void Start()
    {
        // 获取飞机配置
        _planeConfig = ConfigService.instance.planesConfig.PlayerPlaneConfig;
        // 设置刚体质量
        rb.mass = _planeConfig.movement.mass;
        // 设置刚体初始速度
        rb.AddForce(transform.forward * _planeConfig.movement.initThrust);
    }

    private void FixedUpdate()
    {
        // 1.空气密度
        _airDensity = AirMechanismAlgorithm.GetAirDensity(transform.position.y);

        // 飞机速度在垂直截面上的分量
        Vector3 velocityYZ = Vector3.ProjectOnPlane(rb.velocity, transform.right);

        // 计算迎角
        _aoa = AirMechanismAlgorithm.GetAoa(velocityYZ, transform.up, transform.forward);

        //升力
        _lift = AirMechanismAlgorithm.GetLift(_airDensity, velocityYZ, _planeConfig, _aoa, transform.right);

        // 阻力
        _drag = AirMechanismAlgorithm.GetDrag(_airDensity, velocityYZ, _planeConfig, _aoa);

        //推力
        _thrust = AirMechanismAlgorithm.GetThrust(velocityYZ, transform.forward, engine.power);

        _combinedForce = _lift + _thrust + _drag;
        var pos = transform.position;
        lr_velo.SetPosition(0, pos);
        lr_velo.SetPosition(1, pos + rb.velocity * lrLengthRatio * 10);

        lr_drag.SetPosition(0, pos);
        lr_drag.SetPosition(1, pos + _drag * lrLengthRatio);

        lr_lift.SetPosition(0, pos);
        lr_lift.SetPosition(1, pos + _lift * lrLengthRatio);

        lr_thrust.SetPosition(0, pos);
        lr_thrust.SetPosition(1, pos + _thrust * lrLengthRatio);

        rb.AddForce(_combinedForce * Time.fixedDeltaTime);
    }
}
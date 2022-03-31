using UnityEngine;

public class PlaneMovement : MonoBehaviour
{
    public Rigidbody rb;
    public PlaneEngine engine;

    private PlaneConfig _planeConfig;
    private Vector3 _combinedForce;
    private float _airDensity;
    private Vector3 _lift;
    private Vector3 _drag;
    private Vector3 _thrust;

    // Start is called before the first frame update
    void Start()
    {
        // 获取飞机配置
        _planeConfig = ConfigService.instance.planesConfig.PlayerPlaneConfig;
        // 设置刚体质量
        rb.mass = _planeConfig.movement.mass;
        // 设置刚体初始速度
        rb.velocity = transform.forward * _planeConfig.movement.initialSpeed;
    }

    private void FixedUpdate()
    {
        // 1.空气密度
        _airDensity = AirMechanismAlgorithm.GetAirDensity(transform.position.y);
        // 2.飞机速度大小
        float currentSpeed = rb.velocity.magnitude;
        var relativeForwardSpeed = Vector3.Project(rb.velocity, transform.forward);

        // 3.飞机迎角
        float attackOfAngle = 90;
        var attackOfAngleProject = Vector3.ProjectOnPlane(rb.velocity, transform.right);
        if (attackOfAngleProject != Vector3.zero)
        {
            attackOfAngle = Vector3.Angle(transform.forward, attackOfAngleProject);
        }

        //升力
        _lift = AirMechanismAlgorithm.GetLift(_airDensity, attackOfAngle, rb.velocity, transform.right, transform.forward, _planeConfig);

        // 阻力
        _drag = AirMechanismAlgorithm.GetDrag(_airDensity, rb.velocity, attackOfAngle, transform.forward, _planeConfig);

        //推力
        _thrust = AirMechanismAlgorithm.GetThrust(rb.velocity, transform.forward, engine.power);

        _combinedForce = _lift + _thrust + _drag;
        //1kg 1000N
        rb.AddForce(1000 * _combinedForce * Time.fixedDeltaTime);
    }
}
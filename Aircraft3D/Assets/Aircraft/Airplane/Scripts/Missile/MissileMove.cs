using UnityEngine;
using com;

public class MissileMove : Ticker
{
    public float angularSpeed = 10;
    public float acc = 10;
    public float speedMax = 4;
    private Transform _target;
    public float startSpeed = 0;
    private Vector3 _primeDir;
    public float nonRotateTime = 0.25f;
    private float _rotateTimer;
    public float stopRotateTime = 0;

    private Vector3 _dropDir;
    private bool _drop = false;
    public float dropDec = 2f;
    public float dropStartSpeed = 2f;
    private float _dropSpeed;
    public float lifeTime = 9;
    private float _lifeTimer;

    public RotateAlignMove rotateAlignMove;
    public float Speed = 3;
    public Vector3 dir { get; protected set; }

    public void Init(Vector3 pDir, Vector3 dropDir, Transform target)
    {
        _target = target;
        _primeDir = pDir;
        _dropDir = dropDir;
        Speed = startSpeed;
        _rotateTimer = 0;
        _lifeTimer = lifeTime;
        _drop = true;
        _dropSpeed = dropStartSpeed;
    }

    void Translate(Vector3 d)
    {
        rotateAlignMove?.Rotate(d);
        transform.position += d * Speed * GameTime.deltaTime;
    }

    protected override void Tick()
    {
        _lifeTimer -= com.GameTime.deltaTime;
        if (_lifeTimer < 0)
        {
            Destroy(this.gameObject);
            return;
        }

        if (Speed < speedMax)
            Speed += acc * com.GameTime.deltaTime;
        else
            Speed = speedMax;

        _rotateTimer += com.GameTime.deltaTime;
        if (nonRotateTime > 0 && _rotateTimer < nonRotateTime)
            Drop();
        else
            Rotate();

        dir = dir.normalized * Speed;
        Translate(dir);
    }

    void Drop()
    {
        if (!_drop)
            return;

        _dropSpeed -= com.GameTime.deltaTime * dropDec;
        if (_dropSpeed < 0)
        {
            _dropSpeed = 0;
        }
        var delta = Vector3.down * com.GameTime.deltaTime * _dropSpeed;
        transform.position += delta;
    }

    float GetCurrentAngularSpeed()
    {
        var f = Speed / speedMax;
        return angularSpeed * f;
    }

    void Rotate()
    {
        var tpDir = dir;
        if (_target != null)
        {
            var idealDir = _target.position - transform.position;
            tpDir = Vector3.RotateTowards(dir, idealDir, GetCurrentAngularSpeed() * GameTime.deltaTime, 0);
            dir = tpDir;
        }
    }
}
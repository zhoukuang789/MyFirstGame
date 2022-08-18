using UnityEngine;
using com;

public class MissileMove : MonoBehaviour
{
    public float angularSpeed = 10;
    public float acc = 10;
    public float speedMax = 4;
    private Transform _target;

    private Vector3 _dropDir;
    private bool _drop = false;
    public float dropDec = 2f;
    public float dropStartSpeed = 2f;
    private float _dropSpeed;
    public float lifeTime = 9;
    private float _lifeTimer;

    public RotateAlignMove rotateAlignMove;
    float speed;
    public Vector3 dir { get; protected set; }

    public void Init(Vector3 dropDir, Transform target)
    {
        _target = target;
        _lifeTimer = lifeTime;

        _drop = true;
        _dropSpeed = dropStartSpeed;
        _dropDir = dropDir;
    }

    private void Update()
    {
        _lifeTimer -= com.GameTime.deltaTime;
        if (_lifeTimer < 0)
        {
            Destroy(this.gameObject);
            return;
        }

        if (_drop)
            Drop();

        Trace();
    }

    void Drop()
    {
        if (!_drop)
            return;

        _dropSpeed -= com.GameTime.deltaTime * dropDec;
        if (_dropSpeed < 0)
        {
            _dropSpeed = 0;
            _drop = false;
            speed = 0;
            dir = transform.forward;
        }

        var delta = _dropDir * com.GameTime.deltaTime * _dropSpeed;
        transform.position += delta;
    }

    float GetCurrentAngularSpeed()
    {
        var f = speed / speedMax;
        return angularSpeed * f;
    }

    void Trace()
    {
        if (!_drop && _target != null)
        {
            var idealDir = _target.position - transform.position;
            var tpDir = Vector3.RotateTowards(dir, idealDir, GetCurrentAngularSpeed() * GameTime.deltaTime, 0);
            dir = tpDir;
        }

        if (speed < speedMax)
            speed += acc * GameTime.deltaTime;
        else
            speed = speedMax;

        dir = dir.normalized * speed;
        Translate(dir);
    }

    void Translate(Vector3 d)
    {
        rotateAlignMove?.Rotate(d);
        transform.position += d * speed * GameTime.deltaTime;
    }
}
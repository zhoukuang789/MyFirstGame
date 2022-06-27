using UnityEngine;
using DG.Tweening;
using com;

public class CameraShake : MonoBehaviour
{
    public Transform target;

    public static CameraShake instance;

    private PlaneEngine engine;

    private void Start()
    {
        instance = this;
    }

    void RegularShake()
    {
        engine = ReferenceService.instance?.playerPlane?.engine;
        if (engine == null)
        {
            Invoke("RegularShake", 2);
            return;
        }

        target.DOKill();
        var shakeMagnitude = engine.power / 1000000;
        shakeMagnitude = Mathf.Clamp(shakeMagnitude, 0, 1);

        target.DOShakePosition(0.25f, Mathf.Lerp(0.04f, 0.11f, shakeMagnitude), 24, 90, false, false).OnComplete(RegularShake);
    }

    public void ShakeOnHit(int powerLevel)
    {
        target.DOKill();
        target.DOShakePosition(3, 2, 10);
    }

    private void OnDisable()
    {
        target.DOKill();
    }

    private void OnEnable()
    {
        RegularShake();
    }
}

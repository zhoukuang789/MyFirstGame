using UnityEngine;
using DG.Tweening;

public class CameraShake : MonoBehaviour
{
    public PlaneEngine engine;
    public Transform target;

    public static CameraShake instance;

    private void Start()
    {
        RegularShake();
        instance = this;
    }

    void RegularShake()
    {
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
}

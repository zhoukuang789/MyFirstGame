using UnityEngine;

public class FollowCam : MonoBehaviour
{
    private Transform _player;
    public Vector3 offset = new Vector3(0, 9.3f, 26.5f);
    public float distance = 150f;

    public static FollowCam instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    public void Init()
    {
        _player = com.ReferenceService.instance.playerPlane.transform;
    }

    void Update()
    {
        if (_player == null || com.ReferenceService.instance.playerPlane.health.IsDead())
            return;

        transform.position = _player.position + (-_player.forward * offset.z + _player.up * offset.y);
        transform.LookAt(_player.position + _player.forward * distance, Vector3.up);
    }
}
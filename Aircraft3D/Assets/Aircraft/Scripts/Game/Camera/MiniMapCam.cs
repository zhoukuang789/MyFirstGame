using UnityEngine;

public class MiniMapCam : MonoBehaviour
{
    private Transform _player;

    private Vector3 _offset;

    private void Start()
    {
        _player = com.ReferenceService.instance.playerPlane.transform;
        _offset = transform.position - _player.position;
    }

    void LateUpdate()
    {
        transform.position = _player.position + _offset;
    }
}

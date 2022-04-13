using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerX1 : MonoBehaviour {
    
    public Transform player;
    public Vector3 offset = new Vector3(0, 9.3f, 26.5f);
    public float distance = 150f;

    
    void LateUpdate() {
        transform.position = player.position + (player.up * offset.z);
        transform.LookAt(player.position, Vector3.up);
    }
}
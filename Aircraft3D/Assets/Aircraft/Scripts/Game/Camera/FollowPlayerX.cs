using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerX : MonoBehaviour {
    
    public Transform player;
    public Vector3 offset = new Vector3(0, 9.3f, 26.5f);
    public float distance = 150f;

    
    void LateUpdate() {
        transform.position = player.position + (-player.forward * offset.z + player.up * offset.y);
        transform.LookAt(player.position + player.forward * distance, Vector3.up);
    }
}
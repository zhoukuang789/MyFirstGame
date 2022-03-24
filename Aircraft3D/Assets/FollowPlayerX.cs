using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerX : MonoBehaviour {
    public Transform target;
    public float distanceH = 7f;
    public float distanceV = 4f;

    void Update() {
        
    }
    
    void LateUpdate()
    {
        Vector3 nextpos = target.position + target.forward * -1 * distanceH + target.up * distanceV;
 
        transform.position = nextpos;
 
        transform.LookAt(target);
    }
}
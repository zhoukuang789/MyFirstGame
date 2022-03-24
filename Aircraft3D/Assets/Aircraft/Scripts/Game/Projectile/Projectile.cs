using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    public float speed = 20;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
    }
}

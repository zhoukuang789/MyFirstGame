using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestOnTriggerEnter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        Debug.Log(other);
    }
}

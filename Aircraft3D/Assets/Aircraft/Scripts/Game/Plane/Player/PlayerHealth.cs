using UnityEngine;
using System.Collections;

public class PlayerHealth : PlaneHealth
{
    public override void Die()
    {
        _dead = true;

        DieFeedBack();

        GetComponent<PlaneMovement>().enabled = false;
        GetComponent<PlaneController>().enabled = false;
        GetComponent<PlaneWeapon>().enabled = false;
        //Destroy(gameObject);
    }
}
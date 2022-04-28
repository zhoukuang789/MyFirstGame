using UnityEngine;
using System.Collections;

public class PlayerHealth : PlaneHealth
{
    public override void Die()
    {
        _dead = true;

        DieFeedBack();

        //GetComponent<PlaneMovement>().enabled = false;
        GetComponent<PlaneController>().enabled = false;
        GetComponent<PlaneWeapon>().enabled = false;
        plane.engine.power = 0;

        plane.movement.rb.AddTorque(transform.right*22+ transform.forward * 15 + transform.up * 6);
        plane.movement.rb.AddForce(Vector3.up * (-1600));
        //Destroy(gameObject);
        ShowDeathMenu();
    }

    void ShowDeathMenu()
    {
        DeathMenuBehaviour.instance.Show();
    }
}
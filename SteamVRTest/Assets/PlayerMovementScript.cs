using UnityEngine;

using Valve.VR;
using Valve.VR.InteractionSystem;

public class PlayerMovementScript : MonoBehaviour
{
    public SteamVR_Action_Vector2 input;
    public float speed;

    void Update()
    {
        Debug.Log(input.axis);
        var localMovement = new Vector3(input.axis.x, 0, input.axis.y);
        var worldMovement = Player.instance.hmdTransform.TransformDirection(localMovement);
        var worldMovementOfPlane = Vector3.ProjectOnPlane(worldMovement, Vector3.up);
        transform.position += speed * Time.deltaTime * worldMovementOfPlane;
    }
}
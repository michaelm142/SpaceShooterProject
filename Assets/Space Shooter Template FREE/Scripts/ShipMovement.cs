using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    private Rigidbody body;

    public float maxSpeed = 10.0f;
    public float acceleration = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        body.maxLinearVelocity = maxSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 move = VirtualInputManager.GetAxis("Move") * acceleration;
        Vector2 rotate = VirtualInputManager.GetAxis("Rotate");

        body.AddForce(transform.up * move.y + transform.right * move.x, ForceMode.VelocityChange);
        body.AddTorque(transform.up * rotate.x);

        // correct rotation
        // allign vectors
        float dot_x = Vector3.Dot(transform.up, Vector3.right);
        float dot_z = -Vector3.Dot(transform.up, Vector3.forward);
        float dot_y = Vector3.Dot(transform.up, Vector3.up);
        // calculate torque
        Vector3 torque = dot_x * Vector3.forward + dot_z * Vector3.right + Vector3.right * (1.0f - Mathf.Clamp01(dot_y));
        // apply torque
        body.AddTorque(torque);
    }
}

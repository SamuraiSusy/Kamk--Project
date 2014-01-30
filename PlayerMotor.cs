using UnityEngine;
using System.Collections;

// player's hitbox is capsule, physics need rigidbody
[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    private float walkingSpeed = 4.0f;

    public void Move(Vector3 direction)
    {
        rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, 0);

        // player's movement
        Vector3 forceX = direction.normalized.x * walkingSpeed * transform.forward;
        Vector3 forceZ = direction.normalized.z * walkingSpeed * transform.right;

        rigidbody.AddForce(forceX, ForceMode.VelocityChange);
        rigidbody.AddForce(forceZ, ForceMode.VelocityChange);
    }

    public void FixedUpdate()
    {
        // running, jumping
    }
}

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

//using UnityEngine;
//using System.Collections;

//[RequireComponent(typeof(Rigidbody))]
//public class PlayerMotor : MonoBehaviour
//{
//    private float speed = 3.0f;

//    public void Move(Vector3 direction)
//    {
//        rigidbody.velocity = new Vector3(0.0f, rigidbody.velocity.y, 0.0f);

//        Vector3 forceX = direction.normalized.x * speed * transform.forward;
//        Vector3 forceZ = direction.normalized.z * speed * transform.right;

//        rigidbody.AddForce(forceX, ForceMode.VelocityChange);
//        rigidbody.AddForce(forceZ, ForceMode.VelocityChange);
//    }

//    void FixedUpdate()
//    {
//        if (Input.GetButtonDown("Jump"))
//            rigidbody.velocity = new Vector3(0, 10, 0);

//        if (Input.GetButtonDown("Running"))
//            speed = 5.0f;
//    }
//}

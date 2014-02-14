using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    // character set up
    private CharacterController controller;
    private float gravity = 10.0f;
    private float movementSpeed = 5.0f;
    private float jumpingSpeed = 7.0f;
    private Vector3 moveDirection = Vector3.zero;

	// Use this for initialization
	void Start()
    {
        // also add a CharacterController to the player in Unity
        controller = GetComponent<CharacterController>();

        if(rigidbody)
            rigidbody.freezeRotation = true;
	}
	
	// Update is called once per frame
	void Update()
    {
        Movement();
        ButtonInput();
	}

    void Movement()
    {
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection = moveDirection * movementSpeed;

            if (Input.GetButtonDown("Jump"))
                moveDirection.y = jumpingSpeed;
        }
        moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);
        controller.Move(moveDirection * Time.deltaTime);
    }

    void ButtonInput()
    {
        if(Input.GetButtonDown("Run"))
            movementSpeed = 9.5f;
        else if(Input.GetButtonUp("Run"))
            movementSpeed = 5.0f;
        else if(Input.GetButtonDown("Sneak"))
            movementSpeed = 2.0f;
        else if(Input.GetButtonUp("Sneak"))
            movementSpeed = 5.0f;
    }
}
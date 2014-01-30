using UnityEngine;
using System.Collections;

// needs PlayerMotor to work
[RequireComponent(typeof(PlayerMotor))]
public class PlayerInput : MonoBehaviour
{
    private Transform head;
    private PlayerMotor motor;

    // Camera
    // moves horizontally
    private Quaternion startRotation;
    private float sensitivityX = 5.0f;
    private float minimumX = -360.0f;
    private float maximumX = 360.0f;
    private float rotationX = 0;

    // moves vertically
    private Quaternion startHeadRotation;
    private float sensitivityY = 5.0f;
    private float minimumY = -85.0f;
    private float maximumY = 85.0f;
    private float rotationY = 0;

	// Use this for initialization
	void Start()
    {
        head = Camera.main.transform;
        motor = GetComponent<PlayerMotor>();
        startRotation = transform.localRotation;
        startHeadRotation = head.localRotation;
	}
	
	// Update is called once per frame
	void Update()
    {
        // locks the cursor to the game window
        if(!Screen.lockCursor)
            Screen.lockCursor = true;

        ButtonInput();
        MouseInput();
	}

    void FixedUpdate()
    {
        PhysicsInput();
    }

    void ButtonInput()
    {
        if(Input.GetButtonDown("Quit"))
        {
            Debug.Log("You pressed Quit button");
            Application.Quit();
        }
    }

    void MouseInput()
    {
        rotationX = MouseRotate(rotationX, "Mouse X", sensitivityX, minimumX, maximumX);
        rotationY = MouseRotate(rotationY, "Mouse Y", sensitivityY, minimumY, maximumY);
        transform.localRotation = RotateTransform(rotationY, Vector3.left, startHeadRotation);
    }

    float MouseRotate(float rotation, string axisName, float sensitivity, float minRotation, float maxRotation)
    {
        rotation += Input.GetAxis(axisName) * sensitivity;
        rotation = ClampAngle(rotation, minRotation, maxRotation);
        return rotation;
    }

    // camera
    public static float ClampAngle(float angle, float min, float max)
    {
        if(angle < -360.0f)
            angle += 360.0f;
        else if(angle > 360.0f)
            angle -= 360.0f;

        return Mathf.Clamp(angle, min, max);
    }

    Quaternion RotateTransform(float rotation, Vector3 axsis, Quaternion startRotation)
    {
        Quaternion newQuaternion = Quaternion.AngleAxis(rotation, axsis);
        Quaternion result = startRotation * newQuaternion;

        return result;
    }

    void PhysicsInput()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal"));

        // add jumping

        motor.Move(movement);
    }
}

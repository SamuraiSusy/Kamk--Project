//using UnityEngine;
//using System.Collections;

//// needs PlayerMotor to work
//[RequireComponent(typeof(PlayerMotor))]
//public class PlayerInput : MonoBehaviour
//{
//    private Transform head;
//    private PlayerMotor motor;

//    // Camera
//    // moves horizontally
//    private Quaternion startRotation;
//    private float sensitivityX = 5.0f;
//    private float minimumX = -360.0f;
//    private float maximumX = 360.0f;
//    private float rotationX = 0;

//    // moves vertically
//    private Quaternion startHeadRotation;
//    private float sensitivityY = 5.0f;
//    private float minimumY = -85.0f;
//    private float maximumY = 85.0f;
//    private float rotationY = 0;
  
//    private float useDistance = 3.5f;

//    // Use this for initialization
//    void Start()
//    {
//        head = Camera.main.transform;
//        motor = GetComponent<PlayerMotor>();
//        startRotation = transform.localRotation;
//        startHeadRotation = head.localRotation;
//    }
	
//    // Update is called once per frame
//    void Update()
//    {
//        // locks the cursor to the game window
//        if(!Screen.lockCursor)
//            Screen.lockCursor = true;

//        ButtonInput();
//        MouseInput();
//    }

//    void FixedUpdate()
//    {
//        PhysicsInput();
//    }

//    void ButtonInput()
//    {
//        if(Input.GetButtonDown("Quit"))
//        {
//            Debug.Log("You pressed Quit button");
//            Application.Quit();
//        }
//    }

//    void MouseInput()
//    {
//        rotationX = MouseRotate(rotationX, "Mouse X", sensitivityX, minimumX, maximumX);
//        rotationY = MouseRotate(rotationY, "Mouse Y", sensitivityY, minimumY, maximumY);
//        transform.localRotation = RotateTransform(rotationY, Vector3.left, startHeadRotation);
//    }

//    float MouseRotate(float rotation, string axisName, float sensitivity, float minRotation, float maxRotation)
//    {
//        rotation += Input.GetAxis(axisName) * sensitivity;
//        rotation = ClampAngle(rotation, minRotation, maxRotation);
//        return rotation;
//    }

//    // camera
//    public static float ClampAngle(float angle, float min, float max)
//    {
//        if(angle < -360.0f)
//            angle += 360.0f;
//        else if(angle > 360.0f)
//            angle -= 360.0f;

//        return Mathf.Clamp(angle, min, max);
//    }

//    Quaternion RotateTransform(float rotation, Vector3 axsis, Quaternion startRotation)
//    {
//        Quaternion newQuaternion = Quaternion.AngleAxis(rotation, axsis);
//        Quaternion result = startRotation * newQuaternion;

//        return result;
//    }

//    void PhysicsInput()
//    {
//        Vector3 movement = new Vector3(Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal"));

//        // add jumping

//        motor.Move(movement);
//    }
  
//    void Use()
//    {
//        Ray ray;
//        RaycastHit hitInfo;

//        ray = new Ray(new Vector3(head.transform.position.x, head.transform.position.y - 1,
//            head.transform.position.z), head.transform.forward);

//        if (Physics.Raycast(ray, out hitInfo, useDistance, layerMask))
//        {
//            Debug.DrawLine(ray.origin, hitInfo.point, Color.red);

//            if (hitInfo.transform.tag == "Usable")
//            {
//                hitInfo.transform.SendMessage("Use", ray.direction, SendMessageOptions.DontRequireReceiver);
//            }
//        }
//    }
//}

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerInput : MonoBehaviour
{
    private Transform head;
    private PlayerMotor motor;

    // Camera settings, has to be MainCamera tag in Unity!
    private Quaternion startRotation;
    private float sensitivityX = 5.0f;
    private float minimumX = -360.0f;
    private float maximumX = 360.0f;
    private float rotationX = 0.0f;

    private Quaternion startHeadRotation;
    private float sensitivityY = 5.0f;
    private float minimumY = -85.0f;
    private float maximumY = 85.0f;
    private float rotationY = 0.0f;

    private float useDistance = 3.5f;

   // private FlareGun flareGun;

    private LayerMask layerMask;

    // Use this for initialization
    void Start()
    {
        head = Camera.main.transform;
        motor = GetComponent<PlayerMotor>();
        startRotation = transform.localRotation;
        startHeadRotation = head.localRotation;
       // flareGun = head.FindChild("FlareGun").GetComponent<FlareGun>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Screen.lockCursor)
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
        //if (Input.GetButtonDown("Use"))
        //{
        //    Use();
        //}
        if (Input.GetButtonDown("Quit"))
        {
            Debug.Log("Quit");
            Application.Quit();
        }
        else if (Input.GetButtonDown("Jump"))
            transform.Translate(Vector3.up * 260 * Time.deltaTime, Space.World);

        //else if (Input.GetButtonDown("Flare"))
        //{
        //    flareGun.Fire();
        //}
    }

    void MouseInput()
    {
        rotationX = MouseRotate(rotationX, "Mouse X", sensitivityX, minimumX, maximumX);
        rotationY = MouseRotate(rotationY, "Mouse Y", sensitivityY, minimumY, maximumY);
        transform.localRotation = RotateTransform(rotationX, Vector3.up, startRotation);
        head.localRotation = RotateTransform(rotationY, Vector3.left, startHeadRotation);
    }

    float MouseRotate(float rotation, string axisName, float sensitivity, float minRotation, float maxRotation)
    {
        rotation += Input.GetAxis(axisName) * sensitivity;
        rotation = ClampAngle(rotation, minRotation, maxRotation);
        return rotation;
    }

    public static float ClampAngle(float angle, float min, float max) // camera
    {
        if (angle < -360.0f)
        {
            angle += 360.0f;
        }
        else if (angle > 360)
        {
            angle -= 360.0f;
        }

        return Mathf.Clamp(angle, min, max);
    }

    Quaternion RotateTransform(float rotation, Vector3 axis, Quaternion startRotation)
    {
        Quaternion newQuaternion = Quaternion.AngleAxis(rotation, axis);
        Quaternion result = startRotation * newQuaternion;
        return result;
    }

    void PhysicsInput()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Vertical"), 0.0f, Input.GetAxis("Horizontal"));

        Vector3 jump = new Vector3(Input.GetAxis("Vertical"), 0, 0);

        motor.Move(movement);
    }

    void Use()
    {
        Ray ray;
        RaycastHit hitInfo;

        ray = new Ray(new Vector3(head.transform.position.x, head.transform.position.y - 1,
            head.transform.position.z), head.transform.forward);

        if (Physics.Raycast(ray, out hitInfo, useDistance, layerMask))
        {
            Debug.DrawLine(ray.origin, hitInfo.point, Color.red);

            if (hitInfo.transform.tag == "Usable")
            {
                hitInfo.transform.SendMessage("Use", ray.direction, SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}

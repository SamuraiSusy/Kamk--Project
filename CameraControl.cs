using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{
    public GameObject capsule;
    public GameObject playerCamera;
    public float rotationSpeed = 1.0f;
    private float maxY = 80.0f;
    private float minY = -80.0f;

	// Use this for initialization
	void Start()
    {
	
	}
	
	// Update is called once per frame
	void Update()
    {
        CameraRotation();
	}

    void CameraRotation()
    {
        if (Input.GetAxis("Mouse X") > 0 || Input.GetAxis("Mouse X") < 0)
        {
            capsule.transform.Rotate(new Vector3(0, 1, 0), rotationSpeed * Input.GetAxis("Mouse X"));
        }
        if (Input.GetAxis("Mouse Y") > 0 || Input.GetAxis("Mouse Y") < 0)
        {
            float rotationY = playerCamera.transform.localEulerAngles.x;
            float tempRot = -rotationSpeed * Input.GetAxis("Mouse Y");

            if(rotationY + tempRot < maxY)
                playerCamera.transform.Rotate(new Vector3(1, 0, 0), -rotationSpeed * Input.GetAxis("Mouse Y"));
            if(rotationY + tempRot > 360 + minY)
                playerCamera.transform.Rotate(new Vector3(1, 0, 0), -rotationSpeed * Input.GetAxis("Mouse Y"));
            if (rotationY < minY && rotationY < maxY)
                playerCamera.transform.localEulerAngles = new Vector3(0, 0, 0);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCamera : MonoBehaviour
{
    public float rotationSpeed = 1;

    //private bool isLocked;
    private bool mouseCamera;

    void Start()
    {
#if UNITY_EDITOR
        mouseCamera = true;
#else
        mouseCamera = false;
#endif
        //isLocked = false;
    }

    void Update()
    {
        //if (Input.GetMouseButton(0))
        //{
        //    isLocked = !isLocked;

        //    if (isLocked)
        //    {
        //        Cursor.lockState = CursorLockMode.Locked;
        //    }
        //    else
        //    {
        //        Cursor.lockState = CursorLockMode.None;
        //    }
        //}

        if (mouseCamera && Input.GetMouseButton(0)) { MoveCameraToMouse(); }
    }

    private void MoveCameraToMouse()
    {
        this.transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * rotationSpeed), Space.World);
        this.transform.Rotate(new Vector3(-Input.GetAxis("Mouse Y") * rotationSpeed, 0, 0), Space.Self);
    }

}

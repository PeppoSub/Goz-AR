using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestoreCamera : MonoBehaviour
{
    public GameObject lookAtThis;
    public float speed = 5f;

    void Update()
    {
        Vector3 direction = lookAtThis.gameObject.transform.position - this.gameObject.transform.position;
        Quaternion toRotation = Quaternion.FromToRotation(this.gameObject.transform.forward, direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, speed * Time.deltaTime);

        //Transform target = lookAtThis.gameObject.transform;
        //this.gameObject.transform.LookAt(target);
    }
}

//public float limitCos = 0.5f;
//public GameObject lookAtThis;
//private bool restoring = false;

//if (lookAtThis != null)
//{
//    Vector3 direction = lookAtThis.gameObject.transform.position - this.gameObject.transform.position;
//    direction = direction.normalized;
//    Vector3 gyroVector = _rawGyroRotation.rotation * Vector3.forward;
//    gyroVector = gyroVector.normalized;
//    Quaternion toRotation = Quaternion.FromToRotation(this.gameObject.transform.forward, direction);

//    float cos = Vector3.Dot(direction, gyroVector);
//    //Debug.Log("cos = " + cos);

//    // ... use the restoring variable to make it go back
//    if (cos < limitCos) { transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, _smoothing); }
//}
//else
//{
//    //#if !UNITY_EDITOR
//    ApplyGyroRotation();
//    ApplyCalibration();

//    transform.rotation = Quaternion.Slerp(transform.rotation, _rawGyroRotation.rotation, _smoothing);
//    //#endif
//}

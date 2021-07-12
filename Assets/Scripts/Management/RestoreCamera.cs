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

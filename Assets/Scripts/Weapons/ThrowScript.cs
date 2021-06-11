using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowScript : MonoBehaviour
{
    public GameObject theCamera;
    public GameObject shootPoint;
    public Rigidbody theProjectile;
    public int thrust = 25;
    public float reloadTime = 0.5f;
    //public Vector3 offset = new Vector3(0f, 0f, 0f);  
    //public Vector3 torque = new Vector3(0f, 0f, 0f);

    private float coolTime = 0;
    private Vector3 cameraPos;

    public void Update()
    { 
        if(coolTime > 0) { coolTime -= Time.deltaTime; }
        //cameraPos = theCamera.transform.position; //.normalized;
    }

    public void Throw()
    {
        Rigidbody clone;

        if(coolTime <= 0)
        {
            //Vector3 throwFrom = new Vector3(cameraPos.x * (1 + offset.x), cameraPos.y * (1 + offset.y), cameraPos.z * (1 + offset.z));
            //Vector3 throwFrom = cameraPos + offset;
            Vector3 throwFrom = shootPoint.transform.position;

            // Instantiate the projectile at the position and rotation of this transform + offset
            clone = Instantiate(theProjectile, throwFrom, Quaternion.identity);
            clone.transform.LookAt(theCamera.transform.forward);

            // Give the cloned object an initial velocity
            clone.velocity = transform.TransformDirection(theCamera.transform.forward * thrust);
            //clone.AddForce(theCamera.transform.forward * thrust);
            //clone.AddRelativeTorque(torque); // clone.AddTorque(torque); 

            coolTime = reloadTime;

            // // previous failed attempts ...
            //Vector3 throwFrom = theCamera.transform.position + offset;
            //Quaternion quat = Quaternion.LookRotation(theCamera.transform.position, Vector3.up);
            //clone = Instantiate(theProjectile, throwFrom, quat);
            //Quaternion q = Quaternion.FromToRotation(clone.transform.forward, theCamera.transform.forward);
            //clone.transform.rotation = q * clone.transform.rotation;
        }

    }


}

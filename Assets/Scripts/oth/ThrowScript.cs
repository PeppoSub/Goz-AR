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

    private float coolTime = 0;
    private Vector3 cameraPos;

    public void Update()
    { 
        if(coolTime > 0) { coolTime -= Time.deltaTime; }
        //cameraPos = theCamera.transform.position; 
    }

    public void Throw()
    {
        Rigidbody clone;

        if(coolTime <= 0)
        {
            Vector3 throwFrom = shootPoint.transform.position;
            //Quaternion throwRotation = shootPoint.transform.rotation;

            // Instantiate the projectile at the position and rotation of this transform + offset
            //clone = Instantiate(theProjectile, throwFrom, throwRotation);
            clone = Instantiate(theProjectile, throwFrom, Quaternion.identity);
            clone.transform.LookAt(theCamera.transform.forward);

            // Give the cloned object an initial velocity
            clone.velocity = transform.TransformDirection(theCamera.transform.forward * thrust);
            //clone.AddForce(theCamera.transform.forward * thrust);
            //clone.AddRelativeTorque(torque); // clone.AddTorque(torque); 

            coolTime = reloadTime;
        }

    }


}

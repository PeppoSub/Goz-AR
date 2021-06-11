using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float maxLo = -1f;
    public float maxFar = 10f;
    public Vector3 rotationSpeed = new Vector3(0f,0f,0f);
    public float stayOnGround = 0f;

    private Vector3 theSpawnPoint;
    private float timeOnGround;
    private Rigidbody rigidBody;

    void Start()
    {
        theSpawnPoint = this.transform.position;
        timeOnGround = 0;
        rigidBody = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        float distance = Vector3.Distance(theSpawnPoint, this.transform.position);
        if ((this.transform.position.y < maxLo) || (distance > maxFar))
        {
            Destroy(gameObject);
        }

        if ((stayOnGround > 0) && (this.transform.position.y <= 0))
        {
            //this.transform.Rotate(0f, 0f, 0f);
            //this.transform.Translate(0f, 0f, 0f);
            rigidBody.velocity = Vector3.zero;
            rigidBody.angularVelocity = Vector3.zero;
            timeOnGround += Time.deltaTime;
            if(timeOnGround >= stayOnGround) { Destroy(gameObject); }
        }
        else
        {
            this.transform.Rotate(rotationSpeed.x, rotationSpeed.y, rotationSpeed.z);
        }

    }

}

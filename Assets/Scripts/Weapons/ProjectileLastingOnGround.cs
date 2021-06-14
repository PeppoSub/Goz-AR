using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLastingOnGround : MonoBehaviour
{
    public float speed = 10;                            // projectile speed
    public Vector3 rotation = new Vector3(0f,0f,0f);    // rotation around the relative x,y,z axis
    public int damage = 10;                             // applied damage. NOTE: on a bomb set this = 0 because damage is done by the explosion
    public float coolDown = 0.5f;                       // cooldown time between shoots
    public bool stayOnGround = false;                   // stop and rest still when hit the ground 
    public bool destroyOnImpact = false;                // destroy on first impact 
    public float lifeTime = 3f;                         // destroy after N seconds
    public float maxFar = 10f;                          // destroy when too far away
    public float groundCorrection = 0.05f;              // lower the ground level slightly below the spawner position 

    public GameObject hitSmoke;                         // particle effect produced when enemy projectile hits something

    private Rigidbody rigidBody;
    private Vector3 theSpawnPoint;
    private float timeAlive;
    private float groundLevel;

    void Start()
    {
        theSpawnPoint = this.transform.position;
        timeAlive = 0;
        rigidBody = this.GetComponent<Rigidbody>();
        groundLevel = GameStatus.groundLevel - groundCorrection;
    }

    void Update()
    {
        float distance = Vector3.Distance(theSpawnPoint, this.transform.position);
        timeAlive += Time.deltaTime;
        if ((distance > maxFar) || (timeAlive >= lifeTime))
        {
            Destroy(gameObject);
        }

        // the projectile lays still on the ground before disappearing ... and slowly sinks into hell 
        if (stayOnGround && (this.transform.position.y <= groundLevel))
        {
            rigidBody.velocity = Vector3.zero;
            rigidBody.angularVelocity = Vector3.zero;
            rotation = Vector3.zero;
            speed = 0f;
        }
        else 
        { 
            // vector calculus for the local axis of rotation
            Vector3 relativeY = Vector3.up.normalized;                             // up
            Vector3 relativeZ = rigidBody.velocity.normalized;                     // forward
            Vector3 relativeX = Vector3.Cross(relativeZ, relativeY).normalized;    // horizon

            // relative rotations
            if (rotation.x != 0) { this.transform.RotateAround(this.transform.position, relativeX, 360f * rotation.x * Time.deltaTime); }
            if (rotation.y != 0) { this.transform.RotateAround(this.transform.position, relativeY, 360f * rotation.y * Time.deltaTime); }
            if (rotation.z != 0) { this.transform.RotateAround(this.transform.position, relativeZ, 360f * rotation.z * Time.deltaTime); }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (damage > 0)   // this fixes the issue with the bomb, by not calling ApplyDamage twice 
        {
            // never mind if the object hitted does not have an ApplyDamage function (e.g., scene object)
            col.gameObject.SendMessage("ApplyDamage", damage, SendMessageOptions.DontRequireReceiver);
        }

        ContactPoint[] contacts = new ContactPoint[10];
        int numContacts = col.GetContacts(contacts);
        if (hitSmoke != null) { Instantiate(hitSmoke, contacts[0].point, Quaternion.identity); }

        if (destroyOnImpact) { Destroy(gameObject); }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileExplode : MonoBehaviour
{
    public int damage = 10;                  // damage applied within the radius
    public float radius = 2f;                // radius of the explosion
    public float force = 10f;                // force applied to rigidbodies
    public bool explodeOnGround = false;     // explode at ground level

    public GameObject explosionEffect;       // particle effect


    void Update()
    {
        if (explodeOnGround && (this.transform.position.y <= GameStatus.groundLevel))
        {
            Explode();
        }
    }

    void OnCollisionEnter(Collision col)
    {
        Explode();
    }

    public void Explode()
    {
        Instantiate(explosionEffect, this.transform.position, Quaternion.identity);

        Collider[] withinRadius = Physics.OverlapSphere(this.transform.position, radius);
        foreach(Collider nearby in withinRadius)
        {
            Rigidbody rb = nearby.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.AddExplosionForce(force, this.transform.position, radius);
            }
            nearby.gameObject.SendMessage("ApplyDamage", damage, SendMessageOptions.DontRequireReceiver);
        }

        Destroy(gameObject);
    }

}

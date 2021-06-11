using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : MonoBehaviour
{
    public int bossEnergy = 10;
    public GameObject hitEffect;
    public GameObject bigExplosion;

    private int energy;

    void Start()
    {
        Transform target = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        transform.LookAt(target);

        //bossEnergy = GameStatus.bossEnergy;
        //energy = bossEnergy;
    }

    void Update()
    {
        Transform target = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        transform.LookAt(target);

        if ((bossEnergy > 0) && (energy <= 0)) { Die(); }
    }

    void OnCollisionEnter(Collision col)
    {
        // Make an empty list to hold contact points & get the contact points for this collision
        ContactPoint[] contacts = new ContactPoint[10];
        int numContacts = col.GetContacts(contacts);
        Instantiate(hitEffect, contacts[0].point, Quaternion.identity);

        energy--;
    }

    void Die()
    {
        // call death animation ... or just explode :)
        Instantiate(bigExplosion, this.transform.position, Quaternion.identity);
        Destroy(gameObject);

        GameStatus.bossKill = true;
    }


}

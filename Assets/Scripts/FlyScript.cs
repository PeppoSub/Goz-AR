using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyScript : MonoBehaviour
{
    public float maxSpeed = 1f;
    public float maxHi = 1f;
    public float hitDistance = 1f;     // at what distance they hit the player
    public float maxDistance = 30f;    // at what distance they are out of the game
    public float sideMove = 0.2f;
    public float updownMove = 0.2f;
    public float sideSpeed = 0.2f;
    public float updownSpeed = 0.2f;
    public float stepTime = 0.5f;
    public float health = 10;
    public GameObject smoke;
    public GameObject smokeRed;

    private Vector3 spawnPoint;
    private float initDistance;
    private float flySpeed;
    private float seconds;
    private float phase;

    void Start()
    {
        Vector3 playerPos = GameObject.FindGameObjectsWithTag("Player")[0].transform.position;
        spawnPoint = this.transform.position;
        initDistance = Vector3.Distance(playerPos, spawnPoint);
        flySpeed = 0;
        seconds = 0;
        phase = Random.Range(0, 2 * Mathf.PI);
    }

    void Update()
    {
        seconds += Time.deltaTime ;
        float distanceFromSpawn = Vector3.Distance(this.transform.position, spawnPoint);
        //Debug.Log("distanceFromSpawn = " + distanceFromSpawn.ToString());

        Vector3 playerPos = GameObject.FindGameObjectsWithTag("Player")[0].transform.position;
        Vector3 vectorDistance = playerPos - this.transform.position; ;
        float distance = vectorDistance.magnitude;
        vectorDistance.Normalize();

        // destroy bubbles that get too far ... it does not happen as all bubbles go to player
        if (distance > maxDistance) { Destroy(gameObject); }

        // set the speed: accelerate up to maxSpeed
        if (flySpeed < maxSpeed) { flySpeed += 9.81f * Time.deltaTime; }
        else { flySpeed = maxSpeed; }

        // after reaching maxHi from the spawn, the enemy flies toward the player
        if (distanceFromSpawn < maxHi)
        {
            this.transform.Translate(Vector3.up * Time.deltaTime * flySpeed);
        }

        Vector3 finalVelocity = vectorDistance * Time.deltaTime * flySpeed;

        float omega = 0;
        if (sideMove > 0)
        {
            omega = 2 * Mathf.PI * sideSpeed;
            finalVelocity += OrthOriz(finalVelocity) * Mathf.Sin(omega * seconds + phase) * sideMove * Time.deltaTime;
        }
        if (updownMove > 0)
        {
            omega = 2 * Mathf.PI * updownSpeed;
            finalVelocity += new Vector3(0f, Mathf.Sin(omega * seconds + phase) * updownMove * Time.deltaTime, 0f);
        }
        if (stepTime > 0)
        {
            omega = 2 * Mathf.PI * stepTime;
            finalVelocity *= Mathf.Sin(omega * seconds) * Mathf.Sin(omega * seconds);
        }

        //Debug.Log("finalVelocity = " + finalVelocity.ToString());
        this.transform.Translate(finalVelocity);

        // destroy bubbles coming too close in red smoke and deal damage
        if (distance < hitDistance)
        {
            Destroy(gameObject);
            Instantiate(smokeRed, gameObject.transform.position, Quaternion.identity);

            GameStatus.life--;
            GameStatus.gotHit = true;
        }

    }

    //void OnCollisionEnter(Collision col)
    //{
    //    Destroy(gameObject);
    //    Instantiate(smoke, gameObject.transform.position, Quaternion.identity);

    //    GameStatus.score++;
    //}

    public void ApplyDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
            Instantiate(smoke, gameObject.transform.position, Quaternion.identity);

            GameStatus.score++;
        }
    }

    private Vector3 OrthOriz(Vector3 invec)
    {
        Vector3 outvec = new Vector3(-invec.z, 0f, invec.x);
        outvec.Normalize();
        return outvec;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMove : MonoBehaviour
{
    public float maxSpeed = 2f;
    public float hitDistance = 1f;     // at what distance they hit the player
    public float maxDistance = 30f;    // at what distance they are out of the game
    public float sideMove = 0.2f;
    public float sideSpeed = 0.2f;
    public float stepTime = 0.5f;
    public bool randomWalk = false;
    public GameObject smokeRed;

    private Vector3 spawnPoint;
    private float initDistance;
    //private float slideSpeed;
    private float seconds;
    private float phase;
    private float slideX = 0;
    //private float slideZ = 0;
    private float coolTime = 0;

    void Start()
    {
        Vector3 playerPos = GameObject.FindGameObjectsWithTag("Player")[0].transform.position;
        spawnPoint = this.transform.position;
        initDistance = Vector3.Distance(playerPos, spawnPoint);
        //slideSpeed = 0;
        seconds = 0;
        phase = Random.Range(0, 2 * Mathf.PI);

        if (GameStatus.speedMultiplier > 0) 
        { 
            maxSpeed = maxSpeed * GameStatus.speedMultiplier / 10f ;
            sideSpeed = sideSpeed * GameStatus.speedMultiplier / 10f ;
            stepTime = stepTime / (GameStatus.speedMultiplier / 10f) ;
        }
    }

    void Update()
    {
        seconds += Time.deltaTime;
        if (coolTime > 0) { coolTime -= Time.deltaTime; }

        float distanceFromSpawn = Vector3.Distance(this.transform.position, spawnPoint);

        Vector3 playerPos = GameObject.FindGameObjectsWithTag("Player")[0].transform.position;
        Vector3 vectorDistance = playerPos - this.transform.position;
        float distance = vectorDistance.magnitude;
        vectorDistance.y = 0f;
        float flatDistance = vectorDistance.magnitude;
        vectorDistance.Normalize();

        // destroy bubbles that get too far ... it does not happen as all bubbles go to player
        if (distance > maxDistance) { Destroy(gameObject); }

        // if not on the ground, fall down then slide toward the player
        if (this.transform.position.y > GameStatus.groundLevel)
        {
            this.transform.Translate(Vector3.down * 4.9f * seconds * seconds);
        }

        // main direction of motion toward the player
        Vector3 finalVelocity = vectorDistance * Time.deltaTime * maxSpeed;

        // if enabled, move sideways wrt player direction
        float omega = 0;
        if (sideMove > 0)
        {
            if (randomWalk)    // if enabled, random sideway motion
            {
                if (coolTime <= 0)
                {
                    slideX = Random.Range(-sideMove, sideMove);
                    coolTime = stepTime;
                }
                finalVelocity += OrthOriz(finalVelocity) * slideX * Time.deltaTime;
            }
            else               // else, oscillating sideway motion
            {
                omega = 2 * Mathf.PI * sideSpeed;
                finalVelocity += OrthOriz(finalVelocity) * Mathf.Sin(omega * seconds + phase) * sideMove * Time.deltaTime;
            }
        }

        // if enabled, move in steps instead than continuously 
        if (stepTime > 0)
        {
            omega = 2 * Mathf.PI * stepTime;
            finalVelocity *= Mathf.Sin(omega * seconds) * Mathf.Sin(omega * seconds);
        }

        // apply the translation calculated above
        this.transform.Translate(finalVelocity);

        // destroy when too close to player (ground distance), intantiate red smoke and deal damage 
        if (flatDistance < hitDistance)
        {
            Destroy(gameObject);
            Instantiate(smokeRed, gameObject.transform.position, Quaternion.identity);

            GameStatus.life--;
            GameStatus.gotHit = true;
        }

    }

    private Vector3 OrthOriz(Vector3 invec)
    {
        Vector3 outvec = new Vector3(-invec.z, 0f, invec.x);
        outvec.Normalize();
        return outvec;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirMove : MonoBehaviour
{
    public float maxSpeed = 1f;
    public float maxHi = 1f;
    public float rndHi = 0.2f;         // randomize max Hi (+ x%)
    public float spreadDistance = 1f;  // how far to spread from the spawning point (flat distance)
    public float hitDistance = 1f;     // at what distance they hit the player
    public float maxDistance = 30f;    // at what distance they are out of the game
    public float sideMove = 0.2f;
    public float updownMove = 0.2f;
    public float sideSpeed = 0.2f;
    public float updownSpeed = 0.2f;
    public float stepTime = 0.5f;
    public GameObject smokeRed;

    private Vector3 spawnPoint;
    private float initDistance;
    private float flySpeed;
    private float seconds;
    private float phase;
    private bool airBorn;
    private int leftRight;

    void Start()
    {
        Vector3 playerPos = GameObject.FindGameObjectsWithTag("Player")[0].transform.position;
        spawnPoint = this.transform.position;
        initDistance = Vector3.Distance(playerPos, spawnPoint);
        seconds = 0;
        phase = Random.Range(0, 2 * Mathf.PI);
        airBorn = false;
        if (phase < Mathf.PI) { leftRight = 1; }
        else { leftRight = -1;  }

        if (GameStatus.speedMultiplier > 0)
        {
            float multiplier = GameStatus.speedMultiplier / 3f;  // see GameStatus about multiplier values
            maxSpeed = maxSpeed * multiplier;
            sideSpeed = sideSpeed * multiplier;
            updownSpeed = updownSpeed * multiplier;
            //stepTime = stepTime / multiplier;
        }
        else { flySpeed = 0; }   // slowly accelerates up as before, other speed unchanged
    }

    void Update()
    {
        seconds += Time.deltaTime;
        float distanceFromSpawn = Vector3.Distance(this.transform.position, spawnPoint);
        float flatDistance = FlatDistance(this.transform.position, spawnPoint);

        Vector3 playerPos = GameObject.FindGameObjectsWithTag("Player")[0].transform.position;
        Vector3 vectorDistance = playerPos - this.transform.position; ;
        float distance = vectorDistance.magnitude;
        vectorDistance.Normalize();

        // destroy bubbles that get too far ... it does not happen as all bubbles go to player
        if (distance > maxDistance) { Destroy(gameObject); }

        // set the speed: accelerate up to maxSpeed
        if (flySpeed < maxSpeed) { flySpeed += 9.81f * Time.deltaTime; }
        else { flySpeed = maxSpeed; }

        // reach maxHi wrt ground level and spread-out, then fly toward the player
        float relativeHi = this.transform.position.y - GameStatus.groundLevel;
        if (rndHi > 0) { relativeHi += rndHi * relativeHi * Random.Range(0f, 1f); }  // randomize hight (+ x%)
        if ((relativeHi < maxHi) && !airBorn)
        {
            this.transform.Translate(Vector3.up * Time.deltaTime * 3f * flySpeed);
            if(flatDistance < spreadDistance)
            { 
               this.transform.Translate(leftRight * (phase/Mathf.PI) * OrthOriz(vectorDistance) * Time.deltaTime * (flySpeed/4f));
            }
        }
        else
        {
            airBorn = true;
        }

        // main direction of motion toward the player
        Vector3 finalVelocity = vectorDistance * Time.deltaTime * flySpeed;

        // if enabled, move sideways wrt player direction
        float omega = 0;
        if (sideMove > 0)
        {
            omega = 2 * Mathf.PI * sideSpeed;
            finalVelocity += OrthOriz(finalVelocity) * Mathf.Sin(omega * seconds + phase) * sideMove * Time.deltaTime;
        }

        // if enabled, move up and down
        if (updownMove > 0)
        {
            omega = 2 * Mathf.PI * updownSpeed;
            finalVelocity += new Vector3(0f, Mathf.Sin(omega * seconds + phase) * updownMove * Time.deltaTime, 0f);
        }

        // if enabled, move in steps instead than continuously 
        if (stepTime > 0)
        {
            omega = 2 * Mathf.PI * stepTime;
            finalVelocity *= Mathf.Sin(omega * seconds) * Mathf.Sin(omega * seconds);
        }

        // apply the translation calculated above
        this.transform.Translate(finalVelocity);

        // destroy when too close to player, intantiate red smoke and deal damage 
        if (distance < hitDistance)
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

    private float FlatDistance(Vector3 vecA, Vector3 vecB)
    {
        float dist2 = ((vecA.x - vecB.x) * (vecA.x - vecB.x)) + ((vecA.z - vecB.z) * (vecA.z - vecB.z));
        return Mathf.Sqrt(dist2);
    }
}

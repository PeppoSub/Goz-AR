using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSpawn : MonoBehaviour
{
    public GameObject[] bubbles;                // prefabs of the objects to spawn

    public int initSleep = 3;                   // sleep for N seconds befor starting to spawn
    public int WaitTimeSeconds = 1;             // initial interval between spawns (is an 'int' for hystorical reasons)
    public float shift = 0.1f;                  // spread in the spawning position
    public float speedUp = 0f;                  // spawner speed-up factor:  deltaT = deltaT - (speedUp * seconds)/60

    private float spawnInterval;
    private float seconds;
    private bool started;
    private System.Random rand = new System.Random();

    void Start()
    {
        started = false;
        spawnInterval = (float)WaitTimeSeconds;
        seconds = 0f;

        GameStatus.ReloadDifficultySettings();
        if (GameStatus.spawnFrequency > 0) 
        {
            float frequency = GameStatus.spawnFrequency / 2f;   // see GameStatus about multiplier values
            spawnInterval = 1f / frequency;                     // overrides basic interval & speedUp 
            speedUp = 0f;
            //WaitTimeSeconds = (int)spawnInterval;
        }
        //Debug.Log("spawnInterval = " + spawnInterval.ToString());
        //Debug.Log("GameStatus.spawnFrequency = " + GameStatus.spawnFrequency);
    }

    void Update()
    {
        if(started) 
        {
            if ((speedUp > 0f) && (spawnInterval>0.1f))    // cannot spawn more than 10 bubbles/sec.
            { 
                spawnInterval = spawnInterval - (speedUp * Time.deltaTime) /60f; 
            }
            return; 
        }

        if (seconds < (float)initSleep) 
        { 
            seconds += Time.deltaTime; 
        }
        else
        {
            StartCoroutine(StartSpawning());
            started = true;
        }

    }

    IEnumerator StartSpawning()
    {
        yield return new WaitForSeconds(spawnInterval);

        Vector3 randomSpawn = transform.position;
        float randFloat = (float)(rand.Next(1000));
        randomSpawn.x = randomSpawn.x + Mathf.Sin(randFloat) * shift;
        randomSpawn.z = randomSpawn.z + Mathf.Cos(randFloat) * shift;
        
        int bubbleTypes = bubbles.Length;
        int randomBubble = rand.Next(bubbleTypes);
        GameObject bubbleToSpawn = bubbles[randomBubble];

        Instantiate(bubbleToSpawn, randomSpawn, Quaternion.identity);
        StartCoroutine(StartSpawning());

        //Debug.Log("spawnInterval = " + spawnInterval.ToString());
    }
}
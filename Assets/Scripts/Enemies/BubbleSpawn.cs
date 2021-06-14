using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSpawn : MonoBehaviour
{
    public GameObject[] bubbles;
    public int WaitTimeSeconds = 2;
    public float shift = 0.1f;

    private System.Random rand = new System.Random();

    void Start()
    {
        StartCoroutine(StartSpawning());
    }

    IEnumerator StartSpawning()
    {
        yield return new WaitForSeconds(WaitTimeSeconds);

        Vector3 randomSpawn = transform.position;
        float randFloat = (float)(rand.Next(1000));
        randomSpawn.x = randomSpawn.x + Mathf.Sin(randFloat) * shift;
        randomSpawn.z = randomSpawn.z + Mathf.Cos(randFloat) * shift;
        
        int bubbleTypes = bubbles.Length;
        int randomBubble = rand.Next(bubbleTypes);
        GameObject bubbleToSpawn = bubbles[randomBubble];

        Instantiate(bubbleToSpawn, randomSpawn, Quaternion.identity);
        StartCoroutine(StartSpawning());
    }
}
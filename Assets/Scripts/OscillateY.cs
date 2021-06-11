using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OscillateY : MonoBehaviour
{
    public float frequency = 0.5f;
    public float maxAngle = 30f;

    private float seconds;
    void Start()
    {
        seconds = 0;
    }

    void Update()
    {
        seconds += Time.deltaTime;
        float omega = 2 * Mathf.PI * frequency;

        this.transform.Rotate(0f, maxAngle * Mathf.Sin(omega * seconds) * Time.deltaTime, 0f);
    }
}

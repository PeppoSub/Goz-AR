using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotOsci : MonoBehaviour
{
    public float frequency = 0.45f;
    public float maxAngle = 60f;
    public float maxMove = -250f;

    private float seconds;
    void Start()
    {
        seconds = 0;
    }

    void Update()
    {
        seconds += Time.deltaTime;
        float omega = 2 * Mathf.PI * frequency;

        this.transform.Rotate(0f, 0f, maxAngle * Mathf.Sin(omega * seconds) * Time.deltaTime);
        this.transform.Translate(maxMove * Mathf.Sin(omega * seconds) * Time.deltaTime,0f,0f);
    }
}

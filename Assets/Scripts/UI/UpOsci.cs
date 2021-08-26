using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpOsci : MonoBehaviour
{
    public float frequency = 0.45f;
    public float maxHi = 100f;
    public float minShr = 0.05f;

    private float seconds;
    void Start()
    {
        seconds = 0;
    }

    void Update()
    {
        seconds += Time.deltaTime;
        float omega = 2 * Mathf.PI * frequency;

        this.transform.Translate(0f, maxHi * Mathf.Sin(omega * seconds) * Time.deltaTime, 0f);
        this.transform.localScale = (1f + minShr * Mathf.Sin(omega * seconds)) * Vector3.one;
    }
}

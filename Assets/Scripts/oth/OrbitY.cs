using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitY : MonoBehaviour
{
    public float orbitRadius = 1f;
    public float orbitSpeed = 0.2f;

    void Start()
    {

    }

    void Update()
    {
        //float circle = 2 * Mathf.PI * orbitRadius;
        float omega = 2 * Mathf.PI * orbitSpeed;
        float fuk = Time.deltaTime * orbitRadius;

        gameObject.transform.Translate(fuk * Mathf.Sin(omega * Time.deltaTime), 0f, fuk * Mathf.Cos(omega * Time.deltaTime));
        gameObject.transform.Rotate(0f, orbitSpeed, 0f);
    }


}

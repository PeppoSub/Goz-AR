using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateX : MonoBehaviour
{
    public float rotationSpeed = 0.5f;

    void Start()
    {
        // gameObject.transform 
    }

    void Update()
    {
        gameObject.transform.Rotate(rotationSpeed, 0f, 0f);
    }

}
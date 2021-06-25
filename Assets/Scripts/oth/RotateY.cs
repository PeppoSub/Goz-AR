using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateY : MonoBehaviour
{
    public float rotationSpeed = 0.5f;

    void Start()
    {
        // gameObject.transform 
    }

    void Update()
    {
        gameObject.transform.Rotate(0f, rotationSpeed, 0f);
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RawImageFlash : MonoBehaviour
{
    public float initTransparency = 0.9f;
    public float dimmPerFrame = 0.05f;

    void Start()
    {
        ResetTransparency();
    }

    void Update()
    {
        var color = this.GetComponent<RawImage>().color; 
        if (color.a > 0)
        {
            color.a -= dimmPerFrame;
            this.GetComponent<RawImage>().color = color;
        }
        else
        {
            gameObject.SetActive(false);
            ResetTransparency();
        }
    }

    void ResetTransparency()
    {
        var color = this.GetComponent<RawImage>().color;
        color.a = initTransparency;
        this.GetComponent<RawImage>().color = color;
    }
}

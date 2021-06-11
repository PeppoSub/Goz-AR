using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashScript : MonoBehaviour
{
    public float dimmPerFrame = 0.05f;
 
    void Start()
    {
        var color = this.GetComponent<Image>().color;
        color.a = 0f;
        this.GetComponent<Image>().color = color;
    }

    void Update()
    {
        var color = this.GetComponent<Image>().color;
        if (color.a > 0)
        {
            color.a -= dimmPerFrame;
            this.GetComponent<Image>().color = color;
        }
        // Debug.Log("color.a = " + color.a.ToString()); 
    }

}

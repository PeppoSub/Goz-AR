using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashScript : MonoBehaviour
{
    public float initTransparency = 0.7f;
    public float dimmPerFrame = 0.035f;
 
    void Start()
    {
        var color = this.GetComponent<Image>().color;
        color.a = initTransparency;
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
        else
        {
            gameObject.SetActive(false);
        }
        // Debug.Log("color.a = " + color.a.ToString()); 
    }

}

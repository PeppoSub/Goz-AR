using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorRotate : MonoBehaviour
{
    public float frequency_r = 1f;        // red color oscillation frequency 
    public float frequency_g = 1f;        // green color oscillation frequency 
    public float frequency_b = 1f;        // blue color oscillation frequency 
    public float freq_alpha = 0f;         // transparency oscillation frequency 
    public float destroyInSeconds = -1f;  // if > 0 deactivate this object after N seconds

    private Color txtcolor;
    private float seconds;

    void Start()
    {
        txtcolor = this.gameObject.GetComponent<Text>().color;
        seconds = 0f;
    }

    void Update()
    {
        //Debug.Log("color = " + txtcolor.ToString());
        //txtcolor.r = txtcolor.r + frequency_r * Time.deltaTime; if (txtcolor.r > 1) { txtcolor.r = txtcolor.r - 1f; }
        //txtcolor.g = txtcolor.r + frequency_g * Time.deltaTime; if (txtcolor.g > 1) { txtcolor.g = txtcolor.g - 1f; }
        //txtcolor.b = txtcolor.r + frequency_b * Time.deltaTime; if (txtcolor.b > 1) { txtcolor.b = txtcolor.b - 1f; }

        seconds += Time.deltaTime;
        if (frequency_r > 0) { txtcolor.r = 0.5f - Mathf.Cos(frequency_r * seconds) / 2f; }
        if (frequency_g > 0) { txtcolor.g = 0.5f - Mathf.Cos(frequency_g * seconds) / 2f; }
        if (frequency_b > 0) { txtcolor.b = 0.5f - Mathf.Cos(frequency_b * seconds) / 2f; }
        if (freq_alpha > 0)
        {
            txtcolor.a = txtcolor.a + freq_alpha * Time.deltaTime; if (txtcolor.a > 1) { txtcolor.a = txtcolor.a - 1f; }
        }
        this.gameObject.GetComponent<Text>().color = txtcolor;

        // deactivate this text (if ... )
        if((destroyInSeconds>0) && (seconds> destroyInSeconds))
        { 
            this.gameObject.SetActive(false); 
        }
    }

}

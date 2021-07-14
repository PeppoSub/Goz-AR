using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ColorRotateTMP : MonoBehaviour
{
    public float frequency_r = 1f;        // red color oscillation frequency 
    public float frequency_g = 1f;        // green color oscillation frequency 
    public float frequency_b = 1f;        // blue color oscillation frequency 
    public float freq_alpha = 0f;         // transparency oscillation frequency 
    public float destroyInSeconds = -1f;  // if > 0 deactivate this object after N seconds

    private Color32 txtcolor;
    private float seconds;

    void Start()
    {
        txtcolor = this.gameObject.GetComponent<TextMeshPro>().color;
        seconds = 0f;
    }

    void Update()
    {
        //Debug.Log("color = " + txtcolor.ToString());

        seconds += Time.deltaTime;
        if (frequency_r > 0) { txtcolor.r = (byte)(int)(255f * (0.5f - Mathf.Cos(frequency_r * seconds) / 2f)); }
        if (frequency_g > 0) { txtcolor.g = (byte)(int)(255f * (0.5f - Mathf.Cos(frequency_g * seconds) / 2f)); }
        if (frequency_b > 0) { txtcolor.b = (byte)(int)(255f * (0.5f - Mathf.Cos(frequency_b * seconds) / 2f)); }
        if (freq_alpha > 0)  { txtcolor.a = (byte)(int)(255f * (0.5f - Mathf.Cos(freq_alpha * seconds) / 2f)); }
        this.gameObject.GetComponent<TextMeshPro>().color = txtcolor;

        // deactivate this text (if ... )
        if ((destroyInSeconds > 0) && (seconds > destroyInSeconds))
        {
            this.gameObject.SetActive(false);
        }
    }

}

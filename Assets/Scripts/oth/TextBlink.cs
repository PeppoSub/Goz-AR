using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.TextCore;

public class TextBlink : MonoBehaviour
{
    public float frequency;

    private float seconds;
    //public GameObject tmxp;
    private TextMeshPro textmeshPro;

    void Start()
    {
        //textmeshPro = tmxp.GetComponent<TextMeshPro>();
        textmeshPro = this.GetComponent<TextMeshPro>();
        textmeshPro.color = new Color32(0, 0, 0, 0);
        seconds = 0f;
    }

    void Update()
    {
        seconds += Time.deltaTime;
        float omega = 2 * Mathf.PI * frequency;

        float oscillate = 255f * Mathf.Sin(omega * seconds);
        int osc = (int)oscillate;
        textmeshPro.color = new Color32((byte)osc, (byte)osc, (byte)osc, 0);

        Debug.Log("TxMP color osc = " + (byte)osc); 
    }
}

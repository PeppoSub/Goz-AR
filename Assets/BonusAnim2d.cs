using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusAnim2d : MonoBehaviour
{
    public GameObject startingSpot;
    public GameObject finalTarget;
    public float speed = 3.3f;
    public float appearRate = 0.11f;
    public float maxAlpha = 0.88f;

    private Vector3 startingPosition;
    private Vector3 finalTargetPosition;
    private Vector3 finalTargetScale;

    void Start()
    {
        startingPosition = startingSpot.GetComponent<Transform>().position;
        finalTargetPosition = finalTarget.GetComponent<Transform>().position;
        finalTargetScale = finalTarget.GetComponent<Transform>().localScale;

        this.gameObject.transform.position = startingPosition;
        this.gameObject.transform.localScale = Vector3.zero;

        var color = this.GetComponent<Image>().color;
        color.a = 0f;
        this.GetComponent<Image>().color = color;
    }

    void Update()
    {
        if(Mathf.Abs(this.gameObject.transform.position.y - finalTargetPosition.y) < 0.01)
        {
            this.gameObject.SetActive(false);
        }
        else
        { 
            this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, finalTargetPosition, speed * Time.deltaTime);
            this.gameObject.transform.localScale = Vector3.Lerp(this.gameObject.transform.localScale, finalTargetScale, speed * Time.deltaTime);

            var color = this.GetComponent<Image>().color;
            if(color.a < maxAlpha)
            {
                color.a += appearRate;
                if (color.a > maxAlpha) { color.a = maxAlpha; }
                this.GetComponent<Image>().color = color;
            }
        }
    }

    void OnEnable()
    {
        Start();
    }
}

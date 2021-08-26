using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveIf : MonoBehaviour
{
    //public GameObject trigger;
    public GameObject target;

    void Start()
    {
        //target.SetActive(true);

        //Debug.Log("trigger = " + trigger.ToString());
        Debug.Log("target = " + target.ToString());
    }

    void Update()
    {
    }

    private void OnEnable()
    {
        target.SetActive(true);
    }

    private void OnDisable()
    {
        target.SetActive(false);
    }

}

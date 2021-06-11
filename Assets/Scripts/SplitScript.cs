using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitScript : MonoBehaviour
{
    public GameObject theChildren;       // objects to spawn when destroyed
    public int nChildren = 2;            // how many of them

    void OnDestroy()
    {
        for (int i = 0; i < nChildren; i++)
        {
            Instantiate(theChildren, this.transform.position, Quaternion.identity);
        }
    }

}

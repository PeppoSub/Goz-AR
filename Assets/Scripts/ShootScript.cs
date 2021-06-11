using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShootScript : MonoBehaviour
{
    public GameObject theArCamera;
    public GameObject theSmoke;

    public void Start()
    {

    }

    public void Shoot()
    {
        RaycastHit theHit;

        if(Physics.Raycast(theArCamera.transform.position, theArCamera.transform.forward, out theHit))
        {
            if(theHit.transform.name.Contains("Slime"))
            {
                Vector3 thePosition = theHit.transform.gameObject.transform.position;
                //Destroy(theHit.transform.gameObject);
                //Instantiate(theSmoke, thePosition, Quaternion.identity);

                //GameStatus.score++;
            }
        }
    }


}

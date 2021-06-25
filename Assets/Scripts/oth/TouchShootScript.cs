using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class TouchShootScript : MonoBehaviour
    {
        public GameObject theArCamera;
        public GameObject theSmoke;

        public void Start()
        {

        }

        public void Update()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(index: 0);

                if (touch.phase == TouchPhase.Began)
                {
                    //Vector3 worldPos = Camera.main.ScreenToWorldPoint(touch.position);
                    //worldPos.z = 0;
                    //if (theRaycastManager.Raycast(worldPos, theHits, ???))

                    // Ray theRay = Camera.main.ScreenPointToRay(touch.position);
                    // if (Physics.Raycast(theRay, out theHit))
                    RaycastHit theHit;
                    if (Physics.Raycast(touch.position, theArCamera.transform.forward, out theHit))
                    {
                        if (theHit.collider != null)
                        {
                            if (theHit.transform.tag == "Enemy")
                            {
                                // destroy object, instantiate smoke, increase score ...
                                Destroy(theHit.transform.gameObject);
                                Instantiate(theSmoke, theHit.point, Quaternion.LookRotation(theHit.normal));
                                GameStatus.score++;
                            }
                        }
                    }
                }
            }
        }

    }
}
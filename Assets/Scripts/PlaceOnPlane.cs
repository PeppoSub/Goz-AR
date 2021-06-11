using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


public class PlaceOnPlane : MonoBehaviour
{
    [SerializeField]
    ARRaycastManager theRaycastManager;

    [SerializeField]
    ARPlaneManager thePlaneManager;

    static List<ARRaycastHit> theHits = new List<ARRaycastHit>();

    public GameObject theObjectToPlace;
    public GameObject theCanvas;

    public GameObject helpStep1;
    public GameObject helpStep2;

    private bool placedTheObject;
    private Canvas thePlayerHud;
    void Start()
    {
        placedTheObject = false;
        thePlayerHud = theCanvas.GetComponent<Canvas>();
        if(thePlayerHud.isActiveAndEnabled)
        {
            placedTheObject = true;
        }
        else
        {
            helpStep1.SetActive(true);
        }
    }

    void Update()
    {
        if (placedTheObject)
        {
            return;
        }
        else
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(index: 0);

                if (touch.phase == TouchPhase.Began)
                {
                    if (theRaycastManager.Raycast(touch.position, theHits, TrackableType.PlaneWithinPolygon))
                    {
                        Pose hitPose = theHits[0].pose;

                        Instantiate(theObjectToPlace, hitPose.position, hitPose.rotation);
                        placedTheObject = true;

                        Instantiate(theCanvas);
                        theCanvas.SetActive(true);
                        Instantiate(thePlayerHud);
                        thePlayerHud.enabled = true;

                        // hide all planes already detected 
                        foreach (var plane in thePlaneManager.trackables)
                        {
                            plane.gameObject.SetActive(false);
                        }
                        thePlaneManager.planePrefab = null;
                        // Destroy(thePlaneManager);   // should not destroy this, as we may need it again ... 

                        // disable 1st help message and display 2nd one (this will disappear aften N seconds)
                        helpStep1.SetActive(false);
                        helpStep2.SetActive(true);
                    }
                }
            }
        }

    }

}

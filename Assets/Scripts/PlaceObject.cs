// this script includes various unsuccessfull attempts to display a virtual cursor to place the spawning point
// see, for instance:
//  https://www.youtube.com/watch?v=KqzlGApWPEA
//  https://www.youtube.com/watch?v=R3OCUE9TwZk
//  https://www.youtube.com/watch?v=Ml2UakwRxjk

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceObject : MonoBehaviour
{
    //[SerializeField]
    //ARPlaneManager thePlaneManager;

    public GameObject theObjectToPlace;
    public GameObject thePlacementIndicator;

    //public ARRaycastManager theRaycastManager;
    private ARRaycastManager theRaycastManager;

    private GameObject theSpawnedObject;
    private Pose thePlacementPose;
    private bool placementPoseIsValid = false;

    //public GameObject theCanvas;
    //static List<ARRaycastHit> theHits = new List<ARRaycastHit>();
    //static private bool placedTheObject ;
    //private Canvas thePlayerHud;
    //public GameObject theCamera;

    void Start()
    {
        theRaycastManager = FindObjectOfType<ARRaycastManager>();

        //thePlacementIndicator.SetActive(false); 
        //placedTheObject = false;
        //thePlayerHud = theCanvas.GetComponent<Canvas>();
    }

    void Update()
    {
        if (theSpawnedObject == null && placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            PlaceTheObject();
        }

        UpdatePlacementPose();
        UpdatePlacementIndicator();
    }

    void UpdatePlacementIndicator()
    {
        if (theSpawnedObject == null && placementPoseIsValid)
        {
            thePlacementIndicator.SetActive(true);
            thePlacementIndicator.transform.SetPositionAndRotation(thePlacementPose.position, thePlacementPose.rotation);
        }
        else
        {
            thePlacementIndicator.SetActive(false);
        }
    }

    void UpdatePlacementPose()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var theHits = new List<ARRaycastHit>();
        theRaycastManager.Raycast(screenCenter, theHits, TrackableType.Planes);

        placementPoseIsValid = theHits.Count > 0;
        if (placementPoseIsValid)
        {
            thePlacementPose = theHits[0].pose;
        }

        //if (theRaycastManager.Raycast(theCamera.transform.position, theHits, TrackableType.PlaneWithinPolygon))
        //{
        //    Pose hitPose = theHits[0].pose;
        //    thePlacementIndicator.transform.position = theHits[0].pose.position;
        //    thePlacementIndicator.transform.rotation = theHits[0].pose.rotation;
        //    thePlacementIndicator.SetActive(true);
        //}

    }

    void PlaceTheObject()
    {
        theSpawnedObject = Instantiate(theObjectToPlace, thePlacementPose.position, thePlacementPose.rotation);

        //placedTheObject = true;
        //thePlacementIndicator.SetActive(false);

        //Instantiate(theCanvas);
        //theCanvas.SetActive(true);
        //Instantiate(thePlayerHud);
        //thePlayerHud.enabled = true;
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;
using UnityEngine.SceneManagement;

public class PlaceObjWHelper : MonoBehaviour
{
    public ARRaycastManager theRaycastManager;

    [SerializeField]
    ARPlaneManager thePlaneManager;

    static List<ARRaycastHit> theHits;

    //public GameObject theCamera;                           // the AR camera
    public GameObject thePlacementIndicatorPrefab;         // placement indicator
    public GameObject theCanvas;                           // player HUD
    public GameObject[] theObjectsToPlace;                 // spawners to place [for each mission] 
    public GameObject helpStep0;                           // help message 
    public GameObject helpStep1;                           // ... make a single help txt field and remove these 2 
    public GameObject helpStep2;

    private int spawnerSelected;                           // this should be the mission number. see GameStatus
    private Vector2 screenPosition;
    private GameObject thePlacementIndicator;
    private Pose thePlacementPose;
    private bool placementPoseIsValid;
    private GameObject thePlacedObject;
    private bool placedTheObject;
    private Canvas thePlayerHud;

    void Start()
    {
        placedTheObject = false;
        thePlayerHud = theCanvas.GetComponent<Canvas>();
#if !UNITY_EDITOR
        theCanvas.SetActive(false);
#endif
        helpStep0.SetActive(true); 

        placementPoseIsValid = false;
        thePlacementIndicator = GameObject.Instantiate(thePlacementIndicatorPrefab, this.transform.position, this.transform.rotation);
        thePlacementIndicator.SetActive(false);

        // changed according to single scene (see "GameStatus.cs")
        // for multiple cene use: spawnerSelected = SceneManager.GetActiveScene().buildIndex - 1;   // !!!
        // ... or just add a single object into the array
        if(theObjectsToPlace.Length>1)
        {
            spawnerSelected = PlayerPrefs.GetInt("currentMission") - 1;
        }
        else { spawnerSelected = 0; }
    }

    void Update()
    {
        if (!placedTheObject) { UpdateCursor(); }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(index: 0);
            if (touch.phase == TouchPhase.Began) { PlaceTheObject(); }
        }
    }
           
    void UpdateCursor()
    {
        theHits = new List<ARRaycastHit>();

        screenPosition = new Vector2(Screen.width / 2f, Screen.height / 2f);
        if (theRaycastManager.Raycast(screenPosition, theHits, TrackableType.PlaneWithinPolygon))   // screenPosition
        {
            placementPoseIsValid = true;
            thePlacementPose = theHits[0].pose;

            thePlacementIndicator.transform.SetPositionAndRotation(thePlacementPose.position, thePlacementPose.rotation);
            thePlacementIndicator.SetActive(true);

            helpStep0.SetActive(false);
            helpStep1.SetActive(true);
        }
        else
        {
            placementPoseIsValid = false;
            if (thePlacementIndicator != null) { thePlacementIndicator.SetActive(false); }

            helpStep0.SetActive(true);
            helpStep1.SetActive(false);
        }

    }
    public void PlaceTheObject()
    {
        if (!placedTheObject && placementPoseIsValid)
        {
            // theObjectsToPlace[spawnerSelected]
            thePlacedObject = GameObject.Instantiate(theObjectsToPlace[spawnerSelected], thePlacementIndicator.transform.position, thePlacementIndicator.transform.rotation);
            placedTheObject = true;
            InitGame();
        }
    }

    void InitGame()
    {
        Instantiate(theCanvas);
        theCanvas.SetActive(true);
        Instantiate(thePlayerHud);
        thePlayerHud.enabled = true;
        GameStatus.groundLevel = thePlacedObject.transform.position.y;

        // hide all planes already detected 
        foreach (var plane in thePlaneManager.trackables)
        {
            plane.gameObject.SetActive(false);
        }
        thePlaneManager.planePrefab = null;

        // destroy the placement indicator
        Destroy(thePlacementIndicator);

        // disable 1st help message and display 2nd one (this will disappear aften N seconds)
        helpStep0.SetActive(false);
        helpStep1.SetActive(false);
        helpStep2.SetActive(true);
        StartCoroutine(DeactivateInSeconds(3));
    }

    IEnumerator DeactivateInSeconds(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        helpStep2.SetActive(false);
    }

}

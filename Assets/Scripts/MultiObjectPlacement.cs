using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;

public class MultiObjectPlacement : MonoBehaviour
{
    public ARRaycastManager theRaycastManager;

    [SerializeField]
    ARPlaneManager thePlaneManager;

    static List<ARRaycastHit> theHits; // = new List<ARRaycastHit>();

    public GameObject theCamera;
    public GameObject theTrail;
    public GameObject thePlacementIndicatorPrefab;
    public GameObject[] theObjectsToPlace;
    public GameObject theCanvas;
    public GameObject theOtherCanvas;
    public GameObject helpStep1;
    public GameObject helpStep2;
    public TextMeshProUGUI prefabText;
    public TextMeshProUGUI planesText;
    public TextMeshProUGUI poseText;
    public TextMeshProUGUI touchText;
    public TextMeshProUGUI camText;
    public TextMeshProUGUI scrText;

    //private GameObject theTrail;
    private Vector2 screenPosition;
    private GameObject thePlacementIndicator;
    private Pose thePlacementPose;
    private bool placementPoseIsValid = false;
    private GameObject thePlacedObject;
    private bool placedTheObject;
    private Canvas thePlayerHud;
    static private int selected = 0;
    static private int nPlanes = 0;

    void Start()
    {
        placedTheObject = false;
        thePlayerHud = theCanvas.GetComponent<Canvas>();
        if (thePlayerHud.isActiveAndEnabled)
        {
            placedTheObject = true;
        }
        else
        {
            helpStep1.SetActive(true);
        }

        placementPoseIsValid = false;
        thePlacementIndicator = GameObject.Instantiate(thePlacementIndicatorPrefab, this.transform.position, this.transform.rotation);
        thePlacementIndicator.SetActive(false);
    }

    void Update()
    {
        if (!placedTheObject) { UpdateCursor(); }

        nPlanes = thePlaneManager.trackables.count;

        string textbuffer = "Prefab: " + selected.ToString() + theObjectsToPlace[selected].name; ;
        prefabText.text = textbuffer;
        textbuffer = "Detected Planes: " + nPlanes.ToString();
        planesText.text = textbuffer;
        textbuffer = "Camera Pose: " + theCamera.transform.position.ToString() + " , " + theCamera.transform.rotation.ToString(); ;
        camText.text = textbuffer;
        textbuffer = "Screen Pose: " + screenPosition.ToString();
        scrText.text = textbuffer;

        textbuffer = "Touch Pose: ";
        if (Input.touchCount > 0) { textbuffer += Input.GetTouch(0).position.ToString(); }
        else { textbuffer = " no touch "; }
        touchText.text = textbuffer;

        textbuffer = "Placemet Pose: " ;
        if (placementPoseIsValid) { textbuffer += thePlacementPose.position.ToString() + " , " + thePlacementPose.rotation.ToString(); }
        else { textbuffer += " not valid! "; }
        poseText.text = textbuffer;
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

            // enable this to leave a trail on the indicator position
            GameObject.Instantiate(theTrail, thePlacementPose.position, thePlacementPose.rotation);
        }
        else
        {
            placementPoseIsValid = false;
            if (thePlacementIndicator != null) { thePlacementIndicator.SetActive(false); }
        }

    }
    public void PlaceTheObject()
    {
        if (!placedTheObject && placementPoseIsValid)
        {
            thePlacedObject = GameObject.Instantiate(theObjectsToPlace[selected], thePlacementIndicator.transform.position, thePlacementIndicator.transform.rotation);
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
        // Destroy(thePlaneManager);   // should not destroy this, as we may need it again ... 

        // destroy the placement indicator
        Destroy(thePlacementIndicator);

        // disable 1st help message and display 2nd one (this will disappear aften N seconds)
        helpStep1.SetActive(false);
        helpStep2.SetActive(true);

        theOtherCanvas.SetActive(false);

    }

    public void ChangeSelect()
    {
        int nObj = theObjectsToPlace.Length;
        selected = (selected + 1) % nObj;
    }

}

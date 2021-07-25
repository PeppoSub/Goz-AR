using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LandPort : MonoBehaviour
{
    // to move the camera between portrait and landscape mode (and fit the menu in the screen)
    public float cameraSpeed = 3;
    public GameObject portraitCamera;
    public float viewAngle = 0f;
    public TextMeshProUGUI theTargetText;
    public string portraitMessage1 = "It plays better in Pandscape ...";
    public string portraitMessage2 = "... but Portrait gives you more";
    //public string landscapeMessage = "";  // use originalText instead

    private Vector3 defaultCameraPos;
    private Vector3 portraitCameraPos;
    private Quaternion defaultCameraRot;
    private Quaternion portraitCameraRot;
    private string originalText;
    private string portraitMessage;
    //private bool portrait;

    void Start()
    {
        if(portraitCamera == null) { return; }

        defaultCameraPos = this.gameObject.transform.position;
        portraitCameraPos = portraitCamera.transform.position;
        defaultCameraRot = this.gameObject.transform.rotation;
        portraitCameraRot = portraitCamera.transform.rotation;

        originalText = theTargetText.text;

        int level3 = PlayerPrefs.GetInt("level3",0);   // change the displayed message when finished game
        if (level3 > 0) { portraitMessage = portraitMessage2; }
        else            { portraitMessage = portraitMessage1; }
    }

    void Update()
    {
        if (viewAngle > 0) { this.gameObject.GetComponent<Camera>().fieldOfView = viewAngle; }

        if (portraitCamera == null) { return; }

        if (Screen.orientation == ScreenOrientation.Portrait || Screen.orientation == ScreenOrientation.PortraitUpsideDown)
        {
            this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, portraitCameraPos, cameraSpeed* Time.deltaTime);
            this.gameObject.transform.rotation = Quaternion.Lerp(this.gameObject.transform.rotation, portraitCameraRot, cameraSpeed * Time.deltaTime);
            if ((theTargetText != null) && (portraitMessage.Length > 0)) { theTargetText.text = portraitMessage; }
        }
        else 
        {
            this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, defaultCameraPos, cameraSpeed * Time.deltaTime);
            this.gameObject.transform.rotation = Quaternion.Lerp(this.gameObject.transform.rotation, defaultCameraRot, cameraSpeed * Time.deltaTime);
            if (theTargetText != null) { theTargetText.text = originalText; }
        }
        // Debug.Log("Screen.orientation = " + Screen.orientation);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LandPort : MonoBehaviour
{
    // to move the camera between portrait and landscape mode (and fit the menu in the screen)
    public float cameraSpeed = 3;
    public GameObject portraitCamera;
    //public TextMeshProUGUI theTargetText;

    private Vector3 defaultCameraPos;
    private Vector3 portraitCameraPos;
    private Quaternion defaultCameraRot;
    private Quaternion portraitCameraRot;
    private float defaultFieldOfView;
    private float portraitFieldOfView;
    //private bool portrait;

    void Start()
    {
        defaultCameraPos = this.gameObject.transform.position;
        portraitCameraPos = portraitCamera.transform.position;
        defaultCameraRot = this.gameObject.transform.rotation;
        portraitCameraRot = portraitCamera.transform.rotation;
        defaultFieldOfView = this.gameObject.GetComponent<Camera>().fieldOfView;

        //portrait = false; 
    }

    void Update()
    {
        if(Screen.orientation == ScreenOrientation.Portrait || Screen.orientation == ScreenOrientation.PortraitUpsideDown)
        {
            this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, portraitCameraPos, cameraSpeed* Time.deltaTime);
            this.gameObject.transform.rotation = Quaternion.Lerp(this.gameObject.transform.rotation, portraitCameraRot, cameraSpeed * Time.deltaTime);
            //this.gameObject.GetComponent<Camera>().fieldOfView = portraitFieldOfView;
            //theTargetText.text = "Portrait";
            //portrait = true;
        }
        else 
        {
            this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, defaultCameraPos, cameraSpeed * Time.deltaTime);
            this.gameObject.transform.rotation = Quaternion.Lerp(this.gameObject.transform.rotation, defaultCameraRot, cameraSpeed * Time.deltaTime);
            //this.gameObject.GetComponent<Camera>().fieldOfView = defaultFieldOfView;
            //theTargetText.text = "LandScape";
        }
        // Debug.Log("Screen.orientation = " + Screen.orientation);
    }
}

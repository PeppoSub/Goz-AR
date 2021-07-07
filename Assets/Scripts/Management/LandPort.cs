using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LandPort : MonoBehaviour
{
    // to move the camera between portrait and landscape mode (and fit the menu in the screen)
    public float cameraSpeed = 3;
    //public TextMeshProUGUI theTargetText;

    private Vector3 defaultCameraPos;
    private Vector3 portraitCameraPos;
    private bool portrait;

    void Start()
    {
        defaultCameraPos = this.gameObject.transform.position;
        portraitCameraPos = new Vector3(defaultCameraPos.x + 0.1f, defaultCameraPos.y + 2.2f, defaultCameraPos.z - 7.2f);

        portrait = false; 
    }

    void Update()
    {
        if(Screen.orientation == ScreenOrientation.Portrait || Screen.orientation == ScreenOrientation.PortraitUpsideDown)
        {
            this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, portraitCameraPos, cameraSpeed* Time.deltaTime);
            //theTargetText.text = "Portrait";
            portrait = true;
        }
        else 
        {
            this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, defaultCameraPos, cameraSpeed * Time.deltaTime);
            //theTargetText.text = "LandScape";
        }
        // Debug.Log("Screen.orientation = " + Screen.orientation);
    }
}

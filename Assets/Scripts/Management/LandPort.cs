using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LandPort : MonoBehaviour
{
    // to move the camera between portrait and landscape mode (and fit the menu in the screen)
    public float cameraSpeed = 3;
    public TextMeshProUGUI theTargetText;

    private Vector3 defaultCameraPos;
    private Vector3 portraitCameraPos;
    private bool portrait;

    void Start()
    {
        defaultCameraPos = this.gameObject.transform.position;
        portraitCameraPos = new Vector3(defaultCameraPos.x, defaultCameraPos.y, defaultCameraPos.z - 7f);

        portrait = false; 
    }

    // Update is called once per frame
    void Update()
    {
        if(Screen.orientation == ScreenOrientation.Portrait || Screen.orientation == ScreenOrientation.PortraitUpsideDown)
        {
            //if(portrait) { return; }
            this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, portraitCameraPos, cameraSpeed* Time.deltaTime);
            //this.gameObject.transform.position = portraitCameraPos;
            theTargetText.text = "Portrait";
            portrait = true;
        }
        else 
        {
            //if (!portrait) { return; }
            this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, defaultCameraPos, cameraSpeed * Time.deltaTime);
            //this.gameObject.transform.position = defaultCameraPos;
            theTargetText.text = "LandScape";
        }
        // Debug.Log("Screen.orientation = " + Screen.orientation);

    }
}

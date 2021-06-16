using UnityEngine;

public class LoadSceneItem : MonoBehaviour
{
    private LoadSceneButton loadButton;
    public Camera theCamera;

    // Start is called before the first frame update
    void Start()
    {
        loadButton = GetComponent<LoadSceneButton>();
    }

    void Update()
    {
        if (Input.touchCount <= 0) return;
        Touch touch = Input.GetTouch(index: 0);

        if (touch.phase != TouchPhase.Ended) return;

        Ray theRay = theCamera.ScreenPointToRay(touch.position);

        // // LayerMask: 8 -> MissionPin
        // LayerMask mask = LayerMask.GetMask("MissionPin");

        RaycastHit theHit;
        bool didHit = Physics.Raycast(theRay, out theHit, 50.0f);  //, mask
        if (!didHit) return;

        loadButton = theHit.collider.gameObject.GetComponent<LoadSceneButton>();
        if (loadButton == null) return;

        loadButton.click();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeAsText : MonoBehaviour
{
    public GameObject textObject;

    void Start()
    {
        // https://gamedev.stackexchange.com/questions/134249/how-to-match-ui-background-size-with-text-length-in-unity
        //RectTransform uiText = textObject.GetComponent<RectTransform>();
        //RectTransform uiImage = imageObject.GetComponent<RectTransform>();

        RectTransform uiText = textObject.GetComponent<RectTransform>();
        RectTransform uiImage = this.GetComponent<RectTransform>();

        uiImage.sizeDelta = uiText.sizeDelta;

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxScript : MonoBehaviour
{
	public Material[] skyBoxes;
	private int current = 0;

	void Start()
	{
		current = PlayerPrefs.GetInt("skyBox", -1);

		if(current < 0) { current = 0; }
		else { ChangeSky(); }

		PlayerPrefs.SetInt("skyBox", current);
		PlayerPrefs.Save();
	}

	public void ChangeSky()
	{
		current = (current + 1) % skyBoxes.Length;
		RenderSettings.skybox = skyBoxes[current];
	}

}
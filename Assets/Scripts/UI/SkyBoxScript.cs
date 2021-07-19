using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxScript : MonoBehaviour
{
	public Material[] skyBoxes;
	public static bool isStarted = false;
	public static int current = 0;

	void Start()
	{
		current = PlayerPrefs.GetInt("skyBox", -1);
		if (!isStarted) { ChangeSkyOnce(); }
		else { SetCurrentSky(); }
	}

	private void ChangeSkyOnce()
	{
		if (current >= 0)
		{
			current = (current + 1) % skyBoxes.Length;
			RenderSettings.skybox = skyBoxes[current];
		}
		else { current = 0; }

		PlayerPrefs.SetInt("skyBox", current);
		PlayerPrefs.Save();

		isStarted = true;
	}

	public void ChangeSkyAnyway()
    {
		current = (current + 1) % skyBoxes.Length;
		RenderSettings.skybox = skyBoxes[current];
	}

	public void SetCurrentSky()
	{
		RenderSettings.skybox = skyBoxes[current];
	}

}
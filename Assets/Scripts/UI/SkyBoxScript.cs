using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxScript : MonoBehaviour
{
	public Material[] skyBoxes;
	private int current = 0;

	void Start()
	{

		//RenderSettings.skybox = skyTwo;

	}

	void Update()
	{

	}

	public void ChangeSky()
	{
		//int n = 
		current = (current + 1) % skyBoxes.Length;
		RenderSettings.skybox = skyBoxes[current];
	}

}
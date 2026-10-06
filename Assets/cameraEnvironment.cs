using System;
using UnityEngine;
using UnityEngine.Splines.Interpolators;

public class CameraEnvironment : MonoBehaviour
{
	private Camera camera;
	private Color targetBackgroundColor = Color.darkGreen;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		camera = GetComponent<Camera>();
	}

	// Update is called once per frame
	void Update()
	{
		switch (GameManager.Instance.season)
		{
			case GameManager.Season.Spring:
				targetBackgroundColor = Color.darkGreen;
				break;
			case GameManager.Season.Summer:
				targetBackgroundColor = Color.deepPink;
				break;
			case GameManager.Season.Autumn:
				targetBackgroundColor = Color.darkGoldenRod;
				break;
			case GameManager.Season.Winter:
				targetBackgroundColor = Color.darkBlue;
				break;
		}
		camera.backgroundColor = Color.Lerp(camera.backgroundColor, targetBackgroundColor, 0.01f);
	}
}

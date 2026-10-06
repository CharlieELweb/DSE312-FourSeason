using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines.Interpolators;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }
	public Season season = Season.Summer;
	float targetTimeScale = 1.0f;
	private void Awake()
	{
		Instance = this;
	}

	public enum Season
	{
		Spring,
		Summer,
		Autumn,
		Winter,
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			targetTimeScale = 0.3f;
		}
		if (Input.GetKey(KeyCode.LeftShift))
		{
			if (Input.GetKeyDown(KeyCode.UpArrow))
			{
				ChangeToSeason(Season.Spring);
			}
			else if (Input.GetKeyDown(KeyCode.RightArrow))
			{
				ChangeToSeason(Season.Summer);
			}
			else if (Input.GetKeyDown(KeyCode.DownArrow))
			{
				ChangeToSeason(Season.Autumn);
			}
			else if (Input.GetKeyDown(KeyCode.LeftArrow))
			{
				ChangeToSeason(Season.Winter);
			}
		}

		if (Input.GetKeyUp(KeyCode.LeftShift))
		{
			targetTimeScale = 1f;

		}
		Time.timeScale = Mathf.Lerp(Time.timeScale, targetTimeScale, 0.01f);
		Time.fixedDeltaTime = 0.02f * Time.timeScale;

	}

	public void ChangeToSeason(Season newSeason)
	{
		season = newSeason;
	}
}

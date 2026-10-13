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
		if (InputManager.IsChangePressed)
		{
			targetTimeScale = 0.3f;
			if (InputManager.season.y > 0)
			{
				ChangeToSeason(Season.Spring);
			}
			else if (InputManager.season.x > 0)
			{
				ChangeToSeason(Season.Summer);
			}
			else if (InputManager.season.y < 0)
			{
				ChangeToSeason(Season.Autumn);
			}
			else if (InputManager.season.x < 0)
			{
				ChangeToSeason(Season.Winter);
			}
		}
		else
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

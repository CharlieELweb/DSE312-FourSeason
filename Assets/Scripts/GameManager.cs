using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines.Interpolators;
using System.Collections.Generic;
using Abertay.Analytics;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
	public bool isTransition = false;
	public bool pause = false;
	public bool showSeasonGroup = false;

	public Player player;
	public static event Action<Season, Season> OnSeasonChanged;
	public static GameManager Instance { get; private set; }
	public TextHint textHint;
	public TextHint textCenter;
	public Season season = Season.Spring;
	public Animator transition;

	public bool[] canSwap = { true, false, false, true };
	float targetTimeScale = 1.0f;

	public void ChangeToScene(String sceneName, Vector3 playerPosition, float velocityX = 0)
	{
		StartCoroutine(TransitionTo(sceneName, playerPosition, velocityX));
	}

	IEnumerator TransitionTo(String sceneName, Vector3 playerPosition, float velocityX = 0)
	{
		isTransition = true;

		transition.SetTrigger("Start");
		yield return new WaitForSeconds(1f);
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
		yield return asyncLoad.isDone;
		yield return new WaitForSeconds(0.01f);
		player.transform.position = playerPosition;
		transition.SetTrigger("End");

		player.GetComponent<Rigidbody2D>().linearVelocityX = velocityX;
		isTransition = true;

		yield return new WaitForSeconds(0.5f);
		player.GetComponent<Rigidbody2D>().linearVelocityX = 0;

		isTransition = false;
	}
	private void Awake()
	{
		if (Instance != null)
		{
			Instance.player = player;
			Instance.textHint = textHint;
			Instance.textCenter = textCenter;
			Destroy(gameObject);
			return;
		}
		DontDestroyOnLoad(gameObject);
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
		if (pause)
		{
			targetTimeScale = 1f;
		}
		else if (InputManager.IsChangePressed || InputManager.season.magnitude > 0.1f)
		{
			if (player)
			{
				player.GetComponent<SortingGroup>().sortingOrder = 1000;
			}
			targetTimeScale = 0.1f;
			if (InputManager.season.y > 0.5)
			{
				if (canSwap[0])
				{
					ChangeToSeason(Season.Spring);
				}

			}
			else if (InputManager.season.x > 0.5)
			{
				if (canSwap[1])
				{
					ChangeToSeason(Season.Summer);
				}
			}
			else if (InputManager.season.y < -0.5)
			{
				if (canSwap[2])
				{
					ChangeToSeason(Season.Autumn);
				}
			}
			else if (InputManager.season.x < -0.5)
			{
				if (canSwap[3])
				{
					ChangeToSeason(Season.Winter);
				}
			}
		}
		else
		{
			if (player)
			{
				player.GetComponent<SortingGroup>().sortingOrder = 0;
			}
			targetTimeScale = 1f;
		}

		Time.timeScale = Mathf.Lerp(Time.timeScale, targetTimeScale, 0.01f);
		Time.fixedDeltaTime = 0.02f * Time.timeScale;

	}

	public void ChangeToSeason(Season newSeason)
	{
		if (pause)
		{
			return;
		}
		if (season == newSeason)
		{
			return;
		}
		OnSeasonChanged?.Invoke(season, newSeason);
		season = newSeason;
		Vector3 pos = GameObject.FindGameObjectsWithTag("Player")[0].transform.position;
		Dictionary<string, object> eventData = new Dictionary<string, object>
		{
			{"Position", pos},
			{"Time", Time.time},
			{"Season", newSeason},
		};
		AnalyticsManager.SendCustomEvent("ChangeSeason", eventData);

		switch (newSeason)
		{
			case Season.Spring:
				AnalyticsManager.LogHeatmapEvent("ChangeSeason", pos, Color.green);
				break;
			case Season.Summer:
				AnalyticsManager.LogHeatmapEvent("ChangeSeason", pos, Color.red);
				break;
			case Season.Autumn:
				AnalyticsManager.LogHeatmapEvent("ChangeSeason", pos, Color.orange);

				break;
			case Season.Winter:
				AnalyticsManager.LogHeatmapEvent("ChangeSeason", pos, Color.black);
				break;
		}

	}

}

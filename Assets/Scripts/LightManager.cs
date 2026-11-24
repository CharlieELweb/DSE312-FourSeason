using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightManager : MonoBehaviour
{
	[SerializeField] float spring, summer, autumn, winter;
	Light2D light2D;
	float targetIntensity;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		light2D = GetComponent<Light2D>();
		OnSeasonChanged(GameManager.Season.Spring, GameManager.Instance.season);
		light2D.intensity = targetIntensity;
	}
	private void OnEnable()
	{
		GameManager.OnSeasonChanged += OnSeasonChanged;
	}

	private void OnDisable()
	{
		GameManager.OnSeasonChanged -= OnSeasonChanged;
	}
	private void OnSeasonChanged(GameManager.Season oldSeason, GameManager.Season newSeason)
	{
		switch (newSeason)
		{
			case GameManager.Season.Spring:
				targetIntensity = spring;
				break;
			case GameManager.Season.Summer:
				targetIntensity = summer;
				break;
			case GameManager.Season.Autumn:
				targetIntensity = autumn;
				break;
			case GameManager.Season.Winter:
				targetIntensity = winter;
				break;
		}
	}

	// Update is called once per frame
	void Update()
	{
		light2D.intensity = Mathf.MoveTowards(light2D.intensity, targetIntensity, Time.deltaTime);
	}
}

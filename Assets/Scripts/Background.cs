using System;
using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour
{


	[Serializable]
	private struct Gradient
	{
		public Color top, middle, bottom;
	}

	[SerializeField]
	private Gradient spring, summer, autumn, winter;
	private Color top, middle, bottom;
	private Color targetTop, targetMiddle, targetBottom;
	[SerializeField] private Material material;
	private void Start()
	{
		OnSeasonChanged(GameManager.Season.Spring, GameManager.Instance.season);
		top = targetTop;
		middle = targetMiddle;
		bottom = targetBottom;
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
				targetTop = spring.top;
				targetMiddle = spring.middle;
				targetBottom = spring.bottom;
				break;
			case GameManager.Season.Summer:
				targetTop = summer.top;
				targetMiddle = summer.middle;
				targetBottom = summer.bottom;
				break;
			case GameManager.Season.Autumn:
				targetTop = autumn.top;
				targetMiddle = autumn.middle;
				targetBottom = autumn.bottom;
				break;
			case GameManager.Season.Winter:
				targetTop = winter.top;
				targetMiddle = winter.middle;
				targetBottom = winter.bottom;
				break;
		}
	}
	void Update()
	{
		material.SetColor(Shader.PropertyToID("_ColorTop"), top);
		material.SetColor(Shader.PropertyToID("_ColorMiddle"), middle);
		material.SetColor(Shader.PropertyToID("_ColorBottom"), bottom);
		top = Color.Lerp(top, targetTop, 5 * Time.deltaTime);
		middle = Color.Lerp(middle, targetMiddle, 5 * Time.deltaTime);
		bottom = Color.Lerp(bottom, targetBottom, 5 * Time.deltaTime);
	}
}

using System;
using UnityEngine;

public class ParticleVisual : MonoBehaviour
{
	[SerializeField] private bool spring, summer, autumn, winter;
	private bool show = false;
	private ParticleSystem particleSystem;
	private Material particleMaterial;
	private void OnEnable()
	{
		GameManager.OnSeasonChanged += OnSeasonChanged;
	}

	private void OnDisable()
	{
		GameManager.OnSeasonChanged += OnSeasonChanged;
	}
	private void Awake()
	{
		particleSystem = GetComponent<ParticleSystem>();
		particleMaterial = particleSystem.GetComponent<ParticleSystemRenderer>().material;
	}

	private void Update()
	{
		particleMaterial.color = Color.Lerp(particleMaterial.color, new Color(1, 1, 1, show ? 1 : 0), 0.005f);

	}

	private void OnSeasonChanged(GameManager.Season oldSeason, GameManager.Season newSeason)
	{
		switch (newSeason)
		{
			case GameManager.Season.Spring:
				show = spring;
				break;
			case GameManager.Season.Summer:
				show = summer;
				break;
			case GameManager.Season.Autumn:
				show = autumn;
				break;
			case GameManager.Season.Winter:
				show = winter;
				break;
		}
	}
}
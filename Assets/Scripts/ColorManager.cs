using Mono.Cecil.Cil;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ColorManager : MonoBehaviour
{
	private Tilemap tilemap;
	private Camera camera;
	private SpriteRenderer spriteRenderer;
	[SerializeField] private Color spring = Color.white, summer = Color.white, autumn = Color.white, winter = Color.white;
	Color targetColor = Color.white;
	private void Awake()
	{
		tilemap = GetComponent<Tilemap>();
		camera = GetComponent<Camera>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		OnSeasonChanged(GameManager.Season.Spring, GameManager.Instance.season);
		if (tilemap) tilemap.color = targetColor;
		if (camera) camera.backgroundColor = targetColor;
		if (spriteRenderer) spriteRenderer.color = targetColor;


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
				targetColor = spring;
				break;
			case GameManager.Season.Summer:
				targetColor = summer;
				break;
			case GameManager.Season.Autumn:
				targetColor = autumn;
				break;
			case GameManager.Season.Winter:
				targetColor = winter;
				break;
		}
	}
	private void Update()
	{
		if (tilemap) tilemap.color = Color.Lerp(tilemap.color, targetColor, 5 * Time.deltaTime);
		if (camera) camera.backgroundColor = Color.Lerp(camera.backgroundColor, targetColor, 5 * Time.deltaTime);
		if (spriteRenderer) spriteRenderer.color = Color.Lerp(spriteRenderer.color, targetColor, 5 * Time.deltaTime);
	}

}

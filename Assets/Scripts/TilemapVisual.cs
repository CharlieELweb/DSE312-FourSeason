using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapVisual : MonoBehaviour
{
	[SerializeField] private RuleTile defaultTile;
	[SerializeField] private RuleOverrideTile springTile;
	[SerializeField] private RuleOverrideTile winterTile;
	private Tilemap tilemap;
	private void Awake()
	{
		tilemap = GetComponent<Tilemap>();

	}
	private void OnEnable()
	{
		GameManager.OnSeasonChanged += OnSeasonChanged;
	}

	private void OnDisable()
	{
		GameManager.OnSeasonChanged += OnSeasonChanged;
	}

	private void OnSeasonChanged(GameManager.Season oldSeason, GameManager.Season newSeason)
	{
		TileBase changeTile = null, newTile = null;
		switch (oldSeason)
		{
			case GameManager.Season.Spring:
				changeTile = defaultTile;
				break;
			case GameManager.Season.Summer:
				break;
			case GameManager.Season.Autumn:
				break;
			case GameManager.Season.Winter:
				changeTile = winterTile;
				break;
		}
		switch (newSeason)
		{
			case GameManager.Season.Spring:
				newTile = defaultTile;
				break;
			case GameManager.Season.Summer:
				break;
			case GameManager.Season.Autumn:
				break;
			case GameManager.Season.Winter:
				newTile = winterTile;
				break;
		}
		if (changeTile == null || newTile == null || changeTile == newTile)
		{
			return;
		}
		tilemap.SwapTile(changeTile, newTile);
	}

}

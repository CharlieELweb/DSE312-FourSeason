using System;
using UnityEngine;

public class Background : MonoBehaviour
{


	[Serializable]
	private struct BackgorundSprite
	{
		public Sprite spring;
		public Sprite summer;
		public Sprite autumn;
		public Sprite winter;
	}

	[SerializeField]
	private BackgorundSprite backgorundSprite;
	private SpriteRenderer spriteRenderer;

	void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	void Update()
	{
		switch (GameManager.Instance.season)
		{
			case GameManager.Season.Spring:
				spriteRenderer.sprite = backgorundSprite.spring;
				break;
			case GameManager.Season.Summer:
				spriteRenderer.sprite = backgorundSprite.summer;
				break;
			case GameManager.Season.Autumn:
				spriteRenderer.sprite = backgorundSprite.autumn;
				break;
			case GameManager.Season.Winter:
				spriteRenderer.sprite = backgorundSprite.winter;
				break;
		}
	}
}

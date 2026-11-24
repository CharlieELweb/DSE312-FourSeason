using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor;

public class SeasonPower : MonoBehaviour
{
	private bool hide = false;
	private bool showHint = false;
	private SpriteRenderer spriteRenderer;

	[SerializeField] private GameManager.Season season;

	private float targetAlpha = 1f;
	private bool collected = false;

	void Awake()
	{
		spriteRenderer = GetComponentInChildren<SpriteRenderer>();
	}


	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collected)
		{
			return;
		}
		if (collision.gameObject.CompareTag("Player"))
		{
			collected = true;
			StartCoroutine(ShowHint());
		}
	}
	IEnumerator ShowHint()
	{
		string seasonText = "";
		switch (season)
		{
			case GameManager.Season.Spring:
				seasonText = "Spring";
				break;
			case GameManager.Season.Summer:
				seasonText = "Summer";
				break;
			case GameManager.Season.Autumn:
				seasonText = "Autumn";
				break;
			case GameManager.Season.Winter:
				seasonText = "Winter";
				break;
		}
		GameManager.Instance.pause = true;
		GameManager.Instance.textHint.ShowInSecond("You collected the [Power of " + seasonText + "]", 5);
		GameManager.Instance.showSeasonGroup = true;

		yield return new WaitForSeconds(1.5f);
		GameManager.Instance.canSwap[(int)season] = true;
		hide = true;
		yield return new WaitForSeconds(3.5f);
		GameManager.Instance.pause = false;
		GameManager.Instance.showSeasonGroup = false;
		showHint = true;
		GameManager.Instance.textHint.Show("<Sprite Name=\"ZL\"> Change Season");
	}
	void Update()
	{
		spriteRenderer.color = Color.Lerp(spriteRenderer.color, new Color(1, 1, 1, targetAlpha), 10 * Time.deltaTime);
		if (hide)
		{
			targetAlpha = 0;

		}
		if (showHint)
		{
			GameManager.Instance.textHint.textMeshProUGUI.text = "<Sprite Name=\"Right Stick All\"> Change Season";
			// if (InputManager.IsChangePressed)
			// {

			// }
			// else
			// {
			// 	GameManager.Instance.textHint.textMeshProUGUI.text = "<Sprite Name=\"ZL\"> Change Season";

			// }
			if (GameManager.Instance.season == season)
			{
				GameManager.Instance.textHint.Hide();
				Destroy(gameObject);
			}
		}
	}
}

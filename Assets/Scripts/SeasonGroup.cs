using UnityEngine;
using UnityEngine.UIElements;

public class SeasonGroup : MonoBehaviour
{
	private GameObject player;

	private CanvasGroup canvasGroup;

	private float targetAlpha = 0;
	private Vector3 targetScale = Vector3.zero;
	void Awake()
	{
		canvasGroup = GetComponent<CanvasGroup>();
		canvasGroup.alpha = 0;
		player = GameObject.FindWithTag("Player");
	}



	// Update is called once per frame
	void Update()
	{

		if (canvasGroup.alpha > 0)
		{
			transform.position = player.transform.position + Vector3.up * 2;
		}
		if (GameManager.Instance.pause)
		{
			if (GameManager.Instance.showSeasonGroup)
			{
				targetAlpha = 1;
				targetScale = Vector3.one;

			}
			else
			{
				targetAlpha = 0;
				targetScale = Vector3.zero;
			}

		}
		else if (InputManager.IsChangePressed || InputManager.season.magnitude > 0.1f)
		{
			targetAlpha = 1;
			targetScale = Vector3.one;
		}
		else
		{
			targetAlpha = 0;
			targetScale = Vector3.zero;
		}
		canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, targetAlpha, 5f * Time.unscaledDeltaTime);
		transform.localScale = Vector3.Lerp(transform.localScale, targetScale, 5f * Time.unscaledDeltaTime);
	}
}

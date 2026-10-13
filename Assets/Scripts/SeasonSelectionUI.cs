using UnityEngine;
using UnityEngine.UIElements;

public class SeasonSelectionUI : MonoBehaviour
{
	private CanvasGroup canvasGroup;

	private float targetAlpha = 0;
	void Awake()
	{
		canvasGroup = GetComponent<CanvasGroup>();
		canvasGroup.alpha = 0;
	}



	// Update is called once per frame
	void Update()
	{
		if (InputManager.IsChangePressed)
		{
			targetAlpha = 1;
		}
		else
		{
			targetAlpha = 0;
		}
		canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, targetAlpha, 0.1f);
	}
}

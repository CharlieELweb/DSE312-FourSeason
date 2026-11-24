using TMPro;
using UnityEngine;

public class TextHint : MonoBehaviour
{
	private CanvasGroup canvasGroup;
	public TextMeshProUGUI textMeshProUGUI;
	private float targetAlpha = 0;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		canvasGroup = GetComponent<CanvasGroup>();
		textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
		canvasGroup.alpha = targetAlpha;
	}

	// Update is called once per frame
	void Update()
	{
		canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, targetAlpha, 0.05f);
	}
	public void ShowInSecond(string text, int second)
	{
		textMeshProUGUI.text = text;
		targetAlpha = 1;
		Invoke("Hide", second);
	}
	public void Show(string text)
	{
		textMeshProUGUI.text = text;
		targetAlpha = 1;
	}
	public void Hide()
	{
		targetAlpha = 0;
	}
}

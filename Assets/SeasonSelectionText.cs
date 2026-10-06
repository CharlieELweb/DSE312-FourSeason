using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SeasonSelectionText : MonoBehaviour
{
	[SerializeField]
	private GameManager.Season season;
	private TextMeshProUGUI textMeshPro;
	private float targetSize = 32;
	private Color targetColor = Color.white;

	private void Start()
	{
		textMeshPro = GetComponent<TextMeshProUGUI>();
	}

	// Update is called once per frame
	private void Update()
	{
		if (GameManager.Instance.season == season)
		{
			targetColor = Color.yellow;
			targetSize = 36;
		}
		else
		{
			targetColor = Color.white;
			targetSize = 25;
		}
		textMeshPro.fontSize = Mathf.Lerp(textMeshPro.fontSize, targetSize, 0.01f);
		textMeshPro.color = Color.Lerp(textMeshPro.color, targetColor, 0.01f);
	}
}

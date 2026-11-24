using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SeasonUI : MonoBehaviour
{
	[SerializeField]
	private GameManager.Season season;
	private Image image;
	private Vector3 targetSize = 1.2f * Vector3.one;
	private Color targetColor = Color.gray;

	private void Start()
	{
		image = GetComponent<Image>();

	}

	// Update is called once per frame
	private void Update()
	{
		if (GameManager.Instance.canSwap[(int)season])
		{
			if (GameManager.Instance.season == season)
			{
				targetColor = Color.white;
				targetSize = 1.2f * Vector3.one;
			}
			else
			{
				targetColor = Color.gray;
				targetSize = 0.8f * Vector3.one;
			}
		}
		else
		{
			targetColor = new Color(0, 0, 0, 0);
		}

		transform.localScale = Vector3.Lerp(transform.localScale, targetSize, 5f * Time.unscaledDeltaTime);
		image.color = Color.Lerp(image.color, targetColor, 5f * Time.unscaledDeltaTime);
	}
}

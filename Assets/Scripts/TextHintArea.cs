using UnityEngine;

public class TextHintArea : MonoBehaviour
{
	[SerializeField] private string text;
	[SerializeField] private bool oneTime = false;
	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			GameManager.Instance.textHint.Show(text);
		}
	}
	void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			GameManager.Instance.textHint.Hide();
			if (oneTime)
			{
				Destroy(gameObject);
			}
		}
	}
}

using System.Collections.Generic;
using Abertay.Analytics;
using UnityEngine;
using System.Collections;

public class GreenStar : MonoBehaviour
{
	private SpriteRenderer spriteRenderer;
	private AudioSource audioSource;


	private float targetAlpha = 1f;
	private bool collected = false;

	private Vector3 targetPosition;

	void Awake()
	{
		spriteRenderer = GetComponentInChildren<SpriteRenderer>();
		audioSource = GetComponent<AudioSource>();
		targetPosition = transform.position;
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
			targetPosition = collision.transform.position + Vector3.up * 7f;
			StartCoroutine(GetStar());
		}
	}
	IEnumerator GetStar()
	{
		GameManager.Instance.pause = true;

		audioSource.Play();
		Dictionary<string, object> eventData = new Dictionary<string, object>
			{
				{"Position", transform.position},
				{"Time", Time.time}
			};
		AnalyticsManager.SendCustomEvent("GreenStar", eventData);
		yield return new WaitForSeconds(3f);
		GameManager.Instance.textCenter.ShowInSecond("You got a\nGreen Star!", 4);
		yield return new WaitForSeconds(4f);
		GameManager.Instance.pause = false;
		targetAlpha = 0;
	}
	void Update()
	{
		spriteRenderer.color = Color.Lerp(spriteRenderer.color, new Color(1, 1, 1, targetAlpha), 10 * Time.deltaTime);
		transform.position = Vector3.Lerp(transform.position, targetPosition, 5 * Time.deltaTime);
		if (spriteRenderer.color.a < 0.01f)
		{
			Destroy(gameObject);
		}
	}
}

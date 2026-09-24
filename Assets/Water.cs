using UnityEngine;

public class Water : MonoBehaviour
{
	private BoxCollider2D boxCollider2D;
	private SpriteRenderer spriteRenderer;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		boxCollider2D = GetComponent<BoxCollider2D>();
	}

	// Update is called once per frame
	void Update()
	{
		if (GameManager.Instance.season == GameManager.Season.Summer)
		{
			spriteRenderer.color = Color.blue;
			boxCollider2D.enabled = false;
		}
		else
		{
			spriteRenderer.color = Color.lightBlue;
			boxCollider2D.enabled = true;
		}
	}
}

using UnityEngine;

public class Water : MonoBehaviour
{
	private BoxCollider2D boxCollider2D;
	private SpriteRenderer spriteRenderer;
	[SerializeField]
	private AreaEffector2D effector2D;
	private ParticleSystem particleSystem;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		spriteRenderer = GetComponentInChildren<SpriteRenderer>();
		boxCollider2D = GetComponent<BoxCollider2D>();
		particleSystem = GetComponentInChildren<ParticleSystem>();
		UpdateFunction();
	}

	// Update is called once per frame
	void Update()
	{
		UpdateFunction();

	}

	void UpdateFunction()
	{
		if (GameManager.Instance.season == GameManager.Season.Summer)
		{
			spriteRenderer.color = Color.blue;
			boxCollider2D.enabled = false;
			effector2D.enabled = true;
			if (!particleSystem.isEmitting)
			{
				particleSystem.Play();
			}
		}
		else if (GameManager.Instance.season == GameManager.Season.Winter)
		{
			spriteRenderer.color = Color.lightBlue;
			boxCollider2D.enabled = true;
			effector2D.enabled = false;
			particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmitting);
		}
		else
		{
			spriteRenderer.color = Color.blue;
			boxCollider2D.enabled = false;
			effector2D.enabled = false;
			particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmitting);
		}
	}
}

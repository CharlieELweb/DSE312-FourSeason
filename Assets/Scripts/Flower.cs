using UnityEngine;

public class Flower : MonoBehaviour
{
	[SerializeField] private float jumpSpeed = 10f;
	private BoxCollider2D boxCollider2D;
	private LayerMask detectLayer;
	private Animator animator;

	private void OnEnable()
	{
		GameManager.OnSeasonChanged += OnSeasonChanged;
	}

	private void OnDisable()
	{
		GameManager.OnSeasonChanged -= OnSeasonChanged;
	}

	private void OnSeasonChanged(GameManager.Season oldSeason, GameManager.Season newSeason)
	{
		switch (newSeason)
		{
			case GameManager.Season.Spring:
				animator.SetBool("IsOpen", true);
				boxCollider2D.enabled = true;
				break;
			case GameManager.Season.Summer:
				animator.SetBool("IsOpen", false);
				boxCollider2D.enabled = false;
				break;
			case GameManager.Season.Autumn:
				animator.SetBool("IsOpen", false);
				boxCollider2D.enabled = false;
				break;
			case GameManager.Season.Winter:
				animator.SetBool("IsOpen", false);
				boxCollider2D.enabled = false;
				break;
		}
	}

	private void Awake()
	{
		boxCollider2D = GetComponent<BoxCollider2D>();
		animator = GetComponent<Animator>();

	}
	void Start()
	{
		OnSeasonChanged(GameManager.Season.Spring, GameManager.Instance.season);
	}
	void OnTriggerEnter2D(Collider2D collision)
	{
		collision.gameObject.GetComponent<Rigidbody2D>().linearVelocityY = jumpSpeed;
	}
}

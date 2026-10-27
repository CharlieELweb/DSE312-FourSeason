using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
	[SerializeField] private GameObject turnDustParticle;
	[SerializeField] private GameObject jumpDustParticle;
	private Transform sprite;
	private Player player;
	private Animator animator;
	private Rigidbody2D rigidbody2D;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	private void Awake()
	{
		player = GetComponent<Player>();
		animator = GetComponent<Animator>();
		rigidbody2D = GetComponent<Rigidbody2D>();
		sprite = transform.GetChild(0);
	}

	private void OnEnable()
	{
		Player.OnPlayerStateEnter += OnPlayerStateEnter;
		Player.OnPlayerStateExit += OnPlayerStateExit;
	}

	private void OnDisable()
	{
		Player.OnPlayerStateEnter -= OnPlayerStateEnter;
		Player.OnPlayerStateExit -= OnPlayerStateExit;
	}

	private void CreateParticle(GameObject particle, Vector3 scale)
	{
		GameObject newParticle = Instantiate(particle, transform.position, Quaternion.Euler(new Vector3(-90, 0, 0)));
		newParticle.transform.localScale = scale;
	}

	private void OnPlayerStateEnter(Player.PlayerState playerState)
	{
		switch (playerState)
		{
			case Player.PlayerState.Idle:
				break;
			case Player.PlayerState.Run:
				break;
			case Player.PlayerState.Fall:
				break;
			case Player.PlayerState.Jump:
				CreateParticle(jumpDustParticle, new Vector3(1, 1, 1));
				sprite.localScale = new Vector3(sprite.localScale.x * 0.7f, 1.3f, 1);
				break;
		}
	}
	private void OnPlayerStateExit(Player.PlayerState playerState)
	{
		switch (playerState)
		{
			case Player.PlayerState.Idle:
				break;
			case Player.PlayerState.Run:
				break;
			case Player.PlayerState.Fall:
				CreateParticle(jumpDustParticle, new Vector3(1, 1, 1));
				sprite.localScale = new Vector3(sprite.localScale.x * 1.2f, 0.8f, 1);
				break;
			case Player.PlayerState.Jump:
				break;
		}
	}
	// Update is called once per frame
	void Update()
	{
		if (rigidbody2D.linearVelocityX < -1 && sprite.localScale.x > 0)
		{
			if (player.isOnFloor)
			{
				CreateParticle(turnDustParticle, new Vector3(-1, 1, 1));
			}

			sprite.localScale = new Vector3(Mathf.Abs(sprite.localScale.x) * -1, sprite.localScale.y, 1);
		}
		else if (rigidbody2D.linearVelocityX > 1 && sprite.localScale.x < 0)
		{
			if (player.isOnFloor)
			{
				CreateParticle(turnDustParticle, new Vector3(1, 1, 1));
			}
			sprite.localScale = new Vector3(Mathf.Abs(sprite.localScale.x), sprite.localScale.y, 1);
		}
		sprite.localScale = Vector3.Lerp(sprite.localScale, new Vector3(Mathf.Sign(sprite.localScale.x), 1, 1), 0.005f);
		animator.SetBool("isRunning", Mathf.Abs(rigidbody2D.linearVelocityX) > 1);
	}
}

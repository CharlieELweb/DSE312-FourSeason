using UnityEngine;
using UnityEditor.Experimental.GraphView;

using UnityEngine.InputSystem;
using Unity.VisualScripting;
using NUnit.Framework;
using System;
using UnityEngine.Assertions.Must;

public class Player : Node2D
{
	public static event Action<PlayerState> OnPlayerStateEnter, OnPlayerStateExit;
	public enum PlayerState
	{
		Idle,
		Run,
		Jump,
		Fall,
	}
	public PlayerState currentPlayerState = PlayerState.Idle;



	[SerializeField]
	private PlayerData playerData;

	public bool isOnFloor;
	public Vector3 groundNormalLeft, groundNormalRight;

	private float coyoteTimer = 0;
	private float jumpBufferTimer = 0;


	private void Update()
	{
		if (GameManager.Instance.pause)
		{
			return;
		}
		if (froze)
		{
			return;
		}
		if (GameManager.Instance.isTransition)
		{
			return;
		}
		switch (currentPlayerState)
		{
			case PlayerState.Idle:
				if (InputManager.movement.x != 0)
				{
					ChangeToState(PlayerState.Run);
				}
				if (InputManager.IsJumpJustPressed)
				{
					ChangeToState(PlayerState.Jump);
				}
				break;
			case PlayerState.Run:
				if (InputManager.IsJumpJustPressed)
				{
					ChangeToState(PlayerState.Jump);
				}
				break;
			case PlayerState.Fall:
				if (InputManager.IsJumpJustPressed)
				{
					if (coyoteTimer > 0)
					{
						ChangeToState(PlayerState.Jump);
					}
					else
					{
						StartJumpBufferTimer();
					}

				}
				break;
			case PlayerState.Jump:
				break;
		}
		coyoteTimer = Mathf.Max(0, coyoteTimer - Time.deltaTime);
		jumpBufferTimer = Mathf.Max(0, jumpBufferTimer - Time.deltaTime);
	}

	protected override void FixedUpdate()
	{
		base.FixedUpdate();
		if (GameManager.Instance.pause)
		{
			return;
		}
		if (froze)
		{
			return;
		}
		isOnFloor = IsOnFloor();
		groundNormalLeft = GetGroundNormalLeft();
		groundNormalRight = GetGroundNormalRight();
		if (IsOnWallRight() && InputManager.movement.x > 0 || IsOnWallLeft() && InputManager.movement.x < 0)
		{
			rigidbody2D.AddForceX(1000 * InputManager.movement.x);
		}
		switch (currentPlayerState)
		{
			case PlayerState.Idle:
				if (groundNormalLeft == groundNormalRight)
				{
					rigidbody2D.AddForce(-(groundNormalLeft + groundNormalRight).normalized * 300);
				}
				if (!isOnFloor)
				{
					StartCoyoteTimer();
					ChangeToState(PlayerState.Fall);
				}
				break;
			case PlayerState.Run:
				Move(InputManager.movement.x, playerData.groundAccelaration, playerData.groundDecelaration);
				if (groundNormalLeft == groundNormalRight)
				{
					rigidbody2D.AddForce(-(groundNormalLeft + groundNormalRight).normalized * 300);
				}
				if (!isOnFloor)
				{
					StartCoyoteTimer();
					ChangeToState(PlayerState.Fall);
				}
				if (rigidbody2D.linearVelocityX == 0)
				{
					ChangeToState(PlayerState.Idle);
				}
				break;
			case PlayerState.Fall:
				HandleGravity(playerData.gravity);
				Move(InputManager.movement.x, playerData.airAccelaration, playerData.airDecelaration);
				if (isOnFloor)
				{
					if (jumpBufferTimer > 0)
					{
						ChangeToState(PlayerState.Jump);
					}
					else if (rigidbody2D.linearVelocityX == 0)
					{
						ChangeToState(PlayerState.Idle);
					}
					else
					{
						ChangeToState(PlayerState.Run);
					}

				}
				break;
			case PlayerState.Jump:
				HandleGravity(playerData.gravity);
				Move(InputManager.movement.x, playerData.airAccelaration, playerData.airDecelaration);
				if (rigidbody2D.linearVelocityY <= 0)
				{
					ChangeToState(PlayerState.Fall);
				}
				break;
		}
	}

	private void StartCoyoteTimer()
	{
		coyoteTimer = playerData.coyoteTime;
	}
	private void StartJumpBufferTimer()
	{
		jumpBufferTimer = playerData.jumpBufferTime;
	}

	private void ChangeToState(PlayerState playerState)
	{
		OnPlayerStateEnter?.Invoke(playerState);
		OnPlayerStateExit?.Invoke(currentPlayerState);
		currentPlayerState = playerState;

		switch (currentPlayerState)
		{
			case PlayerState.Idle:
				rigidbody2D.linearVelocity = Vector2.zero;
				// rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
				break;
			case PlayerState.Run:
				// rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
				break;
			case PlayerState.Fall:
				// rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
				break;
			case PlayerState.Jump:
				// rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
				Jump();
				break;
		}
	}

	private void HandleGravity(float gravity)
	{
		if (froze)
		{
			return;
		}
		if (!isOnFloor)
		{
			rigidbody2D.linearVelocityY -= gravity * Time.deltaTime;
		}
	}

	private void Move(float direction, float acceleration, float deceleration)
	{
		if (froze)
		{
			return;
		}
		if (GameManager.Instance.isTransition)
		{
			return;
		}
		float delta = direction *
			rigidbody2D.linearVelocityX > 0 ? acceleration : deceleration;

		rigidbody2D.linearVelocityX = Mathf.MoveTowards(
			rigidbody2D.linearVelocityX,
			playerData.maxSpeed * direction,
			delta
		);
	}

	private void Jump()
	{
		if (GameManager.Instance.isTransition)
		{
			return;
		}
		coyoteTimer = 0;
		jumpBufferTimer = 0;
		if (rigidbody2D.linearVelocityY > 0)
		{
			rigidbody2D.linearVelocityY = playerData.jumpSpeed;
		}
		else
		{
			rigidbody2D.linearVelocityY = playerData.jumpSpeed;
		}
		GameObject floor = GetFloor();
		if (floor != null && floor.CompareTag("Object"))
		{
			floor.GetComponent<Rigidbody2D>().AddForceY(-200f, ForceMode2D.Impulse);
		}
	}

}

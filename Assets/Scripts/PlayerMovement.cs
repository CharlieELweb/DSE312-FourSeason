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

	private float coyoteTimer = 0;
	private float jumpBufferTimer = 0;


	private void Update()
	{
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
		isOnFloor = IsOnFloor();
		switch (currentPlayerState)
		{
			case PlayerState.Idle:
				if (!isOnFloor)
				{
					StartCoyoteTimer();
					ChangeToState(PlayerState.Fall);
				}
				break;
			case PlayerState.Run:
				Move(InputManager.movement.x, playerData.groundAccelaration, playerData.groundDecelaration);
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
				break;
			case PlayerState.Run:
				break;
			case PlayerState.Fall:
				break;
			case PlayerState.Jump:
				Jump();
				break;
		}
	}

	private void HandleGravity(float gravity)
	{
		if (!isOnFloor)
		{
			rigidbody2D.linearVelocityY -= gravity * Time.deltaTime;
		}
	}

	private void Move(float direction, float acceleration, float deceleration)
	{
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
		coyoteTimer = 0;
		jumpBufferTimer = 0;
		if (rigidbody2D.linearVelocityY > 0)
		{
			rigidbody2D.linearVelocityY += playerData.jumpSpeed;
		}
		else
		{
			rigidbody2D.linearVelocityY = playerData.jumpSpeed;
		}

	}

}

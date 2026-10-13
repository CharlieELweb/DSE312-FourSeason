using UnityEngine;
using UnityEditor.Experimental.GraphView;

using UnityEngine.InputSystem;
using Unity.VisualScripting;
using NUnit.Framework;
using System;

public class Player : Node2D
{
	enum PlayerState
	{
		Idle,
		Run,
		Jump,
		Fall,
	}
	private PlayerState currentPlayerState = PlayerState.Idle;



	[SerializeField]
	private PlayerData playerData;

	private bool isOnFloor;

	private float coyoteTimer = 0;
	// private float jumpBufferTimer = 0;


	private void Update()
	{
		// print(coyoteTimer);
		// if (isOnFloor)
		// {
		// 	if (coyoteTimer != playerData.coyoteTime)
		// 	{
		// 		coyoteTimer = playerData.coyoteTime;
		// 	}
			
		// }
		// else {
		// 	coyoteTimer = Mathf.Max(0, coyoteTimer - Time.deltaTime);
		// }
		// if (InputManager.IsJumpJustPressed)
		// {
		// 	if (coyoteTimer > 0)
		// 	{
		// 		Jump();
		// 	}
		// 	else
		// 	{
		// 		// jumpBufferTimer = playerData.jumpBufferTime;
		// 	}
		// }
		
		// if (isOnFloor && jumpBufferTimer > 0)
		// {
		// 	Jump();
		// }
		// jumpBufferTimer = Mathf.Max(0, jumpBufferTimer - Time.deltaTime);
	}

	private void FixedUpdate()
	{
		isOnFloor = IsOnFloor();
		switch (currentPlayerState)
		{
			case PlayerState.Idle:
				break;
			case PlayerState.Run:
				Move(InputManager.movement.x);
				break;
			case PlayerState.Fall:
				HandleGravity(playerData.gravity);
				break;
		}
	}

	private void ChangeToState(PlayerState playerState)
	{
		currentPlayerState(playerState);
	}

	private void HandleGravity(float gravity)
	{
		if (!isOnFloor)
		{
			rigidbody2D.linearVelocityY -= gravity * Time.deltaTime;
		}
	}

	private void Move(float direction)
	{
		float acceleration = 0;
		if (isOnFloor)
		{
			acceleration =
				direction * rigidbody2D.linearVelocityX > 0 ?
				playerData.groundAccelaration :
				playerData.groundDecelaration;
		}
		else
		{
			acceleration =
				direction * rigidbody2D.linearVelocityX > 0 ?
				playerData.airAccelaration :
				playerData.airDecelaration;
		}
		rigidbody2D.linearVelocityX = Mathf.MoveTowards(
			rigidbody2D.linearVelocityX,
			playerData.maxSpeed * direction,
			acceleration
		);
	}

	private void Jump()
	{
		coyoteTimer = 0;
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

using UnityEngine;
using UnityEditor.Experimental.GraphView;

using UnityEngine.InputSystem;
using Unity.VisualScripting;
using NUnit.Framework;
using System;

public class Player : Node2D
{
	[SerializeField]
	private PlayerData playerData;

	private bool isOnFloor;
	private bool wasOnFloor;

	private float coyoteTimer = 0;
	private float jumpBufferTimer = 0;


	private void Update()
	{
		if (InputManager.IsJumpJustPressed)
		{
			if (isOnFloor || coyoteTimer > 0)
			{
				Jump();
			}
			else
			{
				jumpBufferTimer = playerData.jumpBufferTime;
			}

		}
		coyoteTimer = Mathf.Max(0, coyoteTimer - Time.deltaTime);
		jumpBufferTimer = Mathf.Max(0, jumpBufferTimer - Time.deltaTime);
	}

	private void FixedUpdate()
	{
		isOnFloor = IsOnFloor();
		if (!isOnFloor)
		{
			rigidbody2D.linearVelocityY -= playerData.gravity * Time.deltaTime;
		}
		if (!isOnFloor && wasOnFloor)
		{
			coyoteTimer = playerData.coyoteTime;
		}
		if (isOnFloor && !wasOnFloor && jumpBufferTimer > 0)
		{
			Jump();
		}
		Move(InputManager.movement.x);
		wasOnFloor = isOnFloor;
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



	// [SerializeField]
	// private float moveSpeed = 10f;
	// [SerializeField]
	// private float acceleration = 2f;
	// [SerializeField]
	// private float gravity = 1f;
	// [SerializeField]
	// private float fallMultiplier = 5f;

	// [SerializeField]
	// private float jumpSpeed = 10f;
	// [SerializeField]
	// private float linearDamping = 4f;
	// [SerializeField]
	// private LayerMask groundLayerMask;
	// private Rigidbody2D rigidbody2D;
	// private BoxCollider2D boxCollider2D;
	// private Vector2 movement;

	// bool onFloor = false;
	// void Start()
	// {
	// 	rigidbody2D = GetComponent<Rigidbody2D>();
	// 	boxCollider2D = GetComponent<BoxCollider2D>();
	// }

	// void Update()
	// {
	// 	movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

	// 	if (onFloor && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)))
	// 	{
	// 		Jump();
	// 	}

	// }
	// void FixedUpdate()
	// {
	// 	onFloor = IsOnFloor();
	// 	if (onFloor)
	// 	{
	// 		if (rigidbody2D.linearVelocityY < 0)
	// 		{
	// 			rigidbody2D.linearVelocityY = 0;
	// 		}
	// 	}


	// 	HorizontalMovement(movement.x);
	// }

	// private void HorizontalMovement(float direction)
	// {
	// 	rigidbody2D.AddForceX(acceleration * direction);
	// 	rigidbody2D.linearVelocityX = Mathf.Clamp(rigidbody2D.linearVelocityX, -moveSpeed, moveSpeed);
	// }

	// private void Jump()
	// {
	// 	rigidbody2D.linearVelocityY += jumpSpeed;
	// }

	// private bool IsOnFloor()
	// {
	// 	float boxColliderX = boxCollider2D.bounds.extents.x;
	// 	float boxColliderY = boxCollider2D.bounds.extents.y;
	// 	RaycastHit2D raycastHit2D = Physics2D.BoxCast(
	// 		boxCollider2D.bounds.center,
	// 		boxCollider2D.bounds.size,
	// 		0f,
	// 		Vector2.down,
	// 		0.1f,
	// 		groundLayerMask);
	// 	if (raycastHit2D.collider != null)
	// 	{
	// 		Debug.DrawRay(boxCollider2D.bounds.center, Vector2.down * (boxCollider2D.bounds.extents.y + 0.01f), Color.red);
	// 		return true;
	// 	}
	// 	else
	// 	{
	// 		Debug.DrawRay(boxCollider2D.bounds.center, Vector2.down * (boxCollider2D.bounds.extents.y + 0.01f), Color.green);
	// 		return false;
	// 	}
	// }
}

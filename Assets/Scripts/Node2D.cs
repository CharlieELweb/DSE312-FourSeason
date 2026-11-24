using System;
using UnityEngine;

public class Node2D : MonoBehaviour
{
	public Rigidbody2D rigidbody2D;
	private BoxCollider2D boxCollider2D;
	[SerializeField]
	private LayerMask groundLayer;
	private float stepHeight = 1.5f;
	public bool froze;

	private void Awake()
	{
		rigidbody2D = GetComponent<Rigidbody2D>();
		boxCollider2D = GetComponent<BoxCollider2D>();
	}

	protected virtual void FixedUpdate()
	{
		if (GameManager.Instance.pause || froze)
		{
			rigidbody2D.bodyType = RigidbodyType2D.Static;

			return;
		}
		rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
		if (GameManager.Instance.isTransition)
		{
			return;
		}
		// if (IsOnWallRight() && Mathf.Abs(rigidbody2D.linearVelocityY) <= 0.1f)
		// {
		// 	float height = GetWallHeight(Vector3.right);
		// 	if (height <= stepHeight)
		// 	{
		// 		transform.position += (height + 0.1f) * Vector3.up + 0.1f * Vector3.right;
		// 	}
		// }
		// if (IsOnWallLeft() && Mathf.Abs(rigidbody2D.linearVelocityY) <= 0.1f)
		// {
		// 	float height = GetWallHeight(Vector3.left);
		// 	if (height <= stepHeight)
		// 	{
		// 		transform.position += (height + 0.1f) * Vector3.up + 0.1f * Vector3.left;
		// 	}
		// }

	}

	public Vector3 GetGroundNormalLeft()
	{
		return GetGroundNormal(Vector3.left);
	}

	public Vector3 GetGroundNormalRight()
	{
		return GetGroundNormal(Vector3.right);
	}
	public Vector3 GetWallNormal(Vector3 offset)
	{
		Debug.DrawRay(
			boxCollider2D.bounds.center + (boxCollider2D.bounds.size.y / 2 - 0.1f) * Vector3.down,
			offset * (boxCollider2D.bounds.size.x / 2 + 0.5f),
			Color.orange
		);
		return Physics2D.Raycast(
			boxCollider2D.bounds.center + (boxCollider2D.bounds.size.y / 2 - 0.1f) * Vector3.down,
			offset,
			boxCollider2D.bounds.size.x / 2 + 0.5f,
			groundLayer
		).normal;
	}
	public float GetWallHeight(Vector3 offset)
	{
		Debug.DrawRay(
			boxCollider2D.bounds.center + boxCollider2D.bounds.size.y / 2 * Vector3.up + (boxCollider2D.bounds.size.x / 2 + 0.5f) * offset,
			Vector2.down * (boxCollider2D.bounds.size.y / 2 + 5f),
			Color.pink
		);
		return boxCollider2D.bounds.size.y - Physics2D.Raycast(
			boxCollider2D.bounds.center + boxCollider2D.bounds.size.y / 2 * Vector3.up + (boxCollider2D.bounds.size.x / 2 + 0.5f) * offset,
			Vector2.down,
			boxCollider2D.bounds.size.y / 2 + 5f,
			groundLayer
		).distance;
	}
	public Vector3 GetGroundNormal(Vector3 offset)
	{
		Debug.DrawRay(
			boxCollider2D.bounds.center + (boxCollider2D.bounds.size.y / 2 - 0.05f) * Vector3.down + (boxCollider2D.bounds.size.x / 2 + 0.1f) * offset,
			Vector2.down * 0.5f,
			Color.pink
		);
		return Physics2D.Raycast(
			boxCollider2D.bounds.center + (boxCollider2D.bounds.size.y / 2 - 0.05f) * Vector3.down + (boxCollider2D.bounds.size.x / 2 + 0.1f) * offset,
			Vector2.down,
			0.5f,
			groundLayer
		).normal;
	}
	public GameObject GetFloor()
	{
		RaycastHit2D[] raycastHit2D = BoxCast(Vector2.down, 0.1f, groundLayer);
		if (raycastHit2D.Length > 0)
		{
			return raycastHit2D[0].collider?.gameObject;
		}
		else
		{
			return null;
		}

	}
	public bool IsOnFloor()
	{
		return BoxCast(Vector2.down, 0.1f, groundLayer).Length > 0;
	}
	public bool IsOnCeiling()
	{
		return BoxCast(Vector2.up, 0.1f, groundLayer).Length > 0;
	}
	public bool IsOnWall()
	{
		return IsOnWallLeft() || IsOnWallRight();
	}
	public bool IsOnWallLeft()
	{
		return Mathf.Abs(GetWallNormal(Vector2.left).y) < 0.01f && BoxCast(Vector2.left, 0.1f, groundLayer).Length > 0;
	}
	public bool IsOnWallRight()
	{
		return Mathf.Abs(GetWallNormal(Vector2.right).y) < 0.01f && BoxCast(Vector2.right, 0.1f, groundLayer).Length > 0;
	}
	public RaycastHit2D[] BoxCast(Vector2 direction, float distance, LayerMask layer)
	{
		Color debugColor = Color.red;
		RaycastHit2D[] raycastHit2D = Physics2D.BoxCastAll(
			boxCollider2D.bounds.center,
			boxCollider2D.bounds.size - Vector3.one * 0.01f,
			0f,
			direction,
			distance,
			layer);

		if (raycastHit2D.Length == 0 || (raycastHit2D.Length == 1 && raycastHit2D[0].collider.gameObject == gameObject))
		{
			raycastHit2D = new RaycastHit2D[0];
			debugColor = Color.green;
		}

		DrawBox(boxCollider2D.bounds.center,
			boxCollider2D.bounds.size - Vector3.one * 0.01f,
			0f,
			direction,
			distance,
			debugColor);
		return raycastHit2D;
	}
	void DrawBox(Vector2 origion, Vector2 size, float angle, Vector2 direction, float distance, Color color)
	{
		Debug.DrawRay(origion + size.x * Vector2.left / 2, direction * (size.y / 2 + distance), color);
		Debug.DrawRay(origion + size.x * Vector2.right / 2, direction * (size.y / 2 + distance), color);
	}
}

using UnityEngine;

public class Node2D : MonoBehaviour
{
	public Rigidbody2D rigidbody2D;
	private BoxCollider2D boxCollider2D;
	[SerializeField]
	private LayerMask groundLayer;

	private void Awake()
	{
		rigidbody2D = GetComponent<Rigidbody2D>();
		boxCollider2D = GetComponent<BoxCollider2D>();
	}

	protected virtual void FixedUpdate()
	{
		if (IsOnCeiling())
		{
			print(rigidbody2D.linearVelocityY);
		}
	}

	private void TryMoveToSide(float delta, float maxDistance)
	{

	}

	public bool IsOnFloor()
	{
		return BoxCast(Vector2.down, 0.1f, groundLayer);
	}
	public bool IsOnCeiling()
	{
		return BoxCast(Vector2.up, 0.1f, groundLayer);
	}

	public bool BoxCast(Vector2 direction, float distance, LayerMask layer)
	{
		Color debugColor = Color.red;
		bool result = true;
		RaycastHit2D[] raycastHit2D = Physics2D.BoxCastAll(
			boxCollider2D.bounds.center,
			boxCollider2D.bounds.size,
			0f,
			direction,
			distance,
			layer);

		if (raycastHit2D.Length == 0 || (raycastHit2D.Length == 1 && raycastHit2D[0].collider.gameObject == gameObject))
		{
			result = false;
			debugColor = Color.green;
		}

		DrawBox(boxCollider2D.bounds.center,
			boxCollider2D.bounds.size,
			0f,
			direction,
			distance,
			debugColor);
		return result;
	}
	void DrawBox(Vector2 origion, Vector2 size, float angle, Vector2 direction, float distance, Color color)
	{
		Debug.DrawRay(origion + size.x * Vector2.left, direction * (size.y + distance), color);
		Debug.DrawRay(origion + size.x * Vector2.right, direction * (size.y + distance), color);
	}
}

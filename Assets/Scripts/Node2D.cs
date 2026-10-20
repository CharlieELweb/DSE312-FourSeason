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
		IsOnWall();
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
		return BoxCast(Vector2.down, 0.1f, groundLayer) != null;
	}
	public bool IsOnCeiling()
	{
		return BoxCast(Vector2.up, 0.1f, groundLayer) != null;
	}
	public bool IsOnWall()
	{
		return BoxCast(Vector2.left, 0.1f, groundLayer) != null || BoxCast(Vector2.right, 0.1f, groundLayer) != null;
	}
	public RaycastHit2D[] BoxCast(Vector2 direction, float distance, LayerMask layer)
	{
		Color debugColor = Color.red;
		RaycastHit2D[] raycastHit2D = Physics2D.BoxCastAll(
			boxCollider2D.bounds.center,
			boxCollider2D.bounds.size,
			0f,
			direction,
			distance,
			layer);

		if (raycastHit2D.Length == 0 || (raycastHit2D.Length == 1 && raycastHit2D[0].collider.gameObject == gameObject))
		{
			raycastHit2D = null;
			debugColor = Color.green;
		}

		DrawBox(boxCollider2D.bounds.center,
			boxCollider2D.bounds.size,
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

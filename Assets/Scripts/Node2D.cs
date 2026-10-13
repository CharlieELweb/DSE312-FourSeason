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

	public bool IsOnFloor()
	{
		RaycastHit2D[] raycastHit2D = Physics2D.BoxCastAll(
			boxCollider2D.bounds.center,
			boxCollider2D.bounds.size,
			0f,
			Vector2.down,
			0.1f,
			groundLayer);

		print(gameObject);
		if (raycastHit2D.Length == 0 || (raycastHit2D.Length == 1 && raycastHit2D[0].collider.gameObject == gameObject))
		{
			Debug.DrawRay(boxCollider2D.bounds.center + boxCollider2D.bounds.extents.x * Vector3.left, Vector2.down * (boxCollider2D.bounds.extents.y + 0.1f), Color.red);
			Debug.DrawRay(boxCollider2D.bounds.center + boxCollider2D.bounds.extents.x * Vector3.right, Vector2.down * (boxCollider2D.bounds.extents.y + 0.1f), Color.red);
			return false;
		}
		else
		{
			Debug.DrawRay(boxCollider2D.bounds.center + boxCollider2D.bounds.extents.x * Vector3.left, Vector2.down * (boxCollider2D.bounds.extents.y + 0.1f), Color.green);
			Debug.DrawRay(boxCollider2D.bounds.center + boxCollider2D.bounds.extents.x * Vector3.right, Vector2.down * (boxCollider2D.bounds.extents.y + 0.1f), Color.green);
			return true;
		}
	}
}

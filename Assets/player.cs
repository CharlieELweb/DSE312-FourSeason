using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
public class player : MonoBehaviour
{
	[SerializeField]
	float moveSpeed = 10f;
	[SerializeField]
	float jumpSpeed = 10f;
	[SerializeField]
	LayerMask layerMask;
	Rigidbody2D rigidbody2D;
	BoxCollider2D boxCollider2D;
	Vector2 movement;
	void Start()
	{
		rigidbody2D = GetComponent<Rigidbody2D>();
		boxCollider2D = GetComponent<BoxCollider2D>();
	}

	void Update()
	{
		float movement = Input.GetAxisRaw("Horizontal") * moveSpeed;
		rigidbody2D.linearVelocityX = movement;
		if (IsOnFloor() && Input.GetKeyDown(KeyCode.Space))
		{
			Jump();
		}
	}

	void Jump()
	{
		rigidbody2D.linearVelocityY = jumpSpeed;
	}

	private bool IsOnFloor()
	{

		RaycastHit2D raycastHit2D = Physics2D.Raycast(boxCollider2D.bounds.center, Vector2.down, boxCollider2D.bounds.extents.y + 0.1f, layerMask);
		if (raycastHit2D.collider != null)
		{
			Debug.DrawRay(boxCollider2D.bounds.center, Vector2.down * (boxCollider2D.bounds.extents.y + 0.01f), Color.red);
			return true;
		}
		else
		{
			Debug.DrawRay(boxCollider2D.bounds.center, Vector2.down * (boxCollider2D.bounds.extents.y + 0.01f), Color.green);
			return false;
		}


	}


}

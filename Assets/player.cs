using UnityEngine;
using UnityEditor.Experimental.GraphView;

using UnityEngine.InputSystem;
public class Player : MonoBehaviour
{
	[SerializeField]
	float moveForce = 10f;
	[SerializeField]
	float maxSpeed = 10f;

	[SerializeField]
	float gravityScale = 3f;
	[SerializeField]
	float fallMultiplier = 5f;

	[SerializeField]
	float jumpForce = 10f;
	[SerializeField]
	float linearDamping = 4f;
	[SerializeField]
	LayerMask layerMask;
	Rigidbody2D rigidbody2D;
	BoxCollider2D boxCollider2D;
	Vector2 movement;

	bool onFloor = false;
	void Start()
	{
		rigidbody2D = GetComponent<Rigidbody2D>();
		boxCollider2D = GetComponent<BoxCollider2D>();
	}

	void Update()
	{
		movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		if (onFloor)
		{
			if (Mathf.Abs(movement.x) < 0.4f || movement.x * rigidbody2D.linearVelocityX < 0)
			{
				rigidbody2D.linearDamping = linearDamping;
			}
			else
			{
				rigidbody2D.linearDamping = 0;
			}
			rigidbody2D.gravityScale = 0;
		}
		else
		{
			rigidbody2D.gravityScale = gravityScale;
			rigidbody2D.linearDamping = linearDamping * 0.15f;
			//if (rigidbody2D.linearVelocityY < 0)
			//{
			//	rigidbody2D.gravityScale = gravityScale * fallMultiplier;
			//}
			//if (rigidbody2D.linearVelocityY > 0 && !Input.GetKey(KeyCode.Space))
			//{
			//	rigidbody2D.gravityScale = gravityScale * (fallMultiplier / 2);
			//}
		}

		if (onFloor && (Input.GetKeyDown(KeyCode.Space)) || Input.GetKeyDown(KeyCode.W))
		{
			Jump();
		}
		
	}
	void FixedUpdate()
	{
		onFloor = IsOnFloor();
		if(onFloor) {
			if (rigidbody2D.linearVelocityY < 0) {
				rigidbody2D.linearVelocityY = 0;
			}
		}


        HorizontalMovement(movement.x);
	}

	void HorizontalMovement(float horiziontal)
	{
		rigidbody2D.AddForce(Vector2.right * horiziontal * moveForce);
		rigidbody2D.linearVelocityX = Mathf.Clamp(rigidbody2D.linearVelocityX, -maxSpeed, maxSpeed);
	}

	void Jump()
	{
		rigidbody2D.linearVelocityY = 0;
		rigidbody2D.AddForceY(jumpForce, ForceMode2D.Impulse);
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

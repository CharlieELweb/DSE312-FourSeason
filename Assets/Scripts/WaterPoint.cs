using UnityEngine;

public class WaterPoint : MonoBehaviour
{

	private float k = 0.1f;
	private float dampening = 0.1f;
	public float velocity = 0;
	private float resistance = 40f;
	private float force = 0;
	public float height = 0;
	private float target_height = 0;
	public LayerMask layerMask;

	private void FixedUpdate()
	{
		height = transform.localPosition.y;
		force = -k * (height - target_height) - dampening * velocity;
		velocity += force;
		transform.localPosition = new Vector3(
			transform.localPosition.x,
			transform.localPosition.y + velocity,
			transform.localPosition.z
		);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		float objectVelocity = collision.gameObject.GetComponent<Rigidbody2D>().linearVelocityY;
		velocity += objectVelocity / resistance;
	}

}

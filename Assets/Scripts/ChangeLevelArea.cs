using UnityEngine;

public class ChangeLevelArea : MonoBehaviour
{
	[SerializeField] private string sceneName;
	[SerializeField] private float velocityX = -10;
	[SerializeField] private Vector3 playerPosition;
	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			GameManager.Instance.ChangeToScene(sceneName, playerPosition, velocityX);
			collision.gameObject.GetComponent<Rigidbody2D>().linearVelocityX = velocityX;
		}
	}
}

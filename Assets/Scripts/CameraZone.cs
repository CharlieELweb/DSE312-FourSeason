using UnityEngine;

public class CameraZone : MonoBehaviour
{
	public float smooth = 0.01f;
	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			Camera.main.gameObject.GetComponent<CameraFollow>().target = gameObject;
		}
	}
	void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			Camera.main.gameObject.GetComponent<CameraFollow>().target = collision.gameObject;
		}
	}
}

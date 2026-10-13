using Unity.Cinemachine;
using UnityEngine;

public class FixedCamera : MonoBehaviour
{
	private BoxCollider2D boxCollider2D;
	private CinemachineCamera cinemachineCamera;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		boxCollider2D = GetComponent<BoxCollider2D>();
		cinemachineCamera = GetComponent<CinemachineCamera>();
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag != "Player")
		{
			return;
		}
		cinemachineCamera.Priority = 1;
	}

	void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.tag != "Player")
		{
			return;
		}
		cinemachineCamera.Priority = -1;
	}
	// Update is called once per frame
	void Update()
	{

	}
}

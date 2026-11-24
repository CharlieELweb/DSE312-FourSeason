using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	[SerializeField] private float defaultSize = 25f;
	public GameObject target;
	private Vector3 targetPosition;
	private float targetSize;
	private Camera camera;
	const float DEFAULT_SMOOTH = 0.1f;
	private float targetSmooth, smooth;
	private float lookAhead;
	private float clampY = 10f;
	private void Start()
	{

		camera = GetComponent<Camera>();
		targetSmooth = DEFAULT_SMOOTH;
		smooth = DEFAULT_SMOOTH;
		lookAhead = 10f;
		camera.orthographicSize = defaultSize;
		StartCoroutine(Init());
	}

	IEnumerator Init()
	{
		yield return new WaitForSeconds(0.05f);
		CalculateTargetPosition();
		transform.position = targetPosition;

	}
	private void FixedUpdate()
	{
		if (smooth < targetSmooth)
		{
			smooth = Mathf.Lerp(smooth, targetSmooth, 0.1f);
		}
		else
		{
			smooth = targetSmooth;
		}
		CalculateTargetPosition();
		float newX = Mathf.SmoothStep(transform.position.x, targetPosition.x, smooth);
		float newY = Mathf.SmoothStep(transform.position.y, targetPosition.y, smooth);
		transform.position = new Vector3(newX, newY, targetPosition.z);
		camera.orthographicSize = Mathf.SmoothStep(camera.orthographicSize, targetSize, smooth);


	}

	private void CalculateTargetPosition()
	{
		if (GameManager.Instance.pause)
		{
			targetPosition = target.transform.position + Vector3.up * 2;
			targetSize = 15f;
			smooth = DEFAULT_SMOOTH;
		}
		else if (target.CompareTag("Player"))
		{
			targetSize = defaultSize;
			targetPosition.x = target.transform.position.x;
			if (target.GetComponent<Rigidbody2D>().linearVelocityX > 1)
			{
				lookAhead = Mathf.Lerp(lookAhead, 10f, 0.05f);
			}
			if (target.GetComponent<Rigidbody2D>().linearVelocityX < -1)
			{
				lookAhead = Mathf.Lerp(lookAhead, -10f, 0.05f);
			}
			if (target.GetComponent<Node2D>().IsOnFloor())
			{
				targetPosition.y = target.transform.position.y;
				targetPosition += new Vector3(0, 10, 0);
			}
			targetPosition.y = Mathf.Clamp(targetPosition.y, target.transform.position.y - clampY, target.transform.position.y + clampY);
			targetPosition += new Vector3(lookAhead, 0, 0);
			targetSmooth = DEFAULT_SMOOTH;
		}
		else if (target.CompareTag("Camera"))
		{
			targetPosition = target.transform.position;
			targetSize = target.GetComponent<Camera>().orthographicSize;
			targetSmooth = target.GetComponent<CameraZone>().smooth;
			smooth = targetSmooth;
		}
		targetPosition.z = -10;

	}
}

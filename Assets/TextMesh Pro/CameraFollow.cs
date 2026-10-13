using System;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	[SerializeField]
	private GameObject target;
	private Vector3 targetPosition;
	// Update is called once per frame
	private void FixedUpdate()
	{
		targetPosition = target.transform.position;
		targetPosition.z = -10;

		transform.position = Vector3.Lerp(transform.position, targetPosition, 0.01f);
	}
}

using System;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	[SerializeField]
	private GameObject target;
	private Vector3 targetPosition;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		targetPosition = target.transform.position;
		targetPosition.z = -10;

		transform.position = Vector3.Lerp(transform.position, targetPosition, 0.01f);
	}
}

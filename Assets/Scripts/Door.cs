using System;
using UnityEngine;

public class Door : MonoBehaviour
{
	[SerializeField] Vector3 offset = Vector3.up * 5;
	[SerializeField] PressurePlate pressurePlate;
	Vector3 originalPosition;
	[SerializeField] float onSpeed = 5f;
	[SerializeField] float offSpeed = 10f;
	[SerializeField] private GameObject dustParticle;
	bool isLanded = false;
	Vector3 targetPosition;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		originalPosition = transform.position;
	}
	private void CreateParticle(GameObject particle)
	{
		GameObject newParticle = Instantiate(particle, transform.position, Quaternion.Euler(new Vector3(-90, 0, 0)));
	}

	// Update is called once per frame
	void Update()
	{
		if (pressurePlate.isOn)
		{
			targetPosition = originalPosition + offset;
			transform.position = Vector3.MoveTowards(transform.position, targetPosition, onSpeed * Time.deltaTime);
			isLanded = false;
		}
		else
		{
			targetPosition = originalPosition;
			transform.position = Vector3.MoveTowards(transform.position, targetPosition, offSpeed * Time.deltaTime);
		}
		if (!pressurePlate.isOn && !isLanded && (transform.position - originalPosition).magnitude < 0.01f)
		{
			CreateParticle(dustParticle);
			isLanded = true;
		}
	}
}

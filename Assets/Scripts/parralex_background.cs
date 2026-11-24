using UnityEngine;

public class parralex_background : MonoBehaviour
{
	[SerializeField] float loopWidth = 55;
	[SerializeField] float loopScaleX = 0.9f;
	[SerializeField] float loopScaleY = 0.9f;
	[SerializeField] float loopTime = 3;
	float posX, posY;

	Vector3 offset;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		offset = transform.position;

	}
	// Update is called once per frame
	void Update()
	{
		posX = Camera.main.transform.position.x * loopScaleX + offset.x;
		posY = Camera.main.transform.position.y * loopScaleY + offset.y;
		if (posX < Camera.main.transform.position.x - loopWidth * loopTime / 2)
		{
			posX += loopWidth * loopTime;
		}
		if (posX > Camera.main.transform.position.x + loopWidth * loopTime / 2)
		{
			posX -= loopWidth * loopTime;
		}
		transform.position = new Vector3(posX, posY, 0);
	}
}

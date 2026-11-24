using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PressurePlate : MonoBehaviour
{
	private SpriteRenderer spriteRenderer;
	private Light2D light2D;
	int objectCount = 0;
	public bool isOn = false;
	Color targetColor = Color.white;
	void OnTriggerEnter2D(Collider2D collision)
	{
		objectCount++;
	}

	void OnTriggerExit2D(Collider2D collision)
	{
		objectCount--;
	}
	void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		light2D = GetComponentInChildren<Light2D>();
	}
	void Update()
	{
		if (objectCount <= 0)
		{
			objectCount = 0;
			isOn = false;
			targetColor = Color.white;
		}
		else
		{
			isOn = true;
			targetColor = Color.green;
		}
		spriteRenderer.color = Color.Lerp(spriteRenderer.color, targetColor, 2 * Time.deltaTime);
		light2D.color = Color.Lerp(light2D.color, targetColor, 2 * Time.deltaTime);

	}
}

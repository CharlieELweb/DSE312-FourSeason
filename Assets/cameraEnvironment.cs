using UnityEngine;

public class CameraEnvironment : MonoBehaviour
{
	private Camera camera;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		camera = GetComponent<Camera>();
	}

	// Update is called once per frame
	void Update()
	{
		if (GameManager.Instance.season == GameManager.Season.Summer)
		{
			camera.backgroundColor = Color.hotPink;
		}
		else
		{
			camera.backgroundColor = Color.lightSkyBlue;
		}
	}
}

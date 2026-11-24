using UnityEngine;
using System.Collections.Generic;

public class Fumarole : MonoBehaviour
{
	[SerializeField] private LayerMask layerMask;

	private PolygonCollider2D polygonCollider2D;
	private ParticleSystem particleSystem;

	private void OnEnable()
	{
		GameManager.OnSeasonChanged += OnSeasonChanged;
	}

	private void OnDisable()
	{
		GameManager.OnSeasonChanged -= OnSeasonChanged;
	}

	private void OnSeasonChanged(GameManager.Season oldSeason, GameManager.Season newSeason)
	{
		switch (newSeason)
		{
			case GameManager.Season.Spring:
				particleSystem.Stop();
				polygonCollider2D.enabled = false;
				break;
			case GameManager.Season.Summer:
				particleSystem.Play();
				polygonCollider2D.enabled = true;
				break;
			case GameManager.Season.Autumn:
				particleSystem.Stop();
				polygonCollider2D.enabled = false;
				break;
			case GameManager.Season.Winter:
				particleSystem.Stop();
				polygonCollider2D.enabled = false;
				break;
		}
	}

	private void Awake()
	{
		polygonCollider2D = GetComponentInChildren<PolygonCollider2D>();
		particleSystem = GetComponentInChildren<ParticleSystem>();
		OnSeasonChanged(GameManager.Season.Spring, GameManager.Instance.season);
	}

	private Vector2 RayCast(Vector3 offset)
	{
		RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position + Vector3.up + offset, Vector2.up, 50f, layerMask);
		float dist = raycastHit2D.distance + 2.0f;
		if (dist <= 2)
		{
			dist = 50f;
		}
		Debug.DrawRay(transform.position + offset, Vector3.up * dist, Color.red);
		return new Vector2(offset.x, dist);
	}

	void Update()
	{
		List<Vector2> points = new List<Vector2> {
			new Vector2(3, 0),
			new Vector2(-3, 0),
		};
		for (float dx = -3; dx <= 3; dx += 0.2f)
		{
			points.Add(RayCast(dx * Vector3.right));
		}
		polygonCollider2D.SetPath(0, points);

	}
}

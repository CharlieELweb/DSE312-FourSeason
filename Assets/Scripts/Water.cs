using UnityEditor.ShaderGraph.Drawing.Inspector.PropertyDrawers;
using UnityEngine;
using System.Collections.Generic;

[ExecuteAlways]
public class Water : MonoBehaviour
{
	[SerializeField] private LayerMask layerMask;
	private int objectCount = 0;
	private Vector3 origionPosition;
	const float summerDrop = 10f;

	private float targetDrop = 0;

	[Range(2, 200)]
	public float waterWidth = 10f;
	[Range(10, 100)]
	public float waterDepth = 10f;
	private WaterVisual waterVisual;
	private BoxCollider2D boxCollider2D;
	[SerializeField] private GameObject ice;
	private BuoyancyEffector2D buoyancyEffector2D;
	private float targetAlpha = 0;
	private void OnValidate()
	{
		waterVisual = GetComponentInChildren<WaterVisual>();
		buoyancyEffector2D = GetComponentInChildren<BuoyancyEffector2D>();
		boxCollider2D = GetComponent<BoxCollider2D>();
		waterVisual.waterWidth = waterWidth;
		waterVisual.waterDepth = waterDepth;
		boxCollider2D.size = new Vector2(waterWidth, 4);
		ice.transform.localScale = new Vector3(waterWidth, 4, 1);
		StartCoroutine(waterVisual.CreateSpline());
		buoyancyEffector2D.transform.localScale = new Vector3(waterWidth, waterDepth, 0);
	}

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
				Frozen(false);
				targetDrop = 0;
				break;
			case GameManager.Season.Summer:
				Frozen(false);
				targetDrop = summerDrop;
				break;
			case GameManager.Season.Autumn:
				Frozen(false);
				break;
			case GameManager.Season.Winter:
				Frozen(true);
				break;
		}
	}

	void Start()
	{
		origionPosition = transform.position;
		OnSeasonChanged(GameManager.Season.Spring, GameManager.Instance.season);
	}
	void FixedUpdate()
	{
		transform.position = Vector3.MoveTowards(transform.position, origionPosition + Vector3.down * targetDrop, 5 * Time.deltaTime);
		ice.GetComponent<SpriteRenderer>().color = new Color(
			ice.GetComponent<SpriteRenderer>().color.r,
			ice.GetComponent<SpriteRenderer>().color.g,
			ice.GetComponent<SpriteRenderer>().color.b,
			Mathf.Lerp(ice.GetComponent<SpriteRenderer>().color.a, targetAlpha, 10 * Time.deltaTime)
		);
		if (ice.GetComponent<SpriteRenderer>().color.a > 0.55f)
		{
			ice.GetComponent<BoxCollider2D>().enabled = true;
		}
		else
		{
			ice.GetComponent<BoxCollider2D>().enabled = false;
		}
	}

	void Frozen(bool froze)
	{
		if (froze)
		{
			targetDrop = origionPosition.y - transform.position.y;
			targetAlpha = 0.6f;
		}
		else
		{
			targetAlpha = 0;
		}
		List<Collider2D> overlappingColliders = new List<Collider2D>();

		ContactFilter2D filter = new ContactFilter2D();
		filter.useTriggers = false;
		filter.SetLayerMask(layerMask);
		int count = boxCollider2D.Overlap(filter, overlappingColliders);
		for (int i = 0; i < count; i++)
		{
			print(overlappingColliders[i].GetComponent<Node2D>());
			if (overlappingColliders[i].GetComponent<Node2D>())
			{
				overlappingColliders[i].GetComponent<Node2D>().froze = froze;
			}
		}
	}
}

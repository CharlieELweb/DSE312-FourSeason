using UnityEditor.ShaderGraph.Drawing.Inspector.PropertyDrawers;
using UnityEngine;

[ExecuteAlways]
public class Water : MonoBehaviour
{
	[Range(2, 100)]
	public float waterWidth = 10f;
	[Range(10, 100)]
	public float waterDepth = 10f;
	private WaterVisual waterVisual;
	[SerializeField] private GameObject ice;
	// private SpriteRenderer spriteRenderer;
	// private AreaEffector2D effector2D;
	// private ParticleSystem particleSystem;
	private BuoyancyEffector2D buoyancyEffector2D;
	private void OnValidate()
	{
		waterVisual = GetComponentInChildren<WaterVisual>();
		buoyancyEffector2D = GetComponentInChildren<BuoyancyEffector2D>();
		waterVisual.waterWidth = waterWidth;
		waterVisual.waterDepth = waterDepth;
		ice.transform.localScale = new Vector3(waterWidth, 2, 1);
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
		print(newSeason);
		switch (newSeason)
		{
			case GameManager.Season.Spring:
				ice.SetActive(false);
				break;
			case GameManager.Season.Summer:
				ice.SetActive(false);
				break;
			case GameManager.Season.Autumn:
				ice.SetActive(false);
				break;
			case GameManager.Season.Winter:
				ice.SetActive(true);
				break;
		}
	}

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		// spriteRenderer = GetComponentInChildren<SpriteRenderer>();
		// boxCollider2D = GetComponent<BoxCollider2D>();
		// particleSystem = GetComponentInChildren<ParticleSystem>();

		// effector2D = GetComponentInChildren<AreaEffector2D>();

		// UpdateFunction();
	}

	// // Update is called once per frame
	// void Update()
	// {
	// 	UpdateFunction();

	// }

	// void UpdateFunction()
	// {
	// 	if (GameManager.Instance.season == GameManager.Season.Summer)
	// 	{
	// 		spriteRenderer.color = Color.blue;
	// 		boxCollider2D.enabled = false;
	// 		effector2D.enabled = true;
	// 		if (!particleSystem.isEmitting)
	// 		{
	// 			particleSystem.Play();
	// 		}
	// 	}
	// 	else if (GameManager.Instance.season == GameManager.Season.Winter)
	// 	{
	// 		spriteRenderer.color = Color.lightBlue;
	// 		boxCollider2D.enabled = true;
	// 		effector2D.enabled = false;
	// 		particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmitting);
	// 	}
	// 	else
	// 	{
	// 		spriteRenderer.color = Color.blue;
	// 		boxCollider2D.enabled = false;
	// 		effector2D.enabled = false;
	// 		particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmitting);
	// 	}
	// }
}

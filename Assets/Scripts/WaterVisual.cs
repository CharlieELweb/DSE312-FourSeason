using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UIElements;

[ExecuteAlways]
public class WaterVisual : MonoBehaviour
{
	[SerializeField] private GameObject waterPoint;

	private List<WaterPoint> waterPoints = new List<WaterPoint>();
	public float waterWidth = 10f;
	public float waterDepth = 10f;
	private int waterPointCount = 2;

	private float spread = 0.01f;
	private SpriteShapeController spriteShapeController;
	private Spline spline;
	private void Awake()
	{
		spriteShapeController = GetComponent<SpriteShapeController>();
		spline = spriteShapeController.spline;
		StartCoroutine(CreateSpline());
		waterWidth = GetComponentInParent<Water>().waterWidth;
		waterDepth = GetComponentInParent<Water>().waterDepth;
	}

	public IEnumerator CreateSpline()
	{
		foreach (Transform child in transform)
		{
			StartCoroutine(Destroy(child.gameObject));
		}
		yield return null;

		InitSpline();
		yield return null;
	}
	IEnumerator Destroy(GameObject gameObject)
	{
		yield return null;
		DestroyImmediate(gameObject);
	}
	private void InitSpline()
	{
		waterPoints.Clear();
		spline.Clear();
		waterPointCount = Math.Max((int)(Mathf.Abs(waterWidth / 2.0f)), 2);
		float delta = waterWidth / (waterPointCount - 1);
		for (int i = 0; i < waterPointCount; i++)
		{
			Vector3 waterPointPosition = new Vector3(-waterWidth / 2 + i * delta, 0, 0);
			spline.InsertPointAt(i, waterPointPosition);
			spline.SetHeight(i, 0.1f);
			spline.SetCorner(i, false);
			spline.SetTangentMode(i, ShapeTangentMode.Broken);
			GameObject newWaterPoint = Instantiate(waterPoint, transform.position + waterPointPosition, Quaternion.identity, transform);
			waterPoints.Add(newWaterPoint.GetComponent<WaterPoint>());
		}
		spline.InsertPointAt(waterPointCount, new Vector3(waterWidth / 2, -waterDepth, 0));
		spline.SetHeight(waterPointCount, 0.1f);
		spline.SetCorner(waterPointCount, false);
		spline.InsertPointAt(waterPointCount + 1, new Vector3(-waterWidth / 2, -waterDepth, 0));
		spline.SetHeight(waterPointCount + 1, 0.1f);
		spline.SetCorner(waterPointCount + 1, false);


	}

	private void Smoothen(int index)
	{
		Vector3 currentPos = spline.GetPosition(index);
		Vector3 prevPos = index > 1 ? spline.GetPosition(index - 1) : currentPos;
		Vector3 nextPos = index < waterPointCount - 1 ? spline.GetPosition(index + 1) : currentPos;
		float scale = Mathf.Min(
			(nextPos - currentPos).magnitude,
			(prevPos - currentPos).magnitude
		) * 0.33f;
		Vector3 leftTangent = (prevPos - currentPos).normalized * scale;
		Vector3 rightTangent = (nextPos - currentPos).normalized * scale;
		SplineUtility.CalculateTangents(currentPos, prevPos, nextPos, gameObject.transform.forward, scale, out rightTangent, out leftTangent);

		spline.SetLeftTangent(index, leftTangent);
		spline.SetRightTangent(index, rightTangent);
	}
	void FixedUpdate()
	{
		UpdateWaterPoints();
		UpdateSpline();
	}

	private void UpdateSpline()
	{
		if (waterPoints.Count != waterPointCount)
		{
			return;
		}
		for (int i = 0; i < waterPointCount; i++)
		{
			spline.SetPosition(i, waterPoints[i].transform.localPosition);
		}
		for (int i = 0; i < waterPointCount; i++)
		{
			Smoothen(i);
		}
	}
	private void UpdateWaterPoints()
	{
		int count = waterPoints.Count;
		for (int i = 1; i < count - 1; i++)
		{
			waterPoints[i].velocity += (waterPoints[i - 1].height - waterPoints[i].height) * spread;
			waterPoints[i].velocity += (waterPoints[i + 1].height - waterPoints[i].height) * spread;
		}
	}

}

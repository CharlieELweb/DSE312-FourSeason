using UnityEngine;
using Abertay.Analytics;
using System.Collections.Generic;

public class LevelMileStone : MonoBehaviour
{
	[SerializeField] private string section;
	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			Dictionary<string, object> eventData = new Dictionary<string, object>
			{
				{"Position", transform.position},
				{"Time", Time.time},
				{"Section", section},
			};
			AnalyticsManager.SendCustomEvent("LevelMileStone", eventData);
			Destroy(gameObject);

		}
	}
}

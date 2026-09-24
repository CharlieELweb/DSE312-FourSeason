using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }
	public Season season = Season.Summer;
	private void Awake()
	{
		Instance = this;
	}

	public enum Season
	{
		Summer,
		Winter,
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			if (season == Season.Summer)
			{
				ChangeToSeason(Season.Winter);
			}
			else
			{
				ChangeToSeason(Season.Summer);
			}

		}
	}

	public void ChangeToSeason(Season newSeason)
	{
		season = newSeason;
	}
}

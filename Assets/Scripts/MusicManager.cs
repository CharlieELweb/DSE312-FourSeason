using UnityEngine;

public class MusicManager : MonoBehaviour
{
	[SerializeField] private float volumn = 1;
	[SerializeField] private GameManager.Season season;
	[SerializeField] private bool always = false;
	private AudioSource audioSource;
	private float targetVolumn;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		audioSource = GetComponent<AudioSource>();
		audioSource.volume = 0;
		OnSeasonChanged(GameManager.Season.Spring, GameManager.Instance.season);


	}
	private void OnEnable()
	{
		GameManager.OnSeasonChanged += OnSeasonChanged;
	}

	private void OnDisable()
	{
		GameManager.OnSeasonChanged -= OnSeasonChanged;
	}
	private void Update()
	{
		if (GameManager.Instance.isTransition)
		{
			audioSource.volume = Mathf.MoveTowards(audioSource.volume, 0, Time.deltaTime / 10f);
		}
		else if (GameManager.Instance.pause)
		{
			audioSource.volume = Mathf.MoveTowards(audioSource.volume, 0, Time.deltaTime);
		}
		else
		{
			audioSource.volume = Mathf.MoveTowards(audioSource.volume, targetVolumn, Time.deltaTime);
		}

	}

	private void OnSeasonChanged(GameManager.Season oldSeason, GameManager.Season newSeason)
	{
		if (always)
		{
			targetVolumn = volumn;
		}
		else if (newSeason == season)
		{
			targetVolumn = volumn;
		}
		else
		{
			targetVolumn = 0.0f;
		}
	}
}

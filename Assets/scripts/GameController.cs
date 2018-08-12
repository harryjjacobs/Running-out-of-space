using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
	public enum GameState
	{
		MAIN_MENU,
		PLAYING,
		PAUSED
	}

	// Preamble at the start of each level
	// Index corresponds to level index
	[SerializeField] private Dialog[] levelDialog;
	[SerializeField] private int maxLevels;
	[SerializeField] private Player playerPrefab;
	[SerializeField] private float initialPlayerInvincibilityDuration = 3f;
	[SerializeField] private float loadLevelDelay = 1f;
	[SerializeField] private CanvasFader canvasFader;

	[SerializeField] private int startAtLevel = 0;

	public delegate void LevelLoaded(Level level);
	public static event LevelLoaded LevelLoadedEvent;
	public delegate void LevelCompleted(Level level);
	public static event LevelCompleted LevelCompletedEvent;
	public delegate void GameCompleted();
	public static event GameCompleted GameCompletedEvent;

	private static GameController instance;
	private Level currentLevel;
	private Player player;
	private GameState state;

	void Awake ()
	{
		if (maxLevels != levelDialog.Length)
		{
			Debug.LogError("Not enough dialog for the number of levels (or too much)");
		}

		instance = this;
		state = GameState.MAIN_MENU;
	}
	
	public void NewGame()
	{
		state = GameState.PLAYING;
		LoadLevel(startAtLevel);
		if (player == null)
			player = Instantiate(playerPrefab, Vector2.zero, Quaternion.identity);
		player.gameObject.SetActive(false);
		FindObjectOfType<SmoothFollow>().Target = player.transform;
	}

	void Update ()
	{
		switch (state)
		{
			case GameState.MAIN_MENU:
				if (currentLevel != null)
				{
					currentLevel.Unload();
					currentLevel = null;
				}
				if (player != null)
				{
					Destroy(player.gameObject);
					player = null;
				}
				break;
			case GameState.PAUSED:
				break;
			case GameState.PLAYING:
				if (Input.GetKeyDown(KeyCode.R))
				{
					ReloadLevel();
				}
				break;
			default:
				break;
		}
	}

	public void PlayLevel()
	{
		if (currentLevel != null && currentLevel.Number >= maxLevels - 1)
		{
			// final level isn't playable
			return;
		}
		currentLevel.Universe.BeginShrinking();
		currentLevel.Universe.RandomizeDebrisMovement();
		SpawnPlayer(currentLevel.Universe.Respawn);
	}

    public void LevelComplete()
	{
		DisablePlayer();

		// Fade out screen
		canvasFader.Fade(CanvasFader.FadeType.OUT);

		if (LevelCompletedEvent != null)
			LevelCompletedEvent(currentLevel);

		int levelIndex = currentLevel.Number + 1;
		if (levelIndex == maxLevels - 1) // last level is special final level
		{
			// Load final level with non-collapsing universe- don't call BeginShrinking()
			StartCoroutine(DelayedLoadLevel(levelIndex));
			//SpawnPlayer(currentLevel.Universe.Respawn);
			Debug.Log("Game Complete!");
			if (GameCompletedEvent != null)
				GameCompletedEvent();
			return;
		}

		// Unload previous and then load the new level
		StartCoroutine(DelayedLoadLevel(levelIndex));
	}

	public void LevelFailed()
	{
		DisablePlayer();
		StartCoroutine(DelayedReloadLevel());
	}

	IEnumerator DelayedReloadLevel()
	{
		yield return new WaitForSeconds(loadLevelDelay);
		ReloadLevel();
	}

	public void ReloadLevel()
	{
		DisablePlayer();

		currentLevel.Reload();

		if (LevelLoadedEvent != null)
			LevelLoadedEvent(currentLevel);
	}

	IEnumerator DelayedLoadLevel(int index)
	{
		yield return new WaitForSeconds(loadLevelDelay);

		// Fade in screen
		canvasFader.Fade(CanvasFader.FadeType.IN);

		// Unload previous
		if (currentLevel != null)
			currentLevel.Unload();
		// Load the new level
		LoadLevel(index);
	}

	void LoadLevel(int index, bool isAReload = false)
	{
		if (index > levelDialog.Length - 1)
		{
			return;
		}

		// Hide the player now that we're loading new level
		HidePlayer();

		// Unload previous level
		if (currentLevel != null)
			currentLevel.Unload();
			currentLevel = null;

		// Create new level and load it
		currentLevel = new Level(index, levelDialog[index]);
		currentLevel.Load(isAReload);

		if (LevelLoadedEvent != null)
			LevelLoadedEvent(currentLevel);
	}

	void DisablePlayer()
	{
		player.GetComponent<Player>().enabled = false;
		player.GetComponent<Movement>().enabled = false;
		player.GetComponent<Weapon>().enabled = false;
	}
	
	void HidePlayer()
	{
		if (player == null) return;
		player.gameObject.SetActive(false);
	}

	void SpawnPlayer(Transform spawnPoint)
	{
		player.transform.position = spawnPoint.position;
		player.transform.rotation = spawnPoint.rotation;
		player.gameObject.SetActive(true);
		player.MakeInvincible(initialPlayerInvincibilityDuration);
		player.GetComponent<Player>().enabled = true;
		player.GetComponent<Movement>().enabled = true;
		player.GetComponent<Weapon>().enabled = true;
	}

	public static GameController Instance
	{
		get
		{
			return instance;
		}
	}

    public int MaxLevels
    {
        get
        {
            return maxLevels;
        }
    }

    public GameState State
    {
        get
        {
            return state;
        }

        set
        {
            state = value;
        }
    }
}

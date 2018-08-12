using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
	public MainMenu MainMenu;
	public GameObject PauseMenu;
	public GameObject InGameDisplay;
	public DialogController DialogController;

	void Start()
	{
		MainMenu.gameObject.SetActive(true);
		DialogController.gameObject.SetActive(false);
		PauseMenu.SetActive(false);
		InGameDisplay.SetActive(false);
		GameController.LevelLoadedEvent += OnLevelLoaded;
	}

	void Update()
	{
		if (!MainMenu.gameObject.activeSelf)
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				if (GameController.Instance.State != GameController.GameState.PAUSED)
				{
					OnPauseButton();
				}
				else
				{
					OnResumeButton();
				}
			}
		}
	}

	public void OnPauseButton()
	{
		Time.timeScale = 0;
		GameController.Instance.State = GameController.GameState.PAUSED;
		PauseMenu.SetActive(true);
	}

	public void OnResumeButton()
	{
		Time.timeScale = 1;
		GameController.Instance.State = GameController.GameState.PLAYING;
		PauseMenu.SetActive(false);
	}

	public void OnMainMenuButton()
	{
		Time.timeScale = 1;
		GameController.Instance.State = GameController.GameState.MAIN_MENU;
		MainMenu.gameObject.SetActive(true);
		DialogController.gameObject.SetActive(false);
		PauseMenu.SetActive(false);
		InGameDisplay.SetActive(false);
	}

	public void OnNewGameButton()
	{
		MainMenu.gameObject.SetActive(false);
		PauseMenu.SetActive(false);
		DialogController.gameObject.SetActive(true);
		InGameDisplay.SetActive(true);
		// Call this after so that DialogController has time to
		// register event listeners
		GameController.Instance.NewGame();
	}

	public void OnExitButton()
	{
		Application.Quit();
	}

	void OnLevelLoaded(Level level)
	{
		DialogController.gameObject.SetActive(true);
	}
}

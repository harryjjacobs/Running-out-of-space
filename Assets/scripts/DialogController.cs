using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
	[SerializeField] private Button continueButton;
	[SerializeField] private Button skipButton;
	[SerializeField] private Text textComponent;
	private Queue<string> dialog = new Queue<string>();

	private bool finalDialog = false;

	void Awake ()
	{
		if (textComponent == null)
		{
			Debug.LogError("No text component assigned to dialog controller");
			return;
		}
		GameController.LevelLoadedEvent += OnLevelLoaded;
	}

    private void OnLevelLoaded(Level level)
    {
        dialog = level.Dialog.GetQueue();
		finalDialog = level.Dialog.IsFinalDialog;
		skipButton.enabled = !finalDialog;

		if (dialog.Count > 0)
		{
			textComponent.text = dialog.Dequeue();
		}
		else
		{
			textComponent.text = "Click continue to begin...";
		}
    }

	public void OnAdvanceDialogButton()
	{
		// Button clicked to advance the dialog
		if (dialog.Count > 0)
		{
			textComponent.text = dialog.Dequeue();
		}
		else
		{
			if (finalDialog)
			{
				return;
			}
			GameController.Instance.PlayLevel();
			gameObject.SetActive(false);
		}
	}

	public void OnSkipDialogButton()
	{
		// Just ignore if we have finished the game
		if (finalDialog) return;

		GameController.Instance.PlayLevel();
		gameObject.SetActive(false);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UniverseName : MonoBehaviour
{
	private Text textComponent;

	void Start ()
	{
		textComponent = GetComponent<Text>();
		GameController.LevelLoadedEvent += OnLevelLoaded;
	}

    private void OnLevelLoaded(Level level)
    {
        textComponent.text = "Universe #" + (level.Number + 1);
    }
}

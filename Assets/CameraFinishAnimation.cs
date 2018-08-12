using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFinishAnimation : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		GameController.LevelCompletedEvent += OnLevelCompleted;
	}

    private void OnLevelCompleted(Level level)
    {
        GetComponent<Animator>().SetTrigger("LevelFinished");
    }
}

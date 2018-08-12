using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
	public Transform Target;
	public float SmoothTime = 2;
	public bool FollowWhenTargetDisabled = false;

	void Start ()
	{
		GameController.LevelLoadedEvent += OnLevelLoaded;
	}

    private void OnLevelLoaded(Level level)
    {
		float z = transform.position.z;
		Vector3 target = level.Universe.Respawn.position;
		target.z = z;
        transform.position = target;
    }

    Vector3 refVel;
	void Update ()
	{
		if (Target == null) return;
		if (!FollowWhenTargetDisabled && !Target.gameObject.activeSelf) return;

		float z = transform.position.z;
		Vector3 target = Vector3.SmoothDamp(transform.position, Target.position, ref refVel, SmoothTime);
		target.z = z;

		transform.position = target;
	}
}

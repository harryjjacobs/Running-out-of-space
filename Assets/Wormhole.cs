using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wormhole : MonoBehaviour
{
	private Universe universe;

	void Start()
	{
		universe = FindObjectOfType<Universe>();
	}

	void Update()
	{
		// Keep it inside the universe
		transform.position = Vector3.ClampMagnitude(transform.position, universe.Radius - 1);
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.GetComponent<Player>())
		{
			col.transform.position = transform.position;
			col.gameObject.GetComponent<Player>().MakeInvincible(2f);
			GameController.Instance.LevelComplete();
		}
	}
}

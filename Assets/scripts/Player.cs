using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Player : MonoBehaviour, IInvincible
{
	[SerializeField] private GameObject InvincibilityBubble;
	[SerializeField] private GameObject ExplosionPrefab;

	private float invincibilityTime;
	private TimeSince timeSinceInvincible;
	private bool isInvincible;

	public void MakeInvincible(float duration)
	{
		timeSinceInvincible = 0;
		invincibilityTime = duration;
		isInvincible = true;
		InvincibilityBubble.SetActive(true);
	}

	void Update()
	{
		if (timeSinceInvincible >= invincibilityTime)
		{
			InvincibilityBubble.SetActive(false);
			isInvincible = false;
		}
	}

	public void MoveTo(Vector2 position)
	{
		transform.localPosition = position;
	}

	public void RotateTo(float angle)
	{
		transform.localRotation = Quaternion.Euler(0, 0, angle);
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (!isInvincible && collision.collider.gameObject.tag == "Debris")
		{
			// death animation
			Instantiate(ExplosionPrefab).transform.position = transform.position;;
			gameObject.SetActive(false);
			GameController.Instance.LevelFailed();
		}
	}
}

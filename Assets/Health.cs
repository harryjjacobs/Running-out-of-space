using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
	[SerializeField] private ColoredExplosion DestructionPrefab;

	public int MaxHealth;
	private int health;

	void Awake()
	{
		health = MaxHealth;
	}

	public void Take(int amount)
	{
		health -= amount;

		if (health <= 0)
		{
			if (DestructionPrefab)
			{
				// Instantiate particle explosition
				ColoredExplosion explosion = Instantiate(DestructionPrefab);
				explosion.transform.position = transform.position;

				// Grab colors from attached sprite renderers and set the explosion to be these colors
				SpriteRenderer[] renderers = gameObject.GetComponentsInChildren<SpriteRenderer>();
				Color[] colors = new Color[renderers.Length];
				for (int i = 0; i < renderers.Length; i++)
				{
					colors[i] = renderers[i].color;
				}
				explosion.SetColors(colors);

				// Trigger particle explosion
				explosion.Explode();
			}
			
			Destroy(gameObject);
		}
	}
}

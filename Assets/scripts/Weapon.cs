using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	public float CooldownTime = 1;
	public Bullet BulletPrefab;
	
	private TimeSince ts; // time since the last shot, used for cooldown

	private Queue<Bullet> bulletStore = new Queue<Bullet>();

    public delegate void BulletCollisionCallback(Bullet bullet);

	// Use this for initialization
	void Start ()
	{
		// Add a few to begin with
		for (int i = 0; i < 5; i++)
		{
			AddBulletToStore();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (ts >= CooldownTime && Input.GetAxis("Fire1") > 0)
		{
			Shoot();
			ts = 0;
		}
	}

	void Shoot()
	{
		Bullet bullet = GetBulletFromStore();

		bullet.SetCollisionCallback((b) =>
		{
			b.gameObject.SetActive(false);
			bulletStore.Enqueue(b);
		});

		bullet.transform.up = transform.up;
		bullet.transform.position = transform.position;
		bullet.gameObject.SetActive(true);
	}

	Bullet GetBulletFromStore()
	{
		
		if (bulletStore.Count == 0)
		{
			AddBulletToStore();
		}
		return bulletStore.Dequeue();
	}

	void AddBulletToStore()
	{
		Bullet bullet = Instantiate(BulletPrefab);
		
		bullet.gameObject.SetActive(false);
		bulletStore.Enqueue(bullet);
	}

}

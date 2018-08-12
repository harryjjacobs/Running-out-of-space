using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnAwake : MonoBehaviour
{
	public float Delay = 3f;

	void Awake ()
	{
		Destroy(gameObject, Delay);
	}

}

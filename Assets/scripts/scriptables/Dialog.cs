using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName="Create Dialog", fileName="New Dialog")]
public class Dialog : ScriptableObject
{
	public string[] Lines;
	public bool IsFinalDialog = false;

	// Returns the lines as a queue
	public Queue<string> GetQueue()
	{
		return new Queue<string>(Lines);
	}
}

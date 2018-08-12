using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level
{
	private int number = 0;
	private Dialog dialog;
	private Universe universe;
	private Random.State randomSeedState;
	
	public Level(int number, Dialog dialog)
	{
		this.number = number;
		this.dialog = dialog;
	}

	public void Reload()
	{
		Unload();
		Load(isAReload: true);
	}

    public void Load(bool isAReload = false)
	{
		// Generate universe
		if (isAReload)
		{
			universe = UniverseGenerator.Instance.Generate(number, GameController.Instance.MaxLevels, randomSeedState);
		}
		else
		{
			// Save the state used to load the level in case we need to relead
			// the same configuration
			randomSeedState = Random.state;
			universe = UniverseGenerator.Instance.Generate(number, GameController.Instance.MaxLevels);
		}		
	}

	public void Unload()
	{
		// Destroy universe
		GameObject.Destroy(universe.gameObject);
	}

	public int Number
    {
        get
        {
            return number;
        }
    }

    public Dialog Dialog
    {
        get
        {
            return dialog;
        }
    }

    public Universe Universe
	{
		get
		{
			return universe;
		}
	}
}

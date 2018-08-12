using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniverseGenerator : MonoBehaviour
{
	public GameObject UniversePrefab;
	public GameObject WormholePrefab;
	public float UniverseRadius = 8f; // This is the radius of the sprite mask in world units. TODO: vary the universe size?
	public int MaxDebris = 50;
	public float PlanetToDebrisRatio = 0.2f;
	public float MaxDebrisSpeed = 0.5f;
	public AnimationCurve difficultyQuantityDistribution; // x and y values between 0..1
	public AnimationCurve difficultyShrinkSpeedDistribution; // x and y values between 0..1
	[SerializeField] private GameObject[] planetPrefabs;
	[SerializeField] private Color[] planetColors = new Color[] { Color.white };
	[SerializeField] private GameObject[] asteroidPrefabs;
	[SerializeField] private Color[] asteroidColors = new Color[] { Color.white };

	private static UniverseGenerator instance;
	
	void Awake()
	{
		if (planetPrefabs.Length == 0 || asteroidPrefabs.Length == 0 ||
			planetColors.Length == 0 || asteroidColors.Length == 0)
		{
			Debug.LogError("Must provide prefabs and colors for the universe generator");
		}
		instance = this;
		Random.InitState(System.Guid.NewGuid().GetHashCode()); // At one point this broke and the seed persisted after play mode had exited. Bug?
	}

	public Universe Generate(int difficulty, int maxDifficulty, UnityEngine.Random.State randomState)
	{
		Random.state = randomState;
		return Generate(difficulty, maxDifficulty, false);
	}

    public Universe Generate(int difficulty, int maxDifficulty, bool changeRandomSeed=true)
    {
		// Creates a new GameObject and adds a universe component
		GameObject universeGameObject = Instantiate(UniversePrefab, Vector2.zero, Quaternion.identity);
		universeGameObject.transform.position.Set(0, 0, 0);
		Universe universe = universeGameObject.GetComponent<Universe>();

		float difficultyRatio = (float) difficulty / (float) maxDifficulty;
		float shrinkSpeed = difficultyShrinkSpeedDistribution.Evaluate(difficultyRatio);

		// Initialise the universe- I AM GOD
		universe.Init(UniverseRadius, shrinkSpeed);

		// Create spawn point near edge
		Vector2 spawnPosition = universe.GetRandomPositionOnEdge(1f);
		Transform respawn = (new GameObject("Respwawn").transform);
		respawn.position = spawnPosition;								// Give it a random position
		respawn.rotation = Quaternion.Euler(0, 0, Random.value * 360); 	// Give it a random rotation
		respawn.SetParent(universeGameObject.transform);				// Parent it to the universe object
		// Set respawn info in universe object
		universe.Respawn = respawn;

		// Create wormhole directly opposite spawn point
		Vector2 wormholePosition = spawnPosition + (universe.GetCentre() - spawnPosition).normalized * 2 * (UniverseRadius - 1);
		GameObject wormhole = Instantiate(WormholePrefab, wormholePosition, Quaternion.identity);
		wormhole.transform.SetParent(universeGameObject.transform);

		int obstacleQuantity = (int) (MaxDebris * difficultyQuantityDistribution.Evaluate(difficultyRatio));
		int numPlanets = (int) (PlanetToDebrisRatio * obstacleQuantity);
		int numDebris = obstacleQuantity - numPlanets;

		List<GameObject> debris = new List<GameObject>();
		for (int i = 0; i < numPlanets; i++)
		{
			GameObject newPlanet = GenerateRandomPlanet();
			newPlanet.transform.SetParent(universeGameObject.transform);
			newPlanet.transform.localPosition = universe.GetRandomPosition(1f);
			debris.Add(newPlanet);
		}

		for (int i = 0; i < numDebris; i++)
		{
			GameObject newAsteroid = GenerateRandomAsteroid();
			newAsteroid.transform.SetParent(universeGameObject.transform);
			newAsteroid.transform.localPosition = universe.GetRandomPosition(1f);
			debris.Add(newAsteroid);
		}
		
		universe.Debris = debris;

        return universe;
    }

	GameObject GenerateRandomPlanet()
	{
		GameObject newPlanet = Instantiate(planetPrefabs.RandomItem());
		newPlanet.transform.rotation = Quaternion.Euler(0, 0, Random.value * 360f);
		foreach (SpriteRenderer r in newPlanet.GetComponentsInChildren<SpriteRenderer>())
		{
			r.color = planetColors.RandomItem();
		}
		return newPlanet;
	}

	GameObject GenerateRandomAsteroid()
	{
		GameObject newAsteroid = Instantiate(asteroidPrefabs.RandomItem());
		newAsteroid.transform.rotation = Quaternion.Euler(0, 0, Random.value * 360f);
		foreach (SpriteRenderer r in newAsteroid.GetComponentsInChildren<SpriteRenderer>())
		{
			r.color = asteroidColors.RandomItem();
		}
		return newAsteroid;
	}

	public static UniverseGenerator Instance
	{
		get
		{
			return instance;
		}
	}
}

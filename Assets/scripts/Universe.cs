using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Universe : MonoBehaviour
{
    public GameObject Mask;
    public EdgeCollider2D UniverseEdge;
    public float MaxDebrisSpeed = 5f;
    public float MaxDebrisRotationSpeed = 50f;

    private float initialRadius;
    private float shrinkSpeed;
    private bool shrinking = false;
    private Transform respawn;
    private List<GameObject> debris = new List<GameObject>();

    public void Init(float universeSize, float shrinkSpeed)
    {
        initialRadius = universeSize;
        this.shrinkSpeed = shrinkSpeed;
    }

    public void BeginShrinking()
    {
        shrinking = true;
    }

    public void RandomizeDebrisMovement()
    {
        foreach(GameObject d in debris)
        {
            d.GetComponent<Rigidbody2D>().velocity = Random.insideUnitCircle * MaxDebrisSpeed * 0.5f;
            d.GetComponent<Rigidbody2D>().angularVelocity = Random.value * MaxDebrisRotationSpeed;
        }
    }

    public Vector2 GetRandomPosition(float padding = 0)
    {
        return Random.insideUnitCircle * (initialRadius - padding);
    }
    
    public Vector2 GetRandomPositionOnEdge(float padding = 0)
    {
        return Random.insideUnitCircle.normalized * (initialRadius - padding);
    }

    public Vector2 GetCentre()
    {
        return Vector2.zero; // TODO: may not always be
    }

    void Update()
    {
        if (!shrinking) return;

        Shrink();
    }

    void Shrink()
    {
        Vector3 newScale = Mask.transform.localScale - Vector3.one * shrinkSpeed * Time.deltaTime;
        if (Mask.transform.localScale.x < 0)
        {
            // Max shrinkage
            return;
        }
        Mask.transform.localScale = newScale;
    }

    public Transform Respawn
	{
		get
		{
			return respawn;
		}
        set
        {
            respawn = value;
        }
	}

    public float Radius
    {
        get
        {
            return (float) initialRadius * Mask.transform.localScale.x;
        }
    }

    public List<GameObject> Debris
    {
        get
        {
            return debris;
        }

        set
        {
            debris = value;
        }
    }
}

using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed = 20;
    public int Damage = 1;
    private Weapon.BulletCollisionCallback callback;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.velocity = transform.up * Speed;
    }

    public void SetCollisionCallback(Weapon.BulletCollisionCallback callback)
    {
        this.callback = callback;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // So we don't shoot ourselves as we fire it
        if (col.gameObject.GetComponent<Player>()) return;

        Health health = col.collider.gameObject.GetComponent<Health>();
        if (health)
        {
            health.Take(Damage);
        }

        if (callback != null)
            callback(this);
    }
}
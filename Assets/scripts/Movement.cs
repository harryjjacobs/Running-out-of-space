using System;
using UnityEngine;
public class Movement : MonoBehaviour
{
    public float ForwardThrust = 20;
    public float TurningSpeed = 20;
    public float MaxSpeed = 200;
    public float AudioPitchFactor = 0.5f;

    private Rigidbody2D rb;
    private float forwardForce;
    private float turnAmount;
    private AudioSource audioSource;

    void OnEnable()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
    }

    void OnDisable()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
    }

    void Start()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();
        
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        audioSource.pitch = 1 + AudioPitchFactor * Mathf.Abs(Input.GetAxis("Vertical"));

        forwardForce = Input.GetAxis("Vertical") * ForwardThrust * Time.deltaTime;
        turnAmount = -Input.GetAxis("Horizontal") * TurningSpeed  * Time.deltaTime;
    }

    void FixedUpdate()
    {
        rb.AddForce(transform.up * forwardForce);
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, MaxSpeed);
        
        rb.rotation += turnAmount;
        rb.angularVelocity = 0;
    }

    public void Stop()
    {
        rb.velocity = Vector2.zero;
    }
}
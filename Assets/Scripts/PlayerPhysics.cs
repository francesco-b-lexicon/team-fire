using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{
    Rigidbody2D rb;
    public float bouncinessMultiplier = .4f;
    Vector2 LastVelocity;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        LastVelocity = rb.velocity;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.name == "Floor")
        {
            var speed = LastVelocity.magnitude * bouncinessMultiplier;
            var direction = Vector2.Reflect(LastVelocity.normalized, col.contacts[0].normal);

            rb.velocity = direction * Mathf.Max(speed, 0f);
        }
    }
}

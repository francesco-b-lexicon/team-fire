using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour, IHittable
{
    public AudioClip hitSound;
    public AudioClip deathSound;
    public bool CanBeHit { get; set; }
    public int health = 3;
    public float immuneWindow = 0.5f;
    private float immuneTimer = 0;
    private bool dead = false;
    private AudioSource sfx;
    private SpriteRenderer sprite;
    private Color normalColor;
    public void Hit()
    {
        if (immuneTimer <= 0)
        {
            health -= 1;
            Debug.Log("ow! I only have " + health + " health remaining");
            if (health <= 0)
            {
                sfx.PlayOneShot(deathSound);
                GetComponent<Collider2D>().enabled = false;
                // GetComponent<ParticleSystem>().Emit();
                dead = true;
                sprite.color = Color.black;

            }
            else
            {
                normalColor = sprite.color;
                sprite.color = Color.red;
                sfx.PlayOneShot(hitSound);
            }
            CanBeHit = false;
            immuneTimer = immuneWindow;
        }
    }

    void Awake()
    {
        sfx = GetComponent<AudioSource>();
        sprite = GetComponent<SpriteRenderer>();
        normalColor = sprite.color;
    }
    void Update()
    {
        if (dead && !sfx.isPlaying)
        {
            Destroy(gameObject);
        }
        if (immuneTimer > 0)
        {
            immuneTimer -= Time.deltaTime;
        }
        else if (!dead)
        {
            sprite.color = normalColor;
            CanBeHit = true;
        }

    }
}

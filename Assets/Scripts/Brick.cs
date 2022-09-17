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
    private bool kill = false;
    private AudioSource sfx;
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
                kill = true;

            }

            sfx.PlayOneShot(hitSound);
            CanBeHit = false;
            immuneTimer = immuneWindow;
        }
    }

    void Awake()
    {
        sfx = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (kill && !sfx.isPlaying)
        {
            Destroy(gameObject);
        }
        if (immuneTimer > 0)
        {
            immuneTimer -= Time.deltaTime;
        }
        else
        {
            CanBeHit = true;
        }
    }
}

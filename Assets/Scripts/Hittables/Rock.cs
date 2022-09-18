using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour, IHittable
{
    public AudioClip hitSound;
    public AudioClip deathSound;
    public Sprite spriteHurt;
    public Sprite spriteDestroyed;
    public bool CanBeHit { get; set; }
    public int health = 3;
    public float immuneWindow = 0.5f;

    public GameObject rockletPrefab;
    public int numberOfRocklets;

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
                dead = true;
                sprite.sprite = spriteDestroyed;

            }
            else
            {
                sprite.sprite = spriteHurt;
                sfx.PlayOneShot(hitSound);
            }
            SpawnRocklets();
            CanBeHit = false;
            immuneTimer = immuneWindow;
        }
    }

    void Awake()
    {
        sfx = GetComponent<AudioSource>();
        sprite = GetComponent<SpriteRenderer>();
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
            CanBeHit = true;
        }

    }

    void SpawnRocklets()
    {
        for (int i = 0; i < numberOfRocklets; i++)
        {
            GameObject littleRock = Instantiate(rockletPrefab, transform.position, transform.rotation);
            float y = Random.Range(0.8f, 1f);
            float x = Random.Range(0, 0.5f) - 1f;
            Vector2 launchVector = new(x, y);
            littleRock.GetComponent<Rigidbody2D>().AddForce(launchVector * 10f, ForceMode2D.Impulse);

        }
    }
}

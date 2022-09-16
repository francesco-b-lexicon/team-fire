using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private string lastKeyUp = null;
    private bool leftLegActive = false;
    private bool rightLegActive = false;
    private bool stumble = false;
    private bool flipped = false;

    private Vector2 walkForce = new Vector2(10,0);
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {   
        if (Input.GetKeyUp(KeyCode.A)) {
            lastKeyUp = "A";
        } else if (Input.GetKeyUp(KeyCode.D)) {
            lastKeyUp = "D";
        }
         else if (Input.GetKeyUp(KeyCode.X)) {
            flipped = !flipped;
        }

        leftLegActive = Input.GetKey(KeyCode.A);
        rightLegActive = Input.GetKey(KeyCode.D);

        stumble = rightLegActive && lastKeyUp == "D" || leftLegActive && lastKeyUp == "A";
    }

    void FixedUpdate() {
        sprite.flipX = flipped;
        if (stumble) { 
            print("stumbled");
            // rb.AddForce(Vector2.left, ForceMode2D.Impulse);
            rb.velocity = Vector2.zero;
        }
        
        else if (leftLegActive | rightLegActive) {
            print("walking using " + (leftLegActive ? "left" : "right") + "leg");
            if (rb.velocity.magnitude < 20) {
                rb.AddForce(walkForce * (flipped ? -1 : 1), ForceMode2D.Force);
            }
            print(rb.velocity);
        }

    }
}

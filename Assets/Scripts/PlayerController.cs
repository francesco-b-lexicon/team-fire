
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace BirbGame
{

    public class PlayerController : MonoBehaviour
    {


        public UIDocument ui;
        private bool runUIUpdates;

        private Rigidbody2D rb;
        private SpriteRenderer sprite;
        private string lastKeyUp = null;

        [HideInInspector]
        public bool leftLegActive = false;
        [HideInInspector]
        public bool rightLegActive = false;
        [HideInInspector]
        public bool leftWingActive = false;
        [HideInInspector]
        public bool rightWingActive = false;

        private bool stumble = false;
        private bool flipped = false;

        private Vector2 walkForce = new(10, 0);
        private Vector2 flyForce = new(0, 20);
        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            sprite = GetComponentInChildren<SpriteRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
            // checking inputs here, but doing the actual updates in FixedUpdate because Physics
            // ie. Update() doesn't run in a fixed time interval, FixedUpdate() does
            if (Input.GetKeyUp(KeyCode.A))
            {
                lastKeyUp = "A";
            }
            else if (Input.GetKeyUp(KeyCode.D))
            {
                lastKeyUp = "D";
            }
            else if (Input.GetKeyUp(KeyCode.X))
            {
                flipped = !flipped;
            }

            leftLegActive = Input.GetKey(KeyCode.A);
            rightLegActive = Input.GetKey(KeyCode.D);
            leftWingActive = Input.GetKey(KeyCode.Q);
            rightWingActive = Input.GetKey(KeyCode.E);

            stumble = rightLegActive && lastKeyUp == "D" || leftLegActive && lastKeyUp == "A";
        }

        void FixedUpdate()
        {
            // what direction we goin?
            sprite.flipX = flipped;

            // walking badly, stumble (zero velocity)
            if (stumble)
            {
                print("stumbled");
                // rb.AddForce(Vector2.left, ForceMode2D.Impulse);
                rb.velocity = new Vector2(0, rb.velocity.y);
            }

            // walking, clip clop
            else if (leftLegActive || rightLegActive && !(leftLegActive && rightLegActive))
            {
                print("walking using " + (leftLegActive ? "left" : "right") + "leg");
                if (rb.velocity.magnitude < 20)
                {
                    rb.AddForce(walkForce * (flipped ? -1 : 1), ForceMode2D.Force);
                }
                print(rb.velocity);
            }

            // flying related, flippity flap
            if (leftWingActive && rightWingActive)
            {
                print("flapping my wings!");
                if (rb.position.y < 20)
                {
                    rb.AddForce(flyForce, ForceMode2D.Force);
                }
            }

        }
    }
}
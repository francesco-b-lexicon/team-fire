
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace BirbGame
{

    public class PlayerController : MonoBehaviour
    {

        public UIDocument ui;
        private Animator animator;
        public float attackWindow = 0.35f;
        public float flapWindow = 0.2f;
        public float legWindow = 0.1f;
        public float flapForce = 10f;
        public float legForce = 10f;

        public float maximumFlightEnergy = 100f;
        public float currentFlightEnergy;
        public float energyUsageUnit = .5f;
        public float energyRestoreUnit = 1f;
        public float energyRestoreRate = .25f;
        bool canFlightEnergyRestore = false;
        float lastEnergyRestoredTime;
        ProgressBar flightEnergyBar;

        private GameObject beak;
        private Rigidbody2D rb;
        private PlayerAudio sfx;
        private SpriteRenderer sprite;
        private string lastLegUp = null;

        private bool leftLegActive = false;
        private bool rightLegActive = false;
        private float leftLegPressTime = 0;
        private float rightLegPressTime = 0;
        private Double leftWingPressedTime = 0.0f;
        private bool leftWingActive = false;
        private Double rightWingPressedTime = 0.0f;
        private bool rightWingActive = false;

        private bool stumble = false;
        private bool flipped = false;

        [HideInInspector]
        public bool attacking = false;
        private Double attackingTimer = 0f;

        private Vector2 walkForce;
        private Vector2 flyForce;
        // Start is called before the first frame update
        void Start()
        {
            ui ??= FindObjectOfType<UIDocument>();
            rb = GetComponent<Rigidbody2D>();
            sprite = GetComponentInChildren<SpriteRenderer>();
            sfx = GetComponent<PlayerAudio>();
            beak = GameObject.FindGameObjectWithTag("beak");
            // set the initial flight energy
            currentFlightEnergy = maximumFlightEnergy;

            flightEnergyBar = ui.rootVisualElement.Query<ProgressBar>("Flight-Bar");
            flightEnergyBar.lowValue = 0;
            flightEnergyBar.highValue = maximumFlightEnergy;

            animator = GetComponentInChildren<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            // checking inputs here, but doing the actual updates in FixedUpdate because Physics
            // ie. Update() doesn't run in a fixed time interval, FixedUpdate() does
            if (Input.GetKeyUp(KeyCode.X))
            {
                flipped = !flipped;
            }
            CheckAttackingInputs();
            CheckLegInputs();
            CheckWingInputs();
            UpdateUIElements();
            stumble = rightLegActive && lastLegUp == "D" || leftLegActive && lastLegUp == "A";

            canFlightEnergyRestore = rb.velocity.y <= 0;
            SetAnimations();
        }

        private void SetAnimations()
        {
            // it's moving in the x axis and it's touching the ground
            animator.SetBool("IsWalking", Math.Abs(GetComponent<Rigidbody2D>().velocity.x) > 0 && canFlightEnergyRestore);

            // it's moving in any direction and it's not on the ground
            animator.SetBool("IsFlying", Math.Abs(GetComponent<Rigidbody2D>().velocity.magnitude) > 0 && !canFlightEnergyRestore);
            animator.SetBool("IsPecking", attacking);
        }

        void FixedUpdate()
        {
            // set forces here in case inspector updates;

            walkForce = new(legForce, 0);
            flyForce = new(0, flapForce);
            // what direction we goin?
            sprite.flipX = flipped;
            Walk();
            Fly();
            Attack();
            RestoreFlightEnergy();
        }

        private void RestoreFlightEnergy()
        {
            if (!canFlightEnergyRestore || currentFlightEnergy >= maximumFlightEnergy) return;

            if (Time.time >= lastEnergyRestoredTime + energyRestoreRate)
            {
                lastEnergyRestoredTime = Time.time;
                currentFlightEnergy = Math.Min(maximumFlightEnergy, currentFlightEnergy + energyRestoreUnit);
            }
        }

        private void CheckAttackingInputs()
        {
            if (Input.GetKey(KeyCode.B) && !attacking)
            {
                sfx.PlayAttackSound();
                attackingTimer = attackWindow;
            }
            else
            {

                attackingTimer -= Time.deltaTime;
            }
            attacking = attackingTimer > 0;
        }

        private void Attack()
        {
            beak.GetComponent<PlayerAttack>().attacking = attacking;
        }

        private void UpdateUIElements()
        {
            var inputbuttons = ui.rootVisualElement.Query("buttonsContainer").AtIndex(0).Children();

            foreach (VisualElement buttonIndicator in inputbuttons)
            {
                var name = buttonIndicator.name;
                if (name == "Wing-L")
                {
                    buttonIndicator.style.backgroundColor = leftWingActive ? Color.green : Color.white;
                }
                if (name == "Wing-R")
                {
                    buttonIndicator.style.backgroundColor = rightWingActive ? Color.green : Color.white;
                }
                if (name == "Leg-L")
                {
                    buttonIndicator.style.backgroundColor = leftLegActive ? Color.green : Color.white;
                }
                if (name == "Leg-R")
                {
                    buttonIndicator.style.backgroundColor = rightLegActive ? Color.green : Color.white;
                }
            }

            flightEnergyBar.value = currentFlightEnergy;
        }

        private void CheckLegInputs()
        {

            if (Input.GetKeyUp(KeyCode.A))
            {
                lastLegUp = "A";
            }
            else if (Input.GetKeyUp(KeyCode.D))
            {
                lastLegUp = "D";
            }

            // check leg controls
            leftLegPressTime -= Time.deltaTime;
            rightLegPressTime -= Time.deltaTime;
            // set a timer for each leg to limit max movement provided
            if (Input.GetKeyDown(KeyCode.A))
            {
                leftLegPressTime = legWindow;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                rightLegPressTime = legWindow;
            }
            // if key is down and timer still alive then flag as active;
            leftLegActive = Input.GetKey(KeyCode.A) && leftLegPressTime > 0;
            rightLegActive = Input.GetKey(KeyCode.D) && rightLegPressTime > 0;

        }

        private void CheckWingInputs()
        {
            // check flight controls
            leftWingActive = false;
            rightWingActive = false;
            if (Input.GetKeyDown(KeyCode.J))
            {
                leftWingPressedTime = flapWindow;
            }
            if (leftWingPressedTime > 0)
            {
                leftWingPressedTime -= Time.deltaTime;
                leftWingActive = true;
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                rightWingPressedTime = flapWindow;
            }
            if (rightWingPressedTime > 0)
            {
                rightWingPressedTime -= Time.deltaTime;
                rightWingActive = true;
            }
        }

        private void Walk()
        {

            // walking badly, stumble (zero velocity)
            if (stumble)
            {
                // Debug.Log("stumbled");
                // rb.AddForce(Vector2.left, ForceMode2D.Impulse);
                rb.velocity = new Vector2(0, rb.velocity.y);
                sfx.PlayStumbleSound();
            }

            // walking, clip clop
            else if (leftLegActive || rightLegActive)
            {
                // Debug.Log("walking using " + (leftLegActive ? "left" : "right") + "leg");
                if (rb.velocity.magnitude < 20)
                {
                    rb.AddForce(walkForce * (flipped ? -1 : 1), ForceMode2D.Force);
                }
                sfx.PlayWalkingSound();
            }
        }

        private void Fly()
        {
            // flying related, flippity flap
            if (leftWingActive && rightWingActive && currentFlightEnergy >= energyUsageUnit)
            {

                // Debug.Log("flapping my wings!");
                sfx.PlayFlyingSound();
                rb.AddForce(flyForce, ForceMode2D.Force);
                rb.AddForce(walkForce * (flipped ? -1 : 1) * 0.35f, ForceMode2D.Force);
                // remove some of the flight energy from birb
                currentFlightEnergy -= energyUsageUnit;
            }
        }


        private bool InputKeyChanged(KeyCode k)
        {
            return Input.GetKeyUp(k) || Input.GetKeyDown(k);
        }
    }
}

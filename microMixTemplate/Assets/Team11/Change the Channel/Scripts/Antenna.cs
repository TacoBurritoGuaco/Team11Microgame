using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace team11
{
    //The class that handles the movement of the antenna.
    public class Antenna : MicrogameInputEvents
    {
        public Vector2 direction; //direction Vector2 used to determine where the antenna should go
        //Based on the position of the stick using the method outlined in the getting started document
        //Start is called before the first frame update

        Rigidbody rb; //the antenna's rigidbody
        public Animator rArrow; //the right arrow animator
        public Animator lArrow; //the left arrow animator
        public Animator punchUI; //The punch ui animator (my hands hurt)

        public float angle; //The current angle of the antenna
        public float speed; //The speed at which this angle will change
        public float acceleration; //The acceleration of speed

        void Start()
        {
            angle = 0; //The base angle at which the antenna starts in
            speed = 0f; //the base speed of the antenna
            acceleration = 0.01f; //the base acceleration value

            rb = GetComponent<Rigidbody>(); //get the rigibody component of the antenna
        }

        //Update is called once per frame
        //Updates specific values we need every frame, such as stick.normalized 
        void Update()
        {
            direction = stick.normalized;
            //stick.normalized is a vector2 with the values of the arcade cabinet joystick

            //If the player is moving the stick either left or right
            if (direction == new Vector2(-1, 0) || direction == new Vector2(1, 0))
            {
                speed += acceleration; //Add acceleration to speed
            }
            else
            {
                speed = 0; //resets speed to minimum
            }
            speed = Mathf.Clamp(speed, 0, 2); //clamps the speed value

            //If statement that updates the current state of the punchUI Animation
            if (angle < (GameObject.Find("StaticScreen").GetComponent<StaticCleaning>().randomClearAngle + 5f) && angle > (GameObject.Find("StaticScreen").GetComponent<StaticCleaning>().randomClearAngle - 5f))
            {
                punchUI.SetBool("InRange", true); //set "inRange" to true
            } else
            {
                punchUI.SetBool("InRange", false); //sets PunchUI to false by default
            }
        }

        private void FixedUpdate()
        {
            //If the player is moving the stick to the right
            if (direction == new Vector2(-1, 0))
            {
                angle += speed; //increases angle by 0.5f
            }
            //If the player is moving the stick to the left
            if (direction == new Vector2(1, 0))
            {
                angle -= speed; //increases angle by 0.5f
            }
            angle = Mathf.Clamp(angle, -50, 50); //clamps the angle to maximum and minimum values
            //(It cannot be greater than this, or less than this)
            transform.rotation = Quaternion.Euler(0, 0, angle);
            //Change the rotation of the object to the angle value
        }
        // Code to execute when the microgame starts
        protected override void OnGameStart()
        {
            rArrow.SetTrigger("GameStart"); //starts the right arrow fade in
            lArrow.SetTrigger("GameStart"); //starts the left arrow fade in
        }
    }
}

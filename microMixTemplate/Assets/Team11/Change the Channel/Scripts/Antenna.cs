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

        public float angle; //The current angle of the antenna
        public float speed; //The speed at which this angle will change

        void Start()
        {
            angle = 0; //The base angle at which the antenna starts in
            speed = 0.5f; //the base speed of the antenna

            rb = GetComponent<Rigidbody>(); //get the rigibody component of the antenna
        }

        //Update is called once per frame
        //Updates specific values we need every frame, such as stick.normalized 
        void Update()
        {
            direction = stick.normalized;
            //stick.normalized is a vector2 with the values of the arcade cabinet joystick
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
            else //If the player is neither moving the stick to the left or to the right
            {
                angle += 0; //adds nothing! leaving the angle in place
            }
            angle = Mathf.Clamp(angle, -75, 75); //clamps the angle to maximum and minimum values
            //(It cannot be greater than this, or less than this)
            transform.rotation = Quaternion.Euler(0, 0, angle);
            //Change the rotation of the object to the angle value
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace team11
{
    public class StaticCleaning : MicrogameInputEvents
    {

        //Variables required for the static cleaning
        private float minRotationAngle = -75f;
        private float maxRotationAngle = 75f;
        public float currentAntennaAngle;
        public float randomClearAngle;
        private float maxStaticDistance;
        private float currentStaticDistance;

        public SpriteRenderer Static;

        private float staticOpacity = 1f;
        private float currentStaticOpacity;
        public float minStaticOpacity = 0.5f;

        public float winPressTimes = 3;
        private float clearTimes = 0;

        void Start ()
        {
            //Generate a random clear angle
            randomStatic();

            //Calculate the maximum static distance 
            float distanceToMinRotation = Mathf.Abs(randomClearAngle-minRotationAngle);
            float distanceToMaxRotation = Mathf.Abs(randomClearAngle - maxRotationAngle);
            maxStaticDistance = Mathf.Max(distanceToMinRotation, distanceToMaxRotation);

        }

        void Update ()
        {
            //Find the antenna current angle
            currentAntennaAngle = GameObject.Find("Antenna").GetComponent<Antenna>().angle;

            //Find the in-game antenna current angle & make sure the random clear angle is different from it
            if (currentAntennaAngle <= (randomClearAngle + 1) || currentAntennaAngle >= (randomClearAngle - 1))
            {
                //Only press the button when at the correct angle
                if (button1.IsPressed())
                {
                    randomStatic();

                    //Increase clear time when a button is pressed successfully 
                    clearTimes+=1;

                    //When clear time equals to win press time, the player wins the game
                    if (clearTimes == winPressTimes)
                    {
                        ReportGameCompletedEarly();
                    }
                }
            }
         

            //Calcualting current static opacity
            currentStaticDistance = Mathf.Abs(currentAntennaAngle - randomClearAngle);
            currentStaticOpacity = currentStaticDistance / maxStaticDistance + minStaticOpacity;

            Static.color = new Color(255, 255, 255, currentStaticOpacity);

        }

        public void randomStatic() //Create a random clear angle everytime when function is called
        {
            randomClearAngle = Random.Range(minRotationAngle, maxRotationAngle);
        }



    }
}

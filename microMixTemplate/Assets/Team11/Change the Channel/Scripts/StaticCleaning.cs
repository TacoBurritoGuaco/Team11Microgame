using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


namespace team11
{
    public class StaticCleaning : MicrogameInputEvents
    {

        //Variables required for the static cleaning
        private float minRotationAngle = -75f;
        private float maxRotationAngle = 75f;
        public float currentAntennaAngle;
        public float randomClearAngle;

        private float currentStaticDistance;

        public SpriteRenderer Static;
        public AnimationCurve fading; //The animation curve of fading static
        float interpolation; //the interpolation value of the static lerp

        private float staticOpacity = 1f;
        public float currentStaticOpacity;
        public float minStaticOpacity = 0.1f;

        public float winPressTimes = 3;
        private float clearTimes = 0;

        void Start ()
        {
            //Generate a random clear angle
            randomStatic();
        }

        void Update ()
        {
            //Find the antenna current angle
            currentAntennaAngle = GameObject.Find("Antenna").GetComponent<Antenna>().angle;

            //Find the in-game antenna current angle & make sure the random clear angle is different from it
            if (currentAntennaAngle < (randomClearAngle + 1.5f) && currentAntennaAngle > (randomClearAngle - 1.5f))
            {
                //Only press the button when at the correct angle
                if (button1.WasPressedThisFrame()) //IMPORTANT DISTINCTION
                    //WasPressedThisFrame() does this only ONCE per frame, unlike IS pressed
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
            currentStaticDistance = Mathf.Abs(currentAntennaAngle - randomClearAngle); //Get the distance between the antenna and the random clear angle
            currentStaticOpacity = currentStaticDistance / 75f; //Divide by the range in which you want the static to begin fading

            interpolation = fading.Evaluate(currentStaticOpacity); //Set the interpolation value of the slope
            Static.color = new Color(1, 1, 1, Mathf.Lerp(0, 1, interpolation)); //Interpolate (I did it yay)

        }

        public void randomStatic() //Create a random clear angle everytime when function is called
        {
            randomClearAngle = Random.Range(minRotationAngle, maxRotationAngle);
        }
    }
}

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
        public bool hasPlayerWon; //if the player has won or not

        public Animator cameraAnim; //The camera's animator
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
            currentAntennaAngle = GameObject.Find("Antenna").GetComponent<Antenna>().angle; //find the antenna's angle at the beginning of the game
            //Generate a random clear angle not within a specified range of the antenna
            //This is why we get the antenna at the beginning of the game as well
            randomStatic();
        }

        void Update ()
        {
            if (hasPlayerWon)
            {
                Static.color = new Color(1, 1, 1, 0); //Remove the static
                return; //Stop anything else from happening if the player has won
            }

            //Find the antenna current angle
            currentAntennaAngle = GameObject.Find("Antenna").GetComponent<Antenna>().angle;

            
            //Only press the button when at the correct angle
            if (button1.WasPressedThisFrame()) //IMPORTANT DISTINCTION
            //WasPressedThisFrame() does this only ONCE per frame, unlike IS pressed
            {
                cameraAnim.SetTrigger("SuccesfulHit");
                //Find the in-game antenna current angle & make sure the random clear angle is different from it
                if (currentAntennaAngle < (randomClearAngle + 1.5f) && currentAntennaAngle > (randomClearAngle - 1.5f))
                {
                    randomStatic();

                    //Increase clear time when a button is pressed successfully 
                    clearTimes+=1;

                    //When clear time equals to win press time, the player wins the game
                    if (clearTimes == winPressTimes)
                    {
                        cameraAnim.SetTrigger("Win");
                        hasPlayerWon = true;
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
            //While a number is within a range we do not want the number to be in
            while (randomClearAngle < (currentAntennaAngle + 30) && randomClearAngle > (currentAntennaAngle - 30)) {
                randomClearAngle = Random.Range(minRotationAngle, maxRotationAngle); //randomize the angle
            }
        }
    }
}

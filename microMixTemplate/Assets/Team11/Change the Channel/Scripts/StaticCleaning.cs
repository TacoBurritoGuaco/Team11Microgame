using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace team11
{


    public class StaticCleaning : MonoBehaviour
    {

        //Variables required for the static cleaning
        private float minRotationAngle = -75f;
        private float maxRotationAngle = 75f;
        private float randomClearAngle;
        private float maxStaticDistance;
        private float currentStaticDistance;

        //Place holder for current angle
        public float currentAngle;

        private float staticOpacity = 1f;
        private float currentStaticOpacity;
        public float minStaticOpacity = 0.5f;


        void Start ()
        {
            //Set the random clear angle when the game start between the max & min range
            randomClearAngle = Random.Range(minRotationAngle, maxRotationAngle);

            //Calculate the maximum static distance 
            float distanceToMinRotation = Mathf.Abs(randomClearAngle-minRotationAngle);
            float distanceToMaxRotation = Mathf.Abs(randomClearAngle - maxRotationAngle);
            maxStaticDistance = Mathf.Max(distanceToMinRotation, distanceToMaxRotation);

        }

        void Update ()
        {
            //Calcualting current static opacity
            currentStaticDistance = Mathf.Abs(currentAngle - randomClearAngle);
            currentStaticOpacity = currentStaticDistance / maxStaticDistance + minStaticOpacity;


        }




    }
}

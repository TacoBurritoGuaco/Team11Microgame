using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace team11
{
    public class HandSlap : MicrogameInputEvents
    {


        //Reference the hand animator
        public Animator handAnimator;

        public AudioSource slamSound; //the sound the hand makes when it hits the TV

        // Refernce the static clean script
        public StaticCleaning cleaningReference;


        //When press the button, slap hand, then delay for TV shake and press check
        protected override void OnButton1Pressed(InputAction.CallbackContext context)
        {
            base.OnButton1Pressed(context);
 

            if (button1.WasPressedThisFrame())
            {
                handAnimator.Play("Flash in");
                Invoke("tvShaking", 0.5f);
            }
        }

        //Reference the static clean script for tv shake and clear time add
        public void tvShaking()
        {
            slamSound.Play();
            cleaningReference.slapTV();
        }

    }
}

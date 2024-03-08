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


        public StaticCleaning cleaningReference;

        void Update()
        {
        }

        protected override void OnButton1Pressed(InputAction.CallbackContext context)
        {
            base.OnButton1Pressed(context);
            Debug.Log("hand slap");

            if (button1.WasPressedThisFrame())
            {
                handAnimator.Play("Hand Slap");
                Invoke("tvShaking", 0.5f);
             

            }
        }

        public void tvShaking()
        {
            cleaningReference.slapTV();
        }

    }
}

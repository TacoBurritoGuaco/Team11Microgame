using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

namespace team11
{
    public class Channels : MicrogameEvents
    {
        public int currentChannel; //the current channel that should be playing
        public float lastClearTime;

        public Animator TV; //the tv animator
        private MeshRenderer mRenderer; //the object's mesh renderer
        public List<Material> channels; //the channel materials that will switch around

        void Start()
        {
            mRenderer = GetComponent<MeshRenderer>();
            changeChannel();
        }

        // Update is called once per frame
        void Update()
        {
            //If the last clearTime is NOT equals to the current clear time 
            if (!(lastClearTime == GameObject.Find("StaticScreen").GetComponent<StaticCleaning>().clearTimes)) {
                changeChannel();
            }
        }
        public void changeChannel()
        {
            //Changes the channel texture animation
            currentChannel = (int)Random.Range(0, 4);
            TV.SetInteger("change", currentChannel);

            //Changes the channel texture
            mRenderer.material = channels[currentChannel];

            lastClearTime = GameObject.Find("StaticScreen").GetComponent<StaticCleaning>().clearTimes; //updates last clear time to match current clear time
            //Effectively changing the material's current "Channel"
        }
    }
}

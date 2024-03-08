using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

namespace team11
{
    public class Channels : MicrogameEvents
    {
        public int currentChannel; //the current channel that should be playing
        public int lastChannel; //The last channel that was selected
        public float lastClearTime;

        public Animator TV; //the tv animator
        private MeshRenderer mRenderer; //the object's mesh renderer
        public List<Material> channels; //the channel materials that will switch around
        public List<AudioSource> channelMusic; //a list of all the current audioMusic

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
            while (currentChannel == lastChannel) //while the currentChannel is equal to the lastChannel
            {
                currentChannel = (int)Random.Range(0, 4);
            }

            lastChannel = currentChannel; //Updates the lastChannel to currentChannel
            TV.SetInteger("change", currentChannel);
            GameObject.Find("StaticScreen").GetComponent<StaticCleaning>().currentChannel = channelMusic[currentChannel]; //change music to music correspondant to the clip
            GameObject.Find("StaticScreen").GetComponent<StaticCleaning>().currentChannel.Play(); //begin playing the audio

            //Changes the channel texture
            mRenderer.material = channels[currentChannel];

            lastClearTime = GameObject.Find("StaticScreen").GetComponent<StaticCleaning>().clearTimes; //updates last clear time to match current clear time
            //Effectively changing the material's current "Channel"
        }
    }
}

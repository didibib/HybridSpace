using UnityEngine;
using System.Collections;

namespace NewtonVR.Example
{
    public class Flashlight : NVRInteractableItem
    {
        public GameObject spotlight;

        protected override void Start()
        {
            base.Start();
        }

        public override void UseButtonDown()
        {
            base.UseButtonDown();

            spotlight.GetComponent<Light>().enabled = !spotlight.GetComponent<Light>().enabled;
        }
    }
}


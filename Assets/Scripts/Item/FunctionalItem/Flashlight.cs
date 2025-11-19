using System;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

namespace Item.FunctionalItem
{
    public class Flashlight: GameItemBase
    {

        private float flickerChance = 0.3f;
        private float flickerTime = 1;
        private float lastCheck;

        private Light2D personalIllumination;
        private Light2D flashlightBeam;


        public void Awake()
        {
            personalIllumination = transform.GetChild(0).GetComponent<Light2D>();
            flashlightBeam = transform.GetChild(1).GetComponent<Light2D>();
            lastCheck = 0;
        }

        public override void holdTick(Vector2 holdDirection, float holdOffset)
        {
            proceduralTransformUpdate(holdDirection, holdOffset);
        }


        private void proceduralTransformUpdate(Vector2 holdDirection, float holdOffset)
        {
            Transform form = gameObject.transform;
            
            double theta = holdDirection.x == 0 ? 0 : Math.Atan2(holdDirection.y,holdDirection.x);
            
            theta = theta * (180f) / Mathf.PI;

            theta -= 90f;
            
            form.localRotation = Quaternion.Euler(0, 0, (float)theta);
        
        }
    }
}
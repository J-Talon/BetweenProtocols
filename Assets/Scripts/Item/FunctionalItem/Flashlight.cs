using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Item.FunctionalItem
{
    public class Flashlight: GameItemBase
    {

        private float flickerChance = 0;
        private float flickerTime = 0;

        private Light2D personalIllumination;
        private Light2D flashlightBeam;


        public void Awake()
        {
            personalIllumination = transform.GetChild(0).GetComponent<Light2D>();
            flashlightBeam = transform.GetChild(1).GetComponent<Light2D>();
        }

        public override void holdTick(Vector2 holdDirection, Vector2 entityTransform, float holdOffset)
        {
            proceduralTransformUpdate(holdDirection, entityTransform, holdOffset);
        }


        private void proceduralTransformUpdate(Vector2 holdDirection, Vector2 entityTransform, float holdOffset)
        {
            Transform form = gameObject.transform;
            
            double theta = holdDirection.x == 0 ? 0 : Math.Atan2(holdDirection.y,holdDirection.x);
            
            theta = theta * (180f) / Mathf.PI;

            theta -= 90f;
            
            form.localRotation = Quaternion.Euler(0, 0, (float)theta);
            Debug.Log(form.localRotation);
        }
    }
}
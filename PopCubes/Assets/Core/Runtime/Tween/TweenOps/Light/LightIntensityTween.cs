using UnityEngine;
using System;
using ZigZaggle.Core.Math;


namespace ZigZaggle.Core.Tweening
{
    internal class LightIntensityTween : TweenOperation
    {
        public float EndValue { get; private set; }


        private readonly Light target;
        private float start;


        public LightIntensityTween(Light target, float endValue, float duration, float delay, bool obeyTimescale,
            AnimationCurve curve, Tween.TweenLoopType tweenLoop, Action startCallback, Action completeCallback)
        {
            SetCommonProperties(Tween.TweenType.LightIntensity, target.GetInstanceID(), duration, delay, obeyTimescale, curve,
                tweenLoop, startCallback, completeCallback);


            this.target = target;
            EndValue = endValue;
        }


        protected override bool SetStartValue()
        {
            if (target == null) return false;
            start = target.intensity;
            return true;
        }

        protected override void Process(float percentage)
        {
            var calculatedValue = Interpolation.LinearInterpolate(start, EndValue, percentage);
            target.intensity = calculatedValue;
        }


        public override void Loop()
        {
            ResetStartTime();
            target.intensity = start;
        }

        public override void PingPong()
        {
            ResetStartTime();
            target.intensity = EndValue;
            EndValue = start;
            start = target.intensity;
        }
    }
}
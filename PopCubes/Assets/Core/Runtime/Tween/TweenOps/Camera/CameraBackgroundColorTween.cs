using UnityEngine;
using System;
using ZigZaggle.Core.Math;


namespace ZigZaggle.Core.Tweening
{
    internal class CameraBackgroundColorTween : TweenOperation
    {
        protected override bool interruptOnStart => true;
        public Color EndValue { get; private set; }


        private readonly Camera target;
        private Color start;


        public CameraBackgroundColorTween(Camera target, Color endValue, float duration, float delay,
            bool obeyTimescale,
            AnimationCurve curve, Tween.TweenLoopType tweenLoop, Action startCallback, Action completeCallback)
        {
            SetCommonProperties(Tween.TweenType.ImageColor, target.GetInstanceID(), duration, delay, obeyTimescale, curve,
                tweenLoop, startCallback, completeCallback);


            this.target = target;
            EndValue = endValue;
        }


        protected override bool SetStartValue()
        {
            if (target == null) return false;
            start = target.backgroundColor;
            return true;
        }

        protected override void Process(float percentage)
        {
            var calculatedValue = Interpolation.LinearInterpolate(start, EndValue, percentage);
            target.backgroundColor = calculatedValue;
        }


        public override void Loop()
        {
            ResetStartTime();
            target.backgroundColor = start;
        }

        public override void PingPong()
        {
            ResetStartTime();
            target.backgroundColor = EndValue;
            EndValue = start;
            start = target.backgroundColor;
        }
    }
}
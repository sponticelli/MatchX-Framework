using UnityEngine;
using System;
using ZigZaggle.Core.Math;

namespace ZigZaggle.Core.Tweening
{
    internal class LightRangeTween : TweenOperation
    {
        protected override bool interruptOnStart => true;
        public float EndValue { get; private set; }


        private readonly Light target;
        private float start;


        public LightRangeTween(Light target, float endValue, float duration, float delay, bool obeyTimescale,
            AnimationCurve curve, Tween.TweenLoopType tweenLoop, Action startCallback, Action completeCallback)
        {
            SetCommonProperties(Tween.TweenType.LightRange, target.GetInstanceID(), duration, delay, obeyTimescale, curve,
                tweenLoop, startCallback, completeCallback);


            this.target = target;
            EndValue = endValue;
        }


        protected override bool SetStartValue()
        {
            if (target == null) return false;
            start = target.range;
            return true;
        }

        protected override void Process(float percentage)
        {
            var calculatedValue = Interpolation.LinearInterpolate(start, EndValue, percentage);
            target.range = calculatedValue;
        }


        public override void Loop()
        {
            ResetStartTime();
            target.range = start;
        }

        public override void PingPong()
        {
            ResetStartTime();
            target.range = EndValue;
            EndValue = start;
            start = target.range;
        }
    }
}
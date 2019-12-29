using UnityEngine;
using System;
using ZigZaggle.Core.Math;

namespace ZigZaggle.Core.Tweening
{
    internal class FieldOfViewTween : TweenOperation
    {
        public float EndValue { get; private set; }


        private readonly Camera target;
        private float start;


        public FieldOfViewTween(Camera target, float endValue, float duration, float delay, bool obeyTimescale,
            AnimationCurve curve, Tween.TweenLoopType tweenLoop, Action startCallback, Action completeCallback)
        {
            SetCommonProperties(Tween.TweenType.FieldOfView, target.GetInstanceID(), duration, delay, obeyTimescale, curve,
                tweenLoop, startCallback, completeCallback);


            this.target = target;
            EndValue = endValue;
        }


        protected override bool SetStartValue()
        {
            if (target == null) return false;
            start = target.fieldOfView;
            return true;
        }

        protected override void Process(float percentage)
        {
            var calculatedValue = Interpolation.LinearInterpolate(start, EndValue, percentage);
            target.fieldOfView = calculatedValue;
        }


        public override void Loop()
        {
            ResetStartTime();
            target.fieldOfView = start;
        }

        public override void PingPong()
        {
            ResetStartTime();
            target.fieldOfView = EndValue;
            EndValue = start;
            start = target.fieldOfView;
        }
    }
}
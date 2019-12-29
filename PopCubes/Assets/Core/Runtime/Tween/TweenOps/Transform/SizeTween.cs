using UnityEngine;
using System;
using ZigZaggle.Core.Math;


namespace ZigZaggle.Core.Tweening
{
    internal class SizeTween : TweenOperation
    {
        public Vector2 EndValue { get; private set; }


        private readonly RectTransform target;
        private Vector2 start;


        public SizeTween(RectTransform target, Vector2 endValue, float duration, float delay, bool obeyTimescale,
            AnimationCurve curve, Tween.TweenLoopType tweenLoop, Action startCallback, Action completeCallback)
        {
            SetCommonProperties(Tween.TweenType.Size, target.GetInstanceID(), duration, delay, obeyTimescale, curve, tweenLoop,
                startCallback, completeCallback);


            this.target = target;
            EndValue = endValue;
        }


        protected override bool SetStartValue()
        {
            if (target == null) return false;
            start = target.sizeDelta;
            return true;
        }

        protected override void Process(float percentage)
        {
            var calculatedValue = Interpolation.LinearInterpolate(start, EndValue, percentage);
            target.sizeDelta = calculatedValue;
        }


        public override void Loop()
        {
            ResetStartTime();
            target.sizeDelta = start;
        }

        public override void PingPong()
        {
            ResetStartTime();
            target.sizeDelta = EndValue;
            EndValue = start;
            start = target.sizeDelta;
        }
    }
}
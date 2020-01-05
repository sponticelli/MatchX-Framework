using UnityEngine;
using System;
using ZigZaggle.Core.Math;


namespace ZigZaggle.Core.Tweening
{
    internal class AnchoredPositionTween : TweenOperation
    {
        protected override bool interruptOnStart => true;
        public Vector2 EndValue { get; private set; }


        private readonly RectTransform target;
        private Vector2 start;


        public AnchoredPositionTween(RectTransform target, Vector2 endValue, float duration, float delay, bool obeyTimescale,
            AnimationCurve curve, Tween.TweenLoopType tweenLoop, Action startCallback, Action completeCallback)
        {
            SetCommonProperties(Tween.TweenType.AnchoredPosition, target.GetInstanceID(), duration, delay, obeyTimescale,
                curve, tweenLoop, startCallback, completeCallback);


            this.target = target;
            EndValue = endValue;
        }


        protected override bool SetStartValue()
        {
            if (target == null) return false;
            start = target.anchoredPosition;
            return true;
        }

        protected override void Process(float percentage)
        {
            Vector3 calculatedValue = Interpolation.LinearInterpolate(start, EndValue, percentage);
            target.anchoredPosition = calculatedValue;
        }


        public override void Loop()
        {
            ResetStartTime();
            target.anchoredPosition = start;
        }

        public override void PingPong()
        {
            ResetStartTime();
            target.anchoredPosition = EndValue;
            EndValue = start;
            start = target.anchoredPosition;
        }
    }
}
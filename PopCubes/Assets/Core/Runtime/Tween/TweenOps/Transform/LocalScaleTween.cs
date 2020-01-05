using UnityEngine;
using System;
using ZigZaggle.Core.Math;


namespace ZigZaggle.Core.Tweening
{
    internal class LocalScaleTween : TweenOperation
    {
        protected override bool interruptOnStart => true;
        public Vector3 EndValue { get; private set; }


        private readonly Transform target;
        private Vector3 start;


        public LocalScaleTween(Transform target, Vector3 endValue, float duration, float delay, bool obeyTimescale,
            AnimationCurve curve, Tween.TweenLoopType tweenLoop, Action startCallback, Action completeCallback)
        {
            SetCommonProperties(Tween.TweenType.LocalScale, target.GetInstanceID(), duration, delay, obeyTimescale, curve,
                tweenLoop, startCallback, completeCallback);


            this.target = target;
            EndValue = endValue;
        }


        protected override bool SetStartValue()
        {
            if (target == null) return false;
            start = target.localScale;
            return true;
        }

        protected override void Process(float percentage)
        {
            Vector3 calculatedValue = Interpolation.LinearInterpolate(start, EndValue, percentage);
            target.localScale = calculatedValue;
        }


        public override void Loop()
        {
            ResetStartTime();
            target.localScale = start;
        }

        public override void PingPong()
        {
            ResetStartTime();
            target.localScale = EndValue;
            EndValue = start;
            start = target.localScale;
        }
    }
}
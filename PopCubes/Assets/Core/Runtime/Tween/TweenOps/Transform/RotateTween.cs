using UnityEngine;
using System;
using ZigZaggle.Core.Math;

namespace ZigZaggle.Core.Tweening
{
    internal class RotateTween : TweenOperation
    {
        public Vector3 EndValue { get; private set; }


        private readonly Transform target;
        private Vector3 start;
        private readonly Space space;
        private Vector3 previous;


        public RotateTween(Transform target, Vector3 endValue, Space space, float duration, float delay, bool obeyTimescale,
            AnimationCurve curve, Tween.TweenLoopType tweenLoop, Action startCallback, Action completeCallback)
        {
            SetCommonProperties(Tween.TweenType.Rotation, target.GetInstanceID(), duration, delay, obeyTimescale, curve, tweenLoop,
                startCallback, completeCallback);


            this.target = target;
            EndValue = endValue;
            this.space = space;
        }


        protected override bool SetStartValue()
        {
            if (target == null) return false;
            start = target.localEulerAngles;
            return true;
        }

        protected override void Process(float percentage)
        {
            if (percentage == 0)
            {
                target.localEulerAngles = start;
            }

            Vector3 spinAmount = Interpolation.LinearInterpolate(Vector3.zero, EndValue, percentage);
            var spinDifference = spinAmount - previous;
            previous += spinDifference;
            target.Rotate(spinDifference, space);
        }


        public override void Loop()
        {
            previous = Vector3.zero;
            ResetStartTime();
        }

        public override void PingPong()
        {
            previous = Vector3.zero;
            EndValue *= -1;
            ResetStartTime();
        }
    }
}
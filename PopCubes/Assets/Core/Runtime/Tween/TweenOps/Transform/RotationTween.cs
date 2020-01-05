using UnityEngine;
using System;
using ZigZaggle.Core.Math;


namespace ZigZaggle.Core.Tweening
{
    internal class RotationTween : TweenOperation
    {
        protected override bool interruptOnStart => true;
        public Vector3 EndValue { get; private set; }


        private readonly Transform target;
        private Vector3 start;


        public RotationTween(Transform target, Vector3 endValue, float duration, float delay, bool obeyTimescale,
            AnimationCurve curve, Tween.TweenLoopType tweenLoop, Action startCallback, Action completeCallback)
        {
            SetCommonProperties(Tween.TweenType.Rotation, target.GetInstanceID(), duration, delay, obeyTimescale, curve, tweenLoop,
                startCallback, completeCallback);


            this.target = target;
            EndValue = endValue;
        }


        protected override bool SetStartValue()
        {
            if (target == null) return false;
            start = target.eulerAngles;
            return true;
        }

        protected override void Process(float percentage)
        {
            var calculatedValue =
                Quaternion.Euler(Interpolation.LinearInterpolateRotational(start, EndValue, percentage));
            target.rotation = calculatedValue;
        }


        public override void Loop()
        {
            ResetStartTime();
            target.eulerAngles = start;
        }

        public override void PingPong()
        {
            ResetStartTime();
            target.eulerAngles = EndValue;
            EndValue = start;
            start = target.eulerAngles;
        }
    }
}
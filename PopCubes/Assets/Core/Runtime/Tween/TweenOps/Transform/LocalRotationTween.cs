using UnityEngine;
using System;
using ZigZaggle.Core.Math;

namespace ZigZaggle.Core.Tweening
{
    internal class LocalRotationTween : TweenOperation
    {
        protected override bool interruptOnStart => true;
        public Vector3 EndValue { get; private set; }


        private readonly Transform target;
        private Vector3 start;


        public LocalRotationTween(Transform target, Vector3 endValue, float duration, float delay, bool obeyTimescale,
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
            start = target.localEulerAngles;
            return true;
        }

        protected override void Process(float percentage)
        {
            var calculatedValue =
                Quaternion.Euler(Interpolation.LinearInterpolateRotational(start, EndValue, percentage));
            target.localRotation = calculatedValue;
        }


        public override void Loop()
        {
            ResetStartTime();
            target.localEulerAngles = start;
        }

        public override void PingPong()
        {
            ResetStartTime();
            target.localEulerAngles = EndValue;
            EndValue = start;
            start = target.localEulerAngles;
        }
    }
}
using UnityEngine;
using System;
using ZigZaggle.Core.Math;

namespace ZigZaggle.Core.Tweening
{
    internal class Vector3Tween : TweenOperation
    {
        public Vector3 EndValue { get; private set; }

        private readonly Action<Vector3> valueUpdatedCallback;
        private Vector3 start;

        public Vector3Tween(Vector3 startValue, Vector3 endValue, Action<Vector3> valueUpdatedCallback,
            float duration, float delay, bool obeyTimescale, AnimationCurve curve, Tween.TweenLoopType tweenLoop,
            Action startCallback, Action completeCallback)
        {
            SetCommonProperties(Tween.TweenType.Value, -1, duration, delay, obeyTimescale, curve, tweenLoop, startCallback,
                completeCallback);
            this.valueUpdatedCallback = valueUpdatedCallback;
            start = startValue;
            EndValue = endValue;
        }

        protected override bool SetStartValue()
        {
            return true;
        }

        protected override void Process(float percentage)
        {
            var calculatedValue = Interpolation.LinearInterpolate(start, EndValue, percentage);
            valueUpdatedCallback(calculatedValue);
        }

        public override void Loop()
        {
            ResetStartTime();
        }

        public override void PingPong()
        {
            ResetStartTime();
            var temp = start;
            start = EndValue;
            EndValue = temp;
        }
    }
}
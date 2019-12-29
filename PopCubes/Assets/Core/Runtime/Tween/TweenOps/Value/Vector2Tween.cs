using UnityEngine;
using System;
using ZigZaggle.Core.Math;

namespace ZigZaggle.Core.Tweening
{
    internal class Vector2Tween : TweenOperation
    {
        private readonly Action<Vector2> valueUpdatedCallback;
        private Vector2 start;

        public Vector2Tween(Vector2 startValue, Vector2 endValue, Action<Vector2> valueUpdatedCallback,
            float duration, float delay, bool obeyTimescale, AnimationCurve curve, Tween.TweenLoopType tweenLoop,
            Action startCallback, Action completeCallback)
        {
            SetCommonProperties(Tween.TweenType.Value, -1, duration, delay, obeyTimescale, curve, tweenLoop, startCallback,
                completeCallback);
            this.valueUpdatedCallback = valueUpdatedCallback;
            start = startValue;
            EndValue = endValue;
        }

        public Vector2 EndValue { get; private set; }

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
using UnityEngine;
using System;
using ZigZaggle.Core.Math;

namespace ZigZaggle.Core.Tweening
{
    internal class IntTween : TweenOperation
    {
        private float start;

        private readonly Action<int> valueUpdatedCallback;


        public IntTween(int startValue, int endValue, Action<int> valueUpdatedCallback, float duration, float delay,
            bool obeyTimescale, AnimationCurve curve, Tween.TweenLoopType tweenLoop, Action startCallback,
            Action completeCallback)
        {
            SetCommonProperties(Tween.TweenType.Value, -1, duration, delay, obeyTimescale, curve, tweenLoop, startCallback,
                completeCallback);

            this.valueUpdatedCallback = valueUpdatedCallback;
            start = startValue;
            EndValue = endValue;
        }

        public float EndValue { get; private set; }


        protected override bool SetStartValue()
        {
            return true;
        }

        protected override void Process(float percentage)
        {
            var calculatedValue = Interpolation.LinearInterpolate(start, EndValue, percentage);
            valueUpdatedCallback((int) calculatedValue);
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
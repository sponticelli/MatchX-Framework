using UnityEngine;
using System;
using ZigZaggle.Core.Math;


namespace ZigZaggle.Core.Tweening
{
    internal class Vector4Tween : TweenOperation
    {
        public Vector4 EndValue { get; private set; }
        
        private readonly Action<Vector4> valueUpdatedCallback;
        private Vector4 start;
        
        public Vector4Tween(Vector4 startValue, Vector4 endValue, Action<Vector4> valueUpdatedCallback, float duration,
            float delay, bool obeyTimescale, AnimationCurve curve, Tween.TweenLoopType tweenLoop, Action startCallback,
            Action completeCallback)
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
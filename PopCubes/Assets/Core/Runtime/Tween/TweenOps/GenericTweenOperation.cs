using System;
using UnityEngine;
using ZigZaggle.Core.Math;

namespace ZigZaggle.Core.Tweening
{
    public class GenericTweenOperation<T> : TweenOperation
    {
        public T EndValue { get; set; }

        private readonly Action<T> valueUpdatedCallback;
        private readonly Func< T, T, float, T> interpolation;
        private T start;

        public GenericTweenOperation(T startValue, 
            T endValue, 
            Action<T> valueUpdatedCallback, 
            float duration,
            float delay, 
            bool obeyTimescale, AnimationCurve curve, 
            Tween.TweenLoopType tweenLoop, 
            Action startCallback,
            Action completeCallback,
            Func<T,T,float, T> interpolate)
        {
            SetCommonProperties(Tween.TweenType.Value, -1, duration, delay, obeyTimescale, curve, tweenLoop, startCallback,
                completeCallback);

            this.valueUpdatedCallback = valueUpdatedCallback;
            interpolation = interpolate;
            start = startValue;
            EndValue = endValue;
        }

        protected override bool SetStartValue()
        {
            return true;
        }

        protected override void Process(float percentage)
        {
            var calculatedValue = interpolation(start, EndValue, percentage);
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
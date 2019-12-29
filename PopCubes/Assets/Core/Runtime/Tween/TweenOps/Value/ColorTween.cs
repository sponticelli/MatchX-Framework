
using UnityEngine;
using System;
using ZigZaggle.Core.Math;

namespace ZigZaggle.Core.Tweening
{
    internal class ColorTween : TweenOperation
    {
        public Color EndValue {get; private set;}

        private readonly Action<Color> valueUpdatedCallback;
        private Color start;
        
        public ColorTween (Color startValue, Color endValue, Action<Color> valueUpdatedCallback, float duration, float delay, bool obeyTimescale, AnimationCurve curve, Tween.TweenLoopType tweenLoop, Action startCallback, Action completeCallback)
        {
            SetCommonProperties (Tween.TweenType.Value, -1, duration, delay, obeyTimescale, curve, tweenLoop, startCallback, completeCallback);

            this.valueUpdatedCallback = valueUpdatedCallback;
            start = startValue;
            EndValue = endValue;
        }

        protected override bool SetStartValue ()
        {
            return true;
        }

        protected override void Process (float percentage)
        {
            var calculatedValue = Interpolation.LinearInterpolate (start, EndValue, percentage);
            valueUpdatedCallback (calculatedValue);
        }
        
        public override void Loop ()
        {
            ResetStartTime ();
        }

        public override void PingPong ()
        {
            ResetStartTime ();
            var temp = start;
            start = EndValue;
            EndValue = temp;
        }
    }
}
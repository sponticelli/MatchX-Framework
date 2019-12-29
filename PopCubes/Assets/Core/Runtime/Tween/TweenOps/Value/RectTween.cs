using System;
using UnityEngine;
using ZigZaggle.Core.Math;

namespace ZigZaggle.Core.Tweening
{
    internal class RectTween : TweenOperation
    {
        public Rect EndValue {get; private set;}

        private readonly Action<Rect> valueUpdatedCallback;
        private Rect start;
        
        public RectTween (Rect startValue, Rect endValue, Action<Rect> valueUpdatedCallback, float duration, float delay, bool obeyTimescale, AnimationCurve curve, Tween.TweenLoopType tweenLoop, Action startCallback, Action completeCallback)
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
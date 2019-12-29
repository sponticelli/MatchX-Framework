using UnityEngine;
using System;
using ZigZaggle.Core.Math;


namespace ZigZaggle.Core.Tweening
{
    internal class ShaderFloatTween : TweenOperation
    {
        public float EndValue { get; private set; }
        
        private readonly Material target;
        private float start;
        private readonly string propertyName;
        
        public ShaderFloatTween(Material target, string propertyName, float endValue, float duration, float delay,
            bool obeyTimescale, AnimationCurve curve, Tween.TweenLoopType tweenLoop, Action startCallback,
            Action completeCallback)
        {
            SetCommonProperties(Tween.TweenType.ShaderFloat, target.GetInstanceID(), duration, delay, obeyTimescale, curve,
                tweenLoop, startCallback, completeCallback);
            
            this.target = target;
            this.propertyName = propertyName;
            EndValue = endValue;
        }
        
        protected override bool SetStartValue()
        {
            start = target.GetFloat(propertyName);
            if (target == null) return false;
            return true;
        }

        protected override void Process(float percentage)
        {
            var calculatedValue = Interpolation.LinearInterpolate(start, EndValue, percentage);
            target.SetFloat(propertyName, calculatedValue);
        }


        public override void Loop()
        {
            ResetStartTime();
            target.SetFloat(propertyName, start);
        }

        public override void PingPong()
        {
            ResetStartTime();
            target.SetFloat(propertyName, EndValue);
            EndValue = start;
            start = target.GetFloat(propertyName);
        }
    }
}
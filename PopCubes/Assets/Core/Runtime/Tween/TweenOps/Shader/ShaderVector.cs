using UnityEngine;
using System;
using ZigZaggle.Core.Math;

namespace ZigZaggle.Core.Tweening
{
    internal class ShaderVector : TweenOperation
    {
        public Vector4 EndValue { get; private set; }


        private readonly Material target;
        private Vector4 start;
        private readonly string propertyName;


        public ShaderVector(Material target, string propertyName, Vector4 endValue, float duration, float delay,
            bool obeyTimescale, AnimationCurve curve, Tween.TweenLoopType tweenLoop, Action startCallback,
            Action completeCallback)
        {
            SetCommonProperties(Tween.TweenType.ShaderVector, target.GetInstanceID(), duration, delay, obeyTimescale, curve,
                tweenLoop, startCallback, completeCallback);
            
            this.target = target;
            this.propertyName = propertyName;
            EndValue = endValue;
        }
        
        protected override bool SetStartValue()
        {
            if (target == null) return false;
            start = target.GetVector(propertyName);
            return true;
        }

        protected override void Process(float percentage)
        {
            var calculatedValue = Interpolation.LinearInterpolate(start, EndValue, percentage);
            target.SetVector(propertyName, calculatedValue);
        }


        public override void Loop()
        {
            ResetStartTime();
            target.SetVector(propertyName, start);
        }

        public override void PingPong()
        {
            ResetStartTime();
            target.SetVector(propertyName, EndValue);
            EndValue = start;
            start = target.GetVector(propertyName);
        }
    }
}
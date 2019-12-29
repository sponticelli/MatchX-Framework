

using UnityEngine;
using System;
using ZigZaggle.Core.Math;

namespace ZigZaggle.Core.Tweening
{
    internal class ShaderIntTween : TweenOperation
    {
        public int EndValue { get; private set; }

        private readonly Material target;
        private int start;
        private readonly string propertyName;

        public ShaderIntTween(Material target, string propertyName, int endValue, float duration, float delay,
            bool obeyTimescale, AnimationCurve curve, Tween.TweenLoopType tweenLoop, Action startCallback,
            Action completeCallback)
        {
            SetCommonProperties(Tween.TweenType.ShaderInt, target.GetInstanceID(), duration, delay, obeyTimescale, curve,
                tweenLoop, startCallback, completeCallback);

            this.target = target;
            this.propertyName = propertyName;
            EndValue = endValue;
        }

        protected override bool SetStartValue()
        {
            start = target.GetInt(propertyName);
            return target != null;
        }

        protected override void Process(float percentage)
        {
            var calculatedValue = Interpolation.LinearInterpolate(start, EndValue, percentage);
            target.SetInt(propertyName, (int) calculatedValue);
        }


        public override void Loop()
        {
            ResetStartTime();
            target.SetInt(propertyName, start);
        }

        public override void PingPong()
        {
            ResetStartTime();
            target.SetInt(propertyName, EndValue);
            EndValue = start;
            start = target.GetInt(propertyName);
        }
    }
}
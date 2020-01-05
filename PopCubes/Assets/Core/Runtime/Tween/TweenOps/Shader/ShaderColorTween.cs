using UnityEngine;
using System;
using ZigZaggle.Core.Math;

namespace ZigZaggle.Core.Tweening
{
    internal class ShaderColorTween : TweenOperation
    {
        protected override bool interruptOnStart => true;
        public Color EndValue { get; private set; }


        private readonly Material target;
        private Color start;
        private readonly string propertyName;


        public ShaderColorTween(Material target, string propertyName, Color endValue, float duration, float delay,
            bool obeyTimescale, AnimationCurve curve, Tween.TweenLoopType tweenLoop, Action startCallback,
            Action completeCallback)
        {
            SetCommonProperties(Tween.TweenType.ShaderColor, target.GetInstanceID(), duration, delay, obeyTimescale, curve,
                tweenLoop, startCallback, completeCallback);


            this.target = target;
            this.propertyName = propertyName;
            EndValue = endValue;
        }


        protected override bool SetStartValue()
        {
            start = target.GetColor(propertyName);
            return target != null;
        }

        protected override void Process(float percentage)
        {
            var calculatedValue = Interpolation.LinearInterpolate(start, EndValue, percentage);
            target.SetColor(propertyName, calculatedValue);
        }

        public override void Loop()
        {
            ResetStartTime();
            target.SetColor(propertyName, start);
        }

        public override void PingPong()
        {
            ResetStartTime();
            target.SetColor(propertyName, EndValue);
            EndValue = start;
            start = target.GetColor(propertyName);
        }
    }
}
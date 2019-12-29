using System;
using UnityEngine;
using ZigZaggle.Core.Math;

namespace ZigZaggle.Core.Tweening
{
    internal class CanvasGroupAlphaTween : TweenOperation
    {
        private readonly CanvasGroup target;
        private float start;


        public CanvasGroupAlphaTween(CanvasGroup target, float endValue, float duration, float delay, bool obeyTimescale,
            AnimationCurve curve, Tween.TweenLoopType tweenLoop, Action startCallback, Action completeCallback)
        {
            SetCommonProperties(Tween.TweenType.CanvasGroupAlpha, target.GetInstanceID(), duration, delay, obeyTimescale,
                curve, tweenLoop, startCallback, completeCallback);

            this.target = target;
            EndValue = endValue;
        }

        public float EndValue { get; private set; }


        protected override bool SetStartValue()
        {
            if (target == null) return false;
            start = target.alpha;
            return true;
        }

        protected override void Process(float percentage)
        {
            var calculatedValue = Interpolation.LinearInterpolate(start, EndValue, percentage);
            target.alpha = calculatedValue;
        }

        public override void Loop()
        {
            ResetStartTime();
            target.alpha = start;
        }

        public override void PingPong()
        {
            ResetStartTime();
            target.alpha = EndValue;
            EndValue = start;
            start = target.alpha;
        }
    }
}
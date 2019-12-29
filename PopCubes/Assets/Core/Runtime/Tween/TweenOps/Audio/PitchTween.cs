using UnityEngine;
using System;
using ZigZaggle.Core.Math;


namespace ZigZaggle.Core.Tweening
{
    internal class PitchTween : TweenOperation
    {
        public float EndValue { get; private set; }

        private readonly AudioSource target;
        private float start;
        
        public PitchTween(AudioSource target, float endValue, float duration, float delay, bool obeyTimescale,
            AnimationCurve curve, Tween.TweenLoopType tweenLoop, Action startCallback, Action completeCallback)
        {
            SetCommonProperties(Tween.TweenType.Pitch, target.GetInstanceID(), duration, delay, obeyTimescale, curve, tweenLoop,
                startCallback, completeCallback);
            
            this.target = target;
            EndValue = endValue;
        }
        
        protected override bool SetStartValue()
        {
            if (target == null) return false;
            start = target.pitch;
            return true;
        }

        protected override void Process(float percentage)
        {
            var calculatedValue = Interpolation.LinearInterpolate(start, EndValue, percentage);
            target.pitch = calculatedValue;
        }
        
        public override void Loop()
        {
            ResetStartTime();
            target.pitch = start;
        }

        public override void PingPong()
        {
            ResetStartTime();
            target.pitch = EndValue;
            EndValue = start;
            start = target.pitch;
        }
    }
}
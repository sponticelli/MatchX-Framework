using UnityEngine;
using System;
using ZigZaggle.Core.Math;


namespace ZigZaggle.Core.Tweening
{
    internal class VolumeTween : TweenOperation
    {
        protected override bool interruptOnStart => true;
        public float EndValue { get; private set; }

        private readonly AudioSource target;
        private float start;

        public VolumeTween(AudioSource target, float endValue, float duration, float delay, bool obeyTimescale,
            AnimationCurve curve, Tween.TweenLoopType tweenLoop, Action startCallback, Action completeCallback)
        {
            SetCommonProperties(Tween.TweenType.Volume, target.GetInstanceID(), duration, delay, obeyTimescale, curve, tweenLoop,
                startCallback, completeCallback);

            this.target = target;
            EndValue = endValue;
        }
        
        protected override bool SetStartValue()
        {
            if (target == null) return false;
            start = target.volume;
            return true;
        }

        protected override void Process(float percentage)
        {
            var calculatedValue = Interpolation.LinearInterpolate(start, EndValue, percentage);
            target.volume = calculatedValue;
        }
        
        public override void Loop()
        {
            ResetStartTime();
            target.volume = start;
        }

        public override void PingPong()
        {
            ResetStartTime();
            target.volume = EndValue;
            EndValue = start;
            start = target.volume;
        }
    }
}
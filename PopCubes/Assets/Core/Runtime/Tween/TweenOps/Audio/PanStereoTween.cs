using UnityEngine;
using System;
using ZigZaggle.Core.Math;


namespace ZigZaggle.Core.Tweening
{
    internal class PanStereoTween : TweenOperation
    {
        protected override bool interruptOnStart => true;
        public float EndValue { get; private set; }

        private readonly AudioSource target;
        private float start;


        public PanStereoTween(AudioSource target, float endValue, float duration, float delay, bool obeyTimescale,
            AnimationCurve curve, Tween.TweenLoopType tweenLoop, Action startCallback, Action completeCallback)
        {
            SetCommonProperties(Tween.TweenType.PanStereo, target.GetInstanceID(), duration, delay, obeyTimescale, curve,
                tweenLoop, startCallback, completeCallback);

            this.target = target;
            EndValue = endValue;
        }


        protected override bool SetStartValue()
        {
            if (target == null) return false;
            start = target.panStereo;
            return true;
        }

        protected override void Process(float percentage)
        {
            var calculatedValue = Interpolation.LinearInterpolate(start, EndValue, percentage);
            target.panStereo = calculatedValue;
        }

        public override void Loop()
        {
            ResetStartTime();
            target.panStereo = start;
        }

        public override void PingPong()
        {
            ResetStartTime();
            target.panStereo = EndValue;
            EndValue = start;
            start = target.panStereo;
        }
    }
}
using UnityEngine;
using System;
using ZigZaggle.Core.Math;

namespace ZigZaggle.Core.Tweening
{
    internal class PositionTween : TweenOperation
    {
        protected override bool interruptOnStart => true;
        public Vector3 EndValue { get; private set; }


        private readonly Transform target;
        private Vector3 start;


        public PositionTween(Transform target, Vector3 endValue, float duration, float delay, bool obeyTimescale,
            AnimationCurve curve, Tween.TweenLoopType tweenLoop, Action startCallback, Action completeCallback)
        {
            SetCommonProperties(Tween.TweenType.Position, target.GetInstanceID(), duration, delay, obeyTimescale, curve, tweenLoop,
                startCallback, completeCallback);


            this.target = target;
            EndValue = endValue;
        }
        
        protected override bool SetStartValue()
        {
            if (target == null) return false;
            start = target.position;
            return true;
        }

        protected override void Process(float percentage)
        {
            Vector3 calculatedValue = Interpolation.LinearInterpolate(start, EndValue, percentage);
            target.position = calculatedValue;
        }


        public override void Loop()
        {
            ResetStartTime();
            target.position = start;
        }

        public override void PingPong()
        {
            ResetStartTime();
            target.position = EndValue;
            EndValue = start;
            start = target.position;
        }
    }
}
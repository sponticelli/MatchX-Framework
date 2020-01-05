using System;
using UnityEngine;

namespace ZigZaggle.Core.Tweening
{
    internal class ShakePositionTween : TweenOperation
    {
        protected override bool interruptOnStart => true;
        private readonly Transform target;
        private readonly Vector3 initialPosition;
        private readonly Vector3 intensity;

        public ShakePositionTween(Transform target, Vector3 initialPosition, Vector3 intensity, float duration, float delay,
            AnimationCurve curve, Action startCallback, Action completeCallback, Tween.TweenLoopType tweenLoop,
            bool obeyTimescale)
        {
            SetCommonProperties(Tween.TweenType.Position, target.GetInstanceID(), duration, delay, obeyTimescale, curve, tweenLoop,
                startCallback, completeCallback);

            this.target = target;
            this.initialPosition = initialPosition;
            this.intensity = intensity;
        }

        protected override bool SetStartValue()
        {
            return target != null;
        }

        protected override void Process(float percentage)
        {
            if (percentage == 0)
            {
                target.localPosition = initialPosition;
            }

            percentage = 1 - percentage;

            var amount = intensity * percentage;
            amount.x = UnityEngine.Random.Range(-amount.x, amount.x);
            amount.y = UnityEngine.Random.Range(-amount.y, amount.y);
            amount.z = UnityEngine.Random.Range(-amount.z, amount.z);

            target.localPosition = initialPosition + amount;
        }

        public override void Loop()
        {
            ResetStartTime();
            target.localPosition = initialPosition;
        }

        public override void PingPong()
        {
        }
    }
}
using UnityEngine;
using System;
using ZigZaggle.Core.Math;


namespace ZigZaggle.Core.Tweening
{
    internal class FloatTweenOperation : GenericTweenOperation<float>
    {
        public FloatTweenOperation(float startValue,
            float endValue,
            Action<float> valueUpdatedCallback,
            float duration, float delay,
            bool obeyTimescale, AnimationCurve curve,
            Tween.TweenLoopType tweenLoop,
            Action startCallback,
            Action completeCallback) :
            base(startValue, endValue, valueUpdatedCallback, duration, delay, obeyTimescale, curve, tweenLoop,
                startCallback, completeCallback, Interpolation.LinearInterpolate)
        {
        }
    }
}
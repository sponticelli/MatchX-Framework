using UnityEngine;
using System;
using ZigZaggle.Core.Math;


namespace ZigZaggle.Core.Tweening
{
    internal class Vector4Tween : GenericTweenOperation<Vector4>
    {
        public Vector4Tween(Vector4 startValue, Vector4 endValue, Action<Vector4> valueUpdatedCallback, float duration,
            float delay, bool obeyTimescale, AnimationCurve curve, Tween.TweenLoopType tweenLoop, Action startCallback,
            Action completeCallback) : base(startValue, endValue,
            valueUpdatedCallback, duration, delay, obeyTimescale, curve, tweenLoop, startCallback, completeCallback,
            Interpolation.LinearInterpolate)
        {
        }
    }
}
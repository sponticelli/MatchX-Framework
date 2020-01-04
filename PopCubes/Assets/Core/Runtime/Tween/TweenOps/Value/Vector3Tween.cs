using UnityEngine;
using System;
using ZigZaggle.Core.Math;

namespace ZigZaggle.Core.Tweening
{
    internal class Vector3Tween : GenericTweenOperation<Vector3>
    {
        public Vector3Tween(Vector3 startValue, Vector3 endValue, Action<Vector3> valueUpdatedCallback, float duration,
            float delay, bool obeyTimescale, AnimationCurve curve, Tween.TweenLoopType tweenLoop, Action startCallback,
            Action completeCallback) : base(startValue, endValue,
            valueUpdatedCallback, duration, delay, obeyTimescale, curve, tweenLoop, startCallback, completeCallback,
            Interpolation.LinearInterpolate)
        {
        }
    }
}
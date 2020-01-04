using UnityEngine;
using System;
using ZigZaggle.Core.Math;

namespace ZigZaggle.Core.Tweening
{
    internal class Vector2TweenOperation : GenericTweenOperation<Vector2>
    {
        public Vector2TweenOperation(Vector2 startValue, Vector2 endValue, Action<Vector2> valueUpdatedCallback, float duration, float delay, bool obeyTimescale, AnimationCurve curve, Tween.TweenLoopType tweenLoop, Action startCallback, Action completeCallback) :
            base(startValue, endValue, valueUpdatedCallback, duration, delay, obeyTimescale, curve, tweenLoop, startCallback, completeCallback, Interpolation.LinearInterpolate)
        {
        }
    }
}
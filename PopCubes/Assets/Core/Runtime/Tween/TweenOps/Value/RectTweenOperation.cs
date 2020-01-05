using System;
using UnityEngine;
using ZigZaggle.Core.Math;

namespace ZigZaggle.Core.Tweening
{
    internal class RectTweenOperation : GenericTweenOperation<Rect>
    {
        protected override bool interruptOnStart => true;
        public RectTweenOperation(Rect startValue, Rect endValue, Action<Rect> valueUpdatedCallback, float duration, float delay, bool obeyTimescale, AnimationCurve curve, Tween.TweenLoopType tweenLoop, Action startCallback, Action completeCallback) :
            base(startValue, endValue, valueUpdatedCallback, duration, delay, obeyTimescale, curve, tweenLoop, startCallback, completeCallback, Interpolation.LinearInterpolate)
        {
        }
    }
}


using UnityEngine;
using System;
using ZigZaggle.Core.Math;

namespace ZigZaggle.Core.Tweening
{
    internal class TextMeshColor : TweenOperation
    {
        
        public Color EndValue {get; private set;}

        
        private readonly TextMesh target;
        private Color start;

        
        public TextMeshColor (TextMesh target, Color endValue, float duration, float delay, bool obeyTimescale, AnimationCurve curve, Tween.TweenLoopType tweenLoop, Action startCallback, Action completeCallback)
        {
            
            SetCommonProperties (Tween.TweenType.TextMeshColor, target.GetInstanceID (), duration, delay, obeyTimescale, curve, tweenLoop, startCallback, completeCallback);

            
            this.target = target;
            EndValue = endValue;
        }

        
        protected override bool SetStartValue ()
        {
            if (target == null) return false;
            start = target.color;
            return true;
        }

        protected override void Process (float percentage)
        {
            Color calculatedValue = Interpolation.LinearInterpolate (start, EndValue, percentage);
            target.color = calculatedValue;
        }

        
        public override void Loop ()
        {
            ResetStartTime ();
            target.color = start;
        }

        public override void PingPong ()
        {
            ResetStartTime ();
            target.color = EndValue;
            EndValue = start;
            start = target.color;
        }
    }
}


using UnityEngine;
using System;
using UnityEngine.UI;
using ZigZaggle.Core.Math;


namespace ZigZaggle.Core.Tweening
{
    internal class TextColor : TweenOperation
    {
        protected override bool interruptOnStart => true;
        public Color EndValue {get; private set;}


        private readonly Text target;
        private Color start;

        
        public TextColor (Text target, Color endValue, float duration, float delay, bool obeyTimescale, AnimationCurve curve, Tween.TweenLoopType tweenLoop, Action startCallback, Action completeCallback)
        {
            
            SetCommonProperties (Tween.TweenType.TextColor, target.GetInstanceID (), duration, delay, obeyTimescale, curve, tweenLoop, startCallback, completeCallback);

            
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
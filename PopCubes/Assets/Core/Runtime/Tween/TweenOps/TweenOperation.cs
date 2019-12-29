using System;
using UnityEngine;
using ZigZaggle.Core.Math;

namespace ZigZaggle.Core.Tweening
{
    public abstract class TweenOperation
    {
        #region Protected Variables

        protected float elapsedTime = 0.0f;

        #endregion

        #region Public Variables

        public int targetInstanceID;
        public Tween.TweenType tweenType;

        #endregion

        #region Public Properties

        public Tween.TweenStatus Status { get; private set; }
        public float Duration { get; private set; }
        public AnimationCurve Curve { get; private set; }
        public Keyframe[] CurveKeys { get; private set; }
        public bool ObeyTimescale { get; private set; }
        public Action StartCallback { get; private set; }
        public Action CompleteCallback { get; private set; }
        public float Delay { get; private set; }
        public Tween.TweenLoopType TweenLoopType { get; private set; }
        public float Percentage { get; private set; }

        #endregion

        #region Public Methods

        public void Stop()
        {
            Status = Tween.TweenStatus.Stopped;
        }


        public void Start()
        {
            elapsedTime = 0.0f;

            if (Status != Tween.TweenStatus.Canceled && Status != Tween.TweenStatus.Finished &&
                Status != Tween.TweenStatus.Stopped) return;
            Status = Tween.TweenStatus.Running;
            Process(0);
            Tween.Instance.ExecuteTween(this);
        }

        public void Resume()
        {
            if (Status != Tween.TweenStatus.Stopped) return;

            if (Status != Tween.TweenStatus.Stopped) return;
            Status = Tween.TweenStatus.Running;
            Tween.Instance.ExecuteTween(this);
        }

        public void Rewind()
        {
            Cancel();
            Process(0);
        }

        public void Cancel()
        {
            Status = Tween.TweenStatus.Canceled;
        }

        public void Finish()
        {
            Status = Tween.TweenStatus.Finished;
        }

        public bool Tick()
        {
            switch (Status)
            {
                case Tween.TweenStatus.Stopped:
                    return false;
                case Tween.TweenStatus.Canceled:
                    Process(0);
                    Percentage = 0;
                    return false;
                case Tween.TweenStatus.Finished:
                {
                    Process(1);
                    Percentage = 1;
                    if (CompleteCallback != null) CompleteCallback();
                    return false;
                }
            }

            var progress = 0.0f;

            if (ObeyTimescale)
            {
                elapsedTime += Time.deltaTime;
            }
            else
            {
                elapsedTime += Time.unscaledDeltaTime;
            }

            progress = System.Math.Max(elapsedTime, 0f);

            var percentage = Mathf.Min(progress / Duration, 1);

            if (percentage == 0 && Status != Tween.TweenStatus.Delayed) Status = Tween.TweenStatus.Delayed;

            if (percentage > 0 && Status == Tween.TweenStatus.Delayed)
            {
                Tween.Stop(targetInstanceID, tweenType);
                if (SetStartValue())
                {
                    if (StartCallback != null) StartCallback();
                    Status = Tween.TweenStatus.Running;
                }
                else
                {
                    return false;
                }
            }

            var curveValue = percentage;
            if (Curve != null && CurveKeys.Length > 0) curveValue = Easing.EvaluateCurve(Curve, percentage);

            if (Status == Tween.TweenStatus.Running)
            {
                try
                {
                    Process(curveValue);
                    Percentage = curveValue;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

            if (percentage != 1) return true;
            CompleteCallback?.Invoke();
            switch (TweenLoopType)
            {
                case Tween.TweenLoopType.Loop:
                    Loop();
                    break;

                case Tween.TweenLoopType.PingPong:
                    PingPong();
                    break;

                default:
                    Status = Tween.TweenStatus.Finished;
                    return false;
            }

            //tell system we are still have work to do:
            return true;
        }

        #endregion

        #region Protected Methods

        protected void ResetStartTime()
        {
            elapsedTime = -Delay;
        }

        protected void SetCommonProperties(Tween.TweenType tweenType, int targetInstanceId, float duration, float delay,
            bool obeyTimeScale, AnimationCurve curve, Tween.TweenLoopType tweenLoop, Action startCallback,
            Action completeCallback)
        {
            this.tweenType = tweenType;
            targetInstanceID = targetInstanceId;

            if (delay > 0)
                Status = Tween.TweenStatus.Delayed;

            Duration = duration;
            Delay = delay;
            Curve = curve;

            CurveKeys = curve?.keys;
            StartCallback = startCallback;
            CompleteCallback = completeCallback;
            TweenLoopType = tweenLoop;
            ObeyTimescale = obeyTimeScale;

            ResetStartTime();
        }

        #endregion

        #region Abstract Methods

        protected abstract bool SetStartValue();

        protected abstract void Process(float percentage);

        public abstract void Loop();

        public abstract void PingPong();

        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using ZigZaggle.Core.Math;

namespace ZigZaggle.Core.Tweening
{
    public class Tween
    {
        public enum TweenLoopType
        {
            None,
            Loop,
            PingPong
        };

        public enum TweenStatus
        {
            Delayed,
            Running,
            Canceled,
            Stopped,
            Finished
        }

        public enum TweenType
        {
            Position,
            Rotation,
            LocalScale,
            LightColor,
            LightIntensity,
            LightRange,
            FieldOfView,
            SpriteRendererColor,
            RawImageColor,
            ImageColor,
            AnchoredPosition,
            Size,
            Volume,
            Pitch,
            PanStereo,
            ShaderFloat,
            ShaderColor,
            ShaderInt,
            ShaderVector,
            Value,
            TextMeshColor,
            GUITextColor,
            TextColor,
            CanvasGroupAlpha
        };

        public static readonly List<TweenOperation> ActiveTweens = new List<TweenOperation>();

        #region Private Variables

        private static TweenManager _instance;
        

        #endregion

        public static TweenManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new GameObject("TweenManager",
                        typeof(TweenManager)).GetComponent<TweenManager>();
                GameObject.DontDestroyOnLoad(_instance.gameObject);
                return _instance;
            }
        }

        #region Ease Curves

        
        #endregion

        #region Public Methods:
        public static TweenOperation Shake(Transform target, Vector3 initialPosition, Vector3 intensity, float duration,
            float delay, TweenLoopType tweenLoop = TweenLoopType.None, Action startCallback = null, Action completeCallback = null,
            bool obeyTimescale = true)
        {
            var tween = new ShakePositionTween(target, initialPosition, intensity, duration, delay, Easing.EaseLinear,
                startCallback, completeCallback, tweenLoop, obeyTimescale);
            SendTweenForProcessing(tween, true);
            return tween;
        }

        public static TweenOperation CanvasGroupAlpha(CanvasGroup target, float endValue, float duration, float delay,
            AnimationCurve easeCurve = null, TweenLoopType tweenLoop = TweenLoopType.None, Action startCallback = null,
            Action completeCallback = null, bool obeyTimescale = true)
        {
            var tween = new CanvasGroupAlphaTween(target, endValue, duration, delay, obeyTimescale, easeCurve,
                tweenLoop, startCallback, completeCallback);
            SendTweenForProcessing(tween, true);
            return tween;
        }
        
        public static TweenOperation CanvasGroupAlpha(CanvasGroup target, float startValue, float endValue, float duration,
            float delay, AnimationCurve easeCurve = null, TweenLoopType tweenLoop = TweenLoopType.None, Action startCallback = null,
            Action completeCallback = null, bool obeyTimescale = true)
        {
            target.alpha = startValue;
            return CanvasGroupAlpha(target, endValue, duration, delay, easeCurve, tweenLoop, startCallback, completeCallback,
                obeyTimescale);
        }
        
        public static TweenOperation Value(Rect startValue, Rect endValue, Action<Rect> valueUpdatedCallback, float duration,
            float delay, AnimationCurve easeCurve = null, TweenLoopType tweenLoop = TweenLoopType.None, Action startCallback = null,
            Action completeCallback = null, bool obeyTimescale = true)
        {
            var tween = new RectTween(startValue, endValue, valueUpdatedCallback, duration, delay, obeyTimescale,
                easeCurve, tweenLoop, startCallback, completeCallback);
            SendTweenForProcessing(tween, true);
            return tween;
        }
        
        public static TweenOperation Value(Vector4 startValue, Vector4 endValue, Action<Vector4> valueUpdatedCallback,
            float duration, float delay, AnimationCurve easeCurve = null, TweenLoopType tweenLoop = TweenLoopType.None,
            Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            var tween = new Vector4Tween(startValue, endValue, valueUpdatedCallback, duration, delay,
                obeyTimescale, easeCurve, tweenLoop, startCallback, completeCallback);
            SendTweenForProcessing(tween);
            return tween;
        }
        
        public static TweenOperation Value(Vector3 startValue, Vector3 endValue, Action<Vector3> valueUpdatedCallback,
            float duration, float delay, AnimationCurve easeCurve = null, TweenLoopType tweenLoop = TweenLoopType.None,
            Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            var tween = new Vector3Tween(startValue, endValue, valueUpdatedCallback, duration, delay,
                obeyTimescale, easeCurve, tweenLoop, startCallback, completeCallback);
            SendTweenForProcessing(tween);
            return tween;
        }
        
        public static TweenOperation Value(Vector2 startValue, Vector2 endValue, Action<Vector2> valueUpdatedCallback,
            float duration, float delay, AnimationCurve easeCurve = null, TweenLoopType tweenLoop = TweenLoopType.None,
            Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            var tween = new Vector2Tween(startValue, endValue, valueUpdatedCallback, duration, delay,
                obeyTimescale, easeCurve, tweenLoop, startCallback, completeCallback);
            SendTweenForProcessing(tween);
            return tween;
        }
        
        public static TweenOperation Value(Color startValue, Color endValue, Action<Color> valueUpdatedCallback,
            float duration, float delay, AnimationCurve easeCurve = null, TweenLoopType tweenLoop = TweenLoopType.None,
            Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            var tween = new ColorTween(startValue, endValue, valueUpdatedCallback, duration, delay,
                obeyTimescale, easeCurve, tweenLoop, startCallback, completeCallback);
            SendTweenForProcessing(tween);
            return tween;
        }
        
        public static TweenOperation Value(int startValue, int endValue, Action<int> valueUpdatedCallback, float duration,
            float delay, AnimationCurve easeCurve = null, TweenLoopType tweenLoop = TweenLoopType.None, Action startCallback = null,
            Action completeCallback = null, bool obeyTimescale = true)
        {
            var tween = new IntTween(startValue, endValue, valueUpdatedCallback, duration, delay, obeyTimescale,
                easeCurve, tweenLoop, startCallback, completeCallback);
            SendTweenForProcessing(tween);
            return tween;
        }
        
        public static TweenOperation Value(float startValue, float endValue, Action<float> valueUpdatedCallback,
            float duration, float delay, AnimationCurve easeCurve = null, TweenLoopType tweenLoop = TweenLoopType.None,
            Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            var tween = new FloatTween(startValue, endValue, valueUpdatedCallback, duration, delay,
                obeyTimescale, easeCurve, tweenLoop, startCallback, completeCallback);
            SendTweenForProcessing(tween);
            return tween;
        }
        
        public static TweenOperation ShaderVector(Material target, string propertyName, Vector4 endValue, float duration,
            float delay, AnimationCurve easeCurve = null, TweenLoopType tweenLoop = TweenLoopType.None, Action startCallback = null,
            Action completeCallback = null, bool obeyTimescale = true)
        {
            ShaderVector tween = new ShaderVector(target, propertyName, endValue, duration, delay, obeyTimescale,
                easeCurve, tweenLoop, startCallback, completeCallback);
            SendTweenForProcessing(tween, true);
            return tween;
        }
        
        public static TweenOperation ShaderVector(Material target, string propertyName, Vector4 startValue, Vector4 endValue,
            float duration, float delay, AnimationCurve easeCurve = null, TweenLoopType tweenLoop = TweenLoopType.None,
            Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            target.SetVector(propertyName, startValue);
            return ShaderVector(target, propertyName, endValue, duration, delay, easeCurve, tweenLoop, startCallback,
                completeCallback, obeyTimescale);
        }
        
        public static TweenOperation ShaderInt(Material target, string propertyName, int endValue, float duration,
            float delay, AnimationCurve easeCurve = null, TweenLoopType tweenLoop = TweenLoopType.None, Action startCallback = null,
            Action completeCallback = null, bool obeyTimescale = true)
        {
            ShaderIntTween tween = new ShaderIntTween(target, propertyName, endValue, duration, delay, obeyTimescale, easeCurve,
                tweenLoop, startCallback, completeCallback);
            SendTweenForProcessing(tween, true);
            return tween;
        }
        
        public static TweenOperation ShaderInt(Material target, string propertyName, int startValue, int endValue,
            float duration, float delay, AnimationCurve easeCurve = null, TweenLoopType tweenLoop = TweenLoopType.None,
            Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            target.SetInt(propertyName, startValue);
            return ShaderInt(target, propertyName, endValue, duration, delay, easeCurve, tweenLoop, startCallback,
                completeCallback, obeyTimescale);
        }
        
        public static TweenOperation ShaderColor(Material target, string propertyName, Color endValue, float duration,
            float delay, AnimationCurve easeCurve = null, TweenLoopType tweenLoop = TweenLoopType.None, Action startCallback = null,
            Action completeCallback = null, bool obeyTimescale = true)
        {
            ShaderColorTween tween = new ShaderColorTween(target, propertyName, endValue, duration, delay, obeyTimescale,
                easeCurve, tweenLoop, startCallback, completeCallback);
            SendTweenForProcessing(tween, true);
            return tween;
        }
        
        public static TweenOperation ShaderColor(Material target, string propertyName, Color startValue, Color endValue,
            float duration, float delay, AnimationCurve easeCurve = null, TweenLoopType tweenLoop = TweenLoopType.None,
            Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            target.SetColor(propertyName, startValue);
            return ShaderColor(target, propertyName, endValue, duration, delay, easeCurve, tweenLoop, startCallback,
                completeCallback, obeyTimescale);
        }
        
        public static TweenOperation ShaderFloat(Material target, string propertyName, float endValue, float duration,
            float delay, AnimationCurve easeCurve = null, TweenLoopType tweenLoop = TweenLoopType.None, Action startCallback = null,
            Action completeCallback = null, bool obeyTimescale = true)
        {
            ShaderFloatTween tween = new ShaderFloatTween(target, propertyName, endValue, duration, delay, obeyTimescale,
                easeCurve, tweenLoop, startCallback, completeCallback);
            SendTweenForProcessing(tween, true);
            return tween;
        }
        
        public static TweenOperation ShaderFloat(Material target, string propertyName, float startValue, float endValue,
            float duration, float delay, AnimationCurve easeCurve = null, TweenLoopType tweenLoop = TweenLoopType.None,
            Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            target.SetFloat(propertyName, startValue);
            return ShaderFloat(target, propertyName, endValue, duration, delay, easeCurve, tweenLoop, startCallback,
                completeCallback, obeyTimescale);
        }
        
        public static TweenOperation Pitch(AudioSource target, float endValue, float duration, float delay,
            AnimationCurve easeCurve = null, TweenLoopType tweenLoop = TweenLoopType.None, Action startCallback = null,
            Action completeCallback = null, bool obeyTimescale = true)
        {
            PitchTween tween = new PitchTween(target, endValue, duration, delay, obeyTimescale, easeCurve, tweenLoop, startCallback,
                completeCallback);
            SendTweenForProcessing(tween, true);
            return tween;
        }
        
        public static TweenOperation Pitch(AudioSource target, float startValue, float endValue, float duration, float delay,
            AnimationCurve easeCurve = null, TweenLoopType tweenLoop = TweenLoopType.None, Action startCallback = null,
            Action completeCallback = null, bool obeyTimescale = true)
        {
            target.pitch = startValue;
            return Pitch(target, endValue, duration, delay, easeCurve, tweenLoop, startCallback, completeCallback,
                obeyTimescale);
        }
        
        public static TweenOperation PanStereo(AudioSource target, float endValue, float duration, float delay,
            AnimationCurve easeCurve = null, TweenLoopType tweenLoop = TweenLoopType.None, Action startCallback = null,
            Action completeCallback = null, bool obeyTimescale = true)
        {
            PanStereoTween tween = new PanStereoTween(target, endValue, duration, delay, obeyTimescale, easeCurve, tweenLoop,
                startCallback, completeCallback);
            SendTweenForProcessing(tween, true);
            return tween;
        }
        
        public static TweenOperation PanStereo(AudioSource target, float startValue, float endValue, float duration,
            float delay, AnimationCurve easeCurve = null, TweenLoopType tweenLoop = TweenLoopType.None, Action startCallback = null,
            Action completeCallback = null, bool obeyTimescale = true)
        {
            target.panStereo = startValue;
            return PanStereo(target, endValue, duration, delay, easeCurve, tweenLoop, startCallback, completeCallback,
                obeyTimescale);
        }
        
        public static TweenOperation Volume(AudioSource target, float endValue, float duration, float delay,
            AnimationCurve easeCurve = null, TweenLoopType tweenLoop = TweenLoopType.None, Action startCallback = null,
            Action completeCallback = null, bool obeyTimescale = true)
        {
            VolumeTween tween = new VolumeTween(target, endValue, duration, delay, obeyTimescale, easeCurve, tweenLoop, startCallback,
                completeCallback);
            SendTweenForProcessing(tween, true);
            return tween;
        }
        
        public static TweenOperation Volume(AudioSource target, float startValue, float endValue, float duration,
            float delay, AnimationCurve easeCurve = null, TweenLoopType tweenLoop = TweenLoopType.None, Action startCallback = null,
            Action completeCallback = null, bool obeyTimescale = true)
        {
            target.volume = startValue;
            return Volume(target, endValue, duration, delay, easeCurve, tweenLoop, startCallback, completeCallback,
                obeyTimescale);
        }
        
        public static TweenOperation Size(RectTransform target, Vector2 endValue, float duration, float delay,
            AnimationCurve easeCurve = null, TweenLoopType tweenLoop = TweenLoopType.None, Action startCallback = null,
            Action completeCallback = null, bool obeyTimescale = true)
        {
            SizeTween tween = new SizeTween(target, endValue, duration, delay, obeyTimescale, easeCurve, tweenLoop, startCallback,
                completeCallback);
            SendTweenForProcessing(tween, true);
            return tween;
        }
        
        public static TweenOperation Size(RectTransform target, Vector2 startValue, Vector2 endValue, float duration,
            float delay, AnimationCurve easeCurve = null, TweenLoopType tweenLoop = TweenLoopType.None, Action startCallback = null,
            Action completeCallback = null, bool obeyTimescale = true)
        {
            target.sizeDelta = startValue;
            return Size(target, endValue, duration, delay, easeCurve, tweenLoop, startCallback, completeCallback,
                obeyTimescale);
        }
        
        public static TweenOperation FieldOfView(Camera target, float endValue, float duration, float delay,
            AnimationCurve easeCurve = null, TweenLoopType tweenLoop = TweenLoopType.None, Action startCallback = null,
            Action completeCallback = null, bool obeyTimescale = true)
        {
            FieldOfViewTween tween = new FieldOfViewTween(target, endValue, duration, delay, obeyTimescale, easeCurve, tweenLoop,
                startCallback, completeCallback);
            SendTweenForProcessing(tween, true);
            return tween;
        }
        
        public static TweenOperation FieldOfView(Camera target, float startValue, float endValue, float duration,
            float delay, AnimationCurve easeCurve = null, TweenLoopType tweenLoop = TweenLoopType.None, Action startCallback = null,
            Action completeCallback = null, bool obeyTimescale = true)
        {
            target.fieldOfView = startValue;
            return FieldOfView(target, endValue, duration, delay, easeCurve, tweenLoop, startCallback, completeCallback,
                obeyTimescale);
        }
        
        public static TweenOperation LightRange(Light target, float endValue, float duration, float delay,
            AnimationCurve easeCurve = null, TweenLoopType tweenLoop = TweenLoopType.None, Action startCallback = null,
            Action completeCallback = null, bool obeyTimescale = true)
        {
            LightRangeTween tween = new LightRangeTween(target, endValue, duration, delay, obeyTimescale, easeCurve, tweenLoop,
                startCallback, completeCallback);
            SendTweenForProcessing(tween, true);
            return tween;
        }
        
        public static TweenOperation LightRange(Light target, float startValue, float endValue, float duration, float delay,
            AnimationCurve easeCurve = null, TweenLoopType tweenLoop = TweenLoopType.None, Action startCallback = null,
            Action completeCallback = null, bool obeyTimescale = true)
        {
            target.range = startValue;
            return LightRange(target, endValue, duration, delay, easeCurve, tweenLoop, startCallback, completeCallback,
                obeyTimescale);
        }
        
        public static TweenOperation LightIntensity(Light target, float endValue, float duration, float delay,
            AnimationCurve easeCurve = null, TweenLoopType tweenLoop = TweenLoopType.None, Action startCallback = null,
            Action completeCallback = null, bool obeyTimescale = true)
        {
            LightIntensityTween tween = new LightIntensityTween(target, endValue, duration, delay, obeyTimescale, easeCurve, tweenLoop,
                startCallback, completeCallback);
            SendTweenForProcessing(tween, true);
            return tween;
        }
        
        public static TweenOperation LightIntensity(Light target, float startValue, float endValue, float duration,
            float delay, AnimationCurve easeCurve = null, TweenLoopType tweenLoop = TweenLoopType.None, Action startCallback = null,
            Action completeCallback = null, bool obeyTimescale = true)
        {
            target.intensity = startValue;
            return LightIntensity(target, endValue, duration, delay, easeCurve, tweenLoop, startCallback, completeCallback,
                obeyTimescale);
        }
        
        public static TweenOperation LocalScale(Transform target, Vector3 endValue, float duration, float delay,
            AnimationCurve easeCurve = null, TweenLoopType tweenLoop = TweenLoopType.None, Action startCallback = null,
            Action completeCallback = null, bool obeyTimescale = true)
        {
            LocalScaleTween tween = new LocalScaleTween(target, endValue, duration, delay, obeyTimescale, easeCurve, tweenLoop,
                startCallback, completeCallback);
            SendTweenForProcessing(tween, true);
            return tween;
        }
        
        public static TweenOperation LocalScale(Transform target, Vector3 startValue, Vector3 endValue, float duration,
            float delay, AnimationCurve easeCurve = null, TweenLoopType tweenLoop = TweenLoopType.None, Action startCallback = null,
            Action completeCallback = null, bool obeyTimescale = true)
        {
            target.localScale = startValue;
            return LocalScale(target, endValue, duration, delay, easeCurve, tweenLoop, startCallback, completeCallback,
                obeyTimescale);
        }
        
        public static TweenOperation Color(RawImage target, Color endValue, float duration, float delay,
            AnimationCurve easeCurve = null, TweenLoopType tweenLoop = TweenLoopType.None, Action startCallback = null,
            Action completeCallback = null, bool obeyTimescale = true)
        {
            RawImageColorTween tween = new RawImageColorTween(target, endValue, duration, delay, obeyTimescale, easeCurve, tweenLoop,
                startCallback, completeCallback);
            SendTweenForProcessing(tween, true);
            return tween;
        }
        
        public static TweenOperation Color(RawImage target, Color startValue, Color endValue, float duration, float delay,
            AnimationCurve easeCurve = null, TweenLoopType tweenLoop = TweenLoopType.None, Action startCallback = null,
            Action completeCallback = null, bool obeyTimescale = true)
        {
            target.color = startValue;
            return Color(target, endValue, duration, delay, easeCurve, tweenLoop, startCallback, completeCallback,
                obeyTimescale);
        }
        
        public static TweenOperation Color(Image target, Color endValue, float duration, float delay,
            AnimationCurve easeCurve = null, TweenLoopType tweenLoop = TweenLoopType.None, Action startCallback = null,
            Action completeCallback = null, bool obeyTimescale = true)
        {
            ImageColorTween tween = new ImageColorTween(target, endValue, duration, delay, obeyTimescale, easeCurve, tweenLoop,
                startCallback, completeCallback);
            SendTweenForProcessing(tween, true);
            return tween;
        }
        
        public static TweenOperation Color(Image target, Color startValue, Color endValue, float duration, float delay,
            AnimationCurve easeCurve = null, TweenLoopType tweenLoop = TweenLoopType.None, Action startCallback = null,
            Action completeCallback = null, bool obeyTimescale = true)
        {
            target.color = startValue;
            return Color(target, endValue, duration, delay, easeCurve, tweenLoop, startCallback, completeCallback,
                obeyTimescale);
        }
        
        public static TweenOperation Color(Text target, Color endValue, float duration, float delay,
            AnimationCurve easeCurve = null, TweenLoopType tweenLoop = TweenLoopType.None, Action startCallback = null,
            Action completeCallback = null, bool obeyTimescale = true)
        {
            TextColor tween = new TextColor(target, endValue, duration, delay, obeyTimescale, easeCurve, tweenLoop,
                startCallback, completeCallback);
            SendTweenForProcessing(tween, true);
            return tween;
        }
        
        public static TweenOperation Color(Text target, Color startValue, Color endValue, float duration, float delay,
            AnimationCurve easeCurve = null, TweenLoopType tweenLoop = TweenLoopType.None, Action startCallback = null,
            Action completeCallback = null, bool obeyTimescale = true)
        {
            target.color = startValue;
            return Color(target, endValue, duration, delay, easeCurve, tweenLoop, startCallback, completeCallback,
                obeyTimescale);
        }
        
        public static TweenOperation Color(Light target, Color endValue, float duration, float delay,
            AnimationCurve easeCurve = null, TweenLoopType tweenLoop = TweenLoopType.None, Action startCallback = null,
            Action completeCallback = null, bool obeyTimescale = true)
        {
            LightColorTween tween = new LightColorTween(target, endValue, duration, delay, obeyTimescale, easeCurve, tweenLoop,
                startCallback, completeCallback);
            SendTweenForProcessing(tween, true);
            return tween;
        }
        
        public static TweenOperation Color(Light target, Color startValue, Color endValue, float duration, float delay,
            AnimationCurve easeCurve = null, TweenLoopType tweenLoop = TweenLoopType.None, Action startCallback = null,
            Action completeCallback = null, bool obeyTimescale = true)
        {
            target.color = startValue;
            return Color(target, endValue, duration, delay, easeCurve, tweenLoop, startCallback, completeCallback,
                obeyTimescale);
        }
        
        public static TweenOperation Color(TextMesh target, Color endValue, float duration, float delay,
            AnimationCurve easeCurve = null, TweenLoopType tweenLoop = TweenLoopType.None, Action startCallback = null,
            Action completeCallback = null, bool obeyTimescale = true)
        {
            TextMeshColor tween = new TextMeshColor(target, endValue, duration, delay, obeyTimescale, easeCurve, tweenLoop,
                startCallback, completeCallback);
            SendTweenForProcessing(tween, true);
            return tween;
        }
        
        public static TweenOperation Color(TextMesh target, Color startValue, Color endValue, float duration, float delay,
            AnimationCurve easeCurve = null, TweenLoopType tweenLoop = TweenLoopType.None, Action startCallback = null,
            Action completeCallback = null, bool obeyTimescale = true)
        {
            target.color = startValue;
            return Color(target, endValue, duration, delay, easeCurve, tweenLoop, startCallback, completeCallback,
                obeyTimescale);
        }
        
        public static TweenOperation Color(Material target, Color endValue, float duration, float delay,
            AnimationCurve easeCurve = null, TweenLoopType tweenLoop = TweenLoopType.None, Action startCallback = null,
            Action completeCallback = null, bool obeyTimescale = true)
        {
            ShaderColorTween tween = new ShaderColorTween(target, "_Color", endValue, duration, delay, obeyTimescale, easeCurve,
                tweenLoop, startCallback, completeCallback);
            SendTweenForProcessing(tween, true);
            return tween;
        }
        
        public static TweenOperation Color(Material target, Color startColor, Color endValue, float duration, float delay,
            AnimationCurve easeCurve = null, TweenLoopType tweenLoop = TweenLoopType.None, Action startCallback = null,
            Action completeCallback = null, bool obeyTimescale = true)
        {
            target.color = startColor;
            return Color(target, endValue, duration, delay, easeCurve, tweenLoop, startCallback, completeCallback,
                obeyTimescale);
        }
        
        public static TweenOperation Color(Renderer target, Color endValue, float duration, float delay,
            AnimationCurve easeCurve = null, TweenLoopType tweenLoop = TweenLoopType.None, Action startCallback = null,
            Action completeCallback = null, bool obeyTimescale = true)
        {
            var material = target.material;
            return Color(material, endValue, duration, delay, easeCurve, tweenLoop, startCallback, completeCallback,
                obeyTimescale);
        }
        
        public static TweenOperation Color(Renderer target, Color startColor, Color endValue, float duration, float delay,
            AnimationCurve easeCurve = null, TweenLoopType tweenLoop = TweenLoopType.None, Action startCallback = null,
            Action completeCallback = null, bool obeyTimescale = true)
        {
            var material = target.material;
            return Color(material, startColor, endValue, duration, delay, easeCurve, tweenLoop, startCallback,
                completeCallback, obeyTimescale);
        }
        
        public static TweenOperation Color(SpriteRenderer target, Color endValue, float duration, float delay,
            AnimationCurve easeCurve = null, TweenLoopType tweenLoop = TweenLoopType.None, Action startCallback = null,
            Action completeCallback = null, bool obeyTimescale = true)
        {
            SpriteRendererColorTween tween = new SpriteRendererColorTween(target, endValue, duration, delay, obeyTimescale,
                easeCurve, tweenLoop, startCallback, completeCallback);
            SendTweenForProcessing(tween, true);
            return tween;
        }
        
        public static TweenOperation Color(SpriteRenderer target, Color startColor, Color endValue, float duration,
            float delay, AnimationCurve easeCurve = null, TweenLoopType tweenLoop = TweenLoopType.None, Action startCallback = null,
            Action completeCallback = null, bool obeyTimescale = true)
        {
            target.color = startColor;
            return Color(target, endValue, duration, delay, easeCurve, tweenLoop, startCallback, completeCallback,
                obeyTimescale);
        }
        
        public static TweenOperation Color(Camera target, Color endValue, float duration, float delay,
            AnimationCurve easeCurve = null, TweenLoopType tweenLoop = TweenLoopType.None, Action startCallback = null,
            Action completeCallback = null, bool obeyTimescale = true)
        {
            CameraBackgroundColorTween tween = new CameraBackgroundColorTween(target, endValue, duration, delay, obeyTimescale,
                easeCurve, tweenLoop, startCallback, completeCallback);
            SendTweenForProcessing(tween, true);
            return tween;
        }
        
        public static TweenOperation Color(Camera target, Color startColor, Color endValue, float duration, float delay,
            AnimationCurve easeCurve = null, TweenLoopType tweenLoop = TweenLoopType.None, Action startCallback = null,
            Action completeCallback = null, bool obeyTimescale = true)
        {
            target.backgroundColor = startColor;
            return Color(target, endValue, duration, delay, easeCurve, tweenLoop, startCallback, completeCallback,
                obeyTimescale);
        }
        
        public static TweenOperation Position(Transform target, Vector3 endValue, float duration, float delay,
            AnimationCurve easeCurve = null, TweenLoopType tweenLoop = TweenLoopType.None, Action startCallback = null,
            Action completeCallback = null, bool obeyTimescale = true)
        {
            PositionTween tween = new PositionTween(target, endValue, duration, delay, obeyTimescale, easeCurve, tweenLoop,
                startCallback, completeCallback);
            SendTweenForProcessing(tween, true);
            return tween;
        }
        
        public static TweenOperation Position(Transform target, Vector3 startValue, Vector3 endValue, float duration,
            float delay, AnimationCurve easeCurve = null, TweenLoopType tweenLoop = TweenLoopType.None, Action startCallback = null,
            Action completeCallback = null, bool obeyTimescale = true)
        {
            target.position = startValue;
            return Position(target, endValue, duration, delay, easeCurve, tweenLoop, startCallback, completeCallback,
                obeyTimescale);
        }
        
        public static TweenOperation AnchoredPosition(RectTransform target, Vector2 endValue, float duration, float delay,
            AnimationCurve easeCurve = null, TweenLoopType tweenLoop = TweenLoopType.None, Action startCallback = null,
            Action completeCallback = null, bool obeyTimescale = true)
        {
            AnchoredPositionTween tween = new AnchoredPositionTween(target, endValue, duration, delay, obeyTimescale, easeCurve,
                tweenLoop, startCallback, completeCallback);
            SendTweenForProcessing(tween, true);
            return tween;
        }
        
        public static TweenOperation AnchoredPosition(RectTransform target, Vector2 startValue, Vector2 endValue,
            float duration, float delay, AnimationCurve easeCurve = null, TweenLoopType tweenLoop = TweenLoopType.None,
            Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            target.anchoredPosition = startValue;
            return AnchoredPosition(target, endValue, duration, delay, easeCurve, tweenLoop, startCallback, completeCallback,
                obeyTimescale);
        }
        
        public static TweenOperation LocalPosition(Transform target, Vector3 endValue, float duration, float delay,
            AnimationCurve easeCurve = null, TweenLoopType tweenLoop = TweenLoopType.None, Action startCallback = null,
            Action completeCallback = null, bool obeyTimescale = true)
        {
            LocalPositionTween tween = new LocalPositionTween(target, endValue, duration, delay, obeyTimescale, easeCurve, tweenLoop,
                startCallback, completeCallback);
            SendTweenForProcessing(tween, true);
            return tween;
        }
        
        public static TweenOperation LocalPosition(Transform target, Vector3 startValue, Vector3 endValue, float duration,
            float delay, AnimationCurve easeCurve = null, TweenLoopType tweenLoop = TweenLoopType.None, Action startCallback = null,
            Action completeCallback = null, bool obeyTimescale = true)
        {
            target.localPosition = startValue;
            return LocalPosition(target, endValue, duration, delay, easeCurve, tweenLoop, startCallback, completeCallback,
                obeyTimescale);
        }
        
        public static TweenOperation Rotate(Transform target, Vector3 amount, Space space, float duration, float delay,
            AnimationCurve easeCurve = null, TweenLoopType tweenLoop = TweenLoopType.None, Action startCallback = null,
            Action completeCallback = null, bool obeyTimescale = true)
        {
            RotateTween tween = new RotateTween(target, amount, space, duration, delay, obeyTimescale, easeCurve, tweenLoop,
                startCallback, completeCallback);
            SendTweenForProcessing(tween, true);
            return tween;
        }
        
        public static TweenOperation Rotation(Transform target, Vector3 endValue, float duration, float delay,
            AnimationCurve easeCurve = null, TweenLoopType tweenLoop = TweenLoopType.None, Action startCallback = null,
            Action completeCallback = null, bool obeyTimescale = true)
        {
            endValue = Quaternion.Euler(endValue).eulerAngles;
            RotationTween tween = new RotationTween(target, endValue, duration, delay, obeyTimescale, easeCurve, tweenLoop,
                startCallback, completeCallback);
            SendTweenForProcessing(tween, true);
            return tween;
        }
        
        public static TweenOperation Rotation(Transform target, Vector3 startValue, Vector3 endValue, float duration,
            float delay, AnimationCurve easeCurve = null, TweenLoopType tweenLoop = TweenLoopType.None, Action startCallback = null,
            Action completeCallback = null, bool obeyTimescale = true)
        {
            startValue = Quaternion.Euler(startValue).eulerAngles;
            endValue = Quaternion.Euler(endValue).eulerAngles;
            target.rotation = Quaternion.Euler(startValue);
            return Rotation(target, endValue, duration, delay, easeCurve, tweenLoop, startCallback, completeCallback,
                obeyTimescale);
        }
        
        public static TweenOperation Rotation(Transform target, Quaternion endValue, float duration, float delay,
            AnimationCurve easeCurve = null, TweenLoopType tweenLoop = TweenLoopType.None, Action startCallback = null,
            Action completeCallback = null, bool obeyTimescale = true)
        {
            RotationTween tween = new RotationTween(target, endValue.eulerAngles, duration, delay, obeyTimescale, easeCurve, tweenLoop,
                startCallback, completeCallback);
            SendTweenForProcessing(tween, true);
            return tween;
        }
        
        public static TweenOperation Rotation(Transform target, Quaternion startValue, Quaternion endValue, float duration,
            float delay, AnimationCurve easeCurve = null, TweenLoopType tweenLoop = TweenLoopType.None, Action startCallback = null,
            Action completeCallback = null, bool obeyTimescale = true)
        {
            target.rotation = startValue;
            return Rotation(target, endValue, duration, delay, easeCurve, tweenLoop, startCallback, completeCallback,
                obeyTimescale);
        }

        public static TweenOperation LocalRotation(Transform target, Vector3 endValue, float duration, float delay,
            AnimationCurve easeCurve = null, TweenLoopType tweenLoop = TweenLoopType.None, Action startCallback = null,
            Action completeCallback = null, bool obeyTimescale = true)
        {
            endValue = Quaternion.Euler(endValue).eulerAngles;
            LocalRotationTween tween = new LocalRotationTween(target, endValue, duration, delay, obeyTimescale, easeCurve, tweenLoop,
                startCallback, completeCallback);
            SendTweenForProcessing(tween, true);
            return tween;
        }
        
        public static TweenOperation LocalRotation(Transform target, Vector3 startValue, Vector3 endValue, float duration,
            float delay, AnimationCurve easeCurve = null, TweenLoopType tweenLoop = TweenLoopType.None, Action startCallback = null,
            Action completeCallback = null, bool obeyTimescale = true)
        {
            startValue = Quaternion.Euler(startValue).eulerAngles;
            endValue = Quaternion.Euler(endValue).eulerAngles;
            target.localRotation = Quaternion.Euler(startValue);
            return LocalRotation(target, endValue, duration, delay, easeCurve, tweenLoop, startCallback, completeCallback,
                obeyTimescale);
        }
        
        public static TweenOperation LocalRotation(Transform target, Quaternion endValue, float duration, float delay,
            AnimationCurve easeCurve = null, TweenLoopType tweenLoop = TweenLoopType.None, Action startCallback = null,
            Action completeCallback = null, bool obeyTimescale = true)
        {
            LocalRotationTween tween = new LocalRotationTween(target, endValue.eulerAngles, duration, delay, obeyTimescale,
                easeCurve, tweenLoop, startCallback, completeCallback);
            SendTweenForProcessing(tween, true);
            return tween;
        }
        
        public static TweenOperation LocalRotation(Transform target, Quaternion startValue, Quaternion endValue,
            float duration, float delay, AnimationCurve easeCurve = null, TweenLoopType tweenLoop = TweenLoopType.None,
            Action startCallback = null, Action completeCallback = null, bool obeyTimescale = true)
        {
            target.localRotation = startValue;
            return LocalRotation(target, endValue, duration, delay, easeCurve, tweenLoop, startCallback, completeCallback,
                obeyTimescale);
        }
        
        public static TweenOperation LookAt(Transform target, Transform targetToLookAt, Vector3 up, float duration,
            float delay, AnimationCurve easeCurve = null, TweenLoopType tweenLoop = TweenLoopType.None, Action startCallback = null,
            Action completeCallback = null, bool obeyTimescale = true)
        {
            var direction = targetToLookAt.position - target.position;
            var rotation = Quaternion.LookRotation(direction, up);
            return Rotation(target, rotation, duration, delay, easeCurve, tweenLoop, startCallback, completeCallback,
                obeyTimescale);
        }
        
        public static TweenOperation LookAt(Transform target, Vector3 positionToLookAt, Vector3 up, float duration,
            float delay, AnimationCurve easeCurve = null, TweenLoopType tweenLoop = TweenLoopType.None, Action startCallback = null,
            Action completeCallback = null, bool obeyTimescale = true)
        {
            var direction = positionToLookAt - target.position;
            var rotation = Quaternion.LookRotation(direction, up);
            return Rotation(target, rotation, duration, delay, easeCurve, tweenLoop, startCallback, completeCallback,
                obeyTimescale);
        }
        
        #endregion

        #region utilities:
        
        public static void Stop(int targetInstanceID, TweenType tweenType)
        {
            if (targetInstanceID == -1) return;
            for (var i = 0; i < ActiveTweens.Count; i++)
            {
                if (ActiveTweens[i].targetInstanceID == targetInstanceID && ActiveTweens[i].tweenType == tweenType &&
                    ActiveTweens[i].Status != TweenStatus.Delayed)
                {
                    ActiveTweens[i].Stop();
                }
            }
        }
        
        public static void Stop(int targetInstanceID)
        {
            StopInstanceTarget(targetInstanceID);
        }
        
        public static void StopAll()
        {
            foreach (var item in ActiveTweens)
            {
                item.Stop();
            }
        }
        
        public static void FinishAll()
        {
            foreach (var item in ActiveTweens)
            {
                item.Finish();
            }
        }
        
        public static void Finish(int targetInstanceID)
        {
            FinishInstanceTarget(targetInstanceID);
        }
        
        public static void Cancel(int targetInstanceID)
        {
            CancelInstanceTarget(targetInstanceID);
        }
        
        public static void CancelAll()
        {
            foreach (var item in ActiveTweens)
            {
                item.Cancel();
            }
        }
        
        #endregion

        #region Private Methods

        private static void StopInstanceTarget(int id)
        {
            foreach (var t in ActiveTweens.Where(t => t.targetInstanceID == id))
            {
                t.Stop();
            }
        }

        private static void StopInstanceTargetType(int id, TweenType type)
        {
            foreach (var t in ActiveTweens.Where(t => t.targetInstanceID == id && t.tweenType == type))
            {
                t.Stop();
            }
        }

        private static void FinishInstanceTarget(int id)
        {
            foreach (var t in ActiveTweens.Where(t => t.targetInstanceID == id))
            {
                t.Finish();
            }
        }

        private static void CancelInstanceTarget(int id)
        {
            foreach (var t in ActiveTweens.Where(t => t.targetInstanceID == id))
            {
                t.Cancel();
            }
        }

        private static void SendTweenForProcessing(TweenOperation tween, bool interrupt = false)
        {
            if (!Application.isPlaying)
            {
                //Tween can not be called in edit mode!
                return;
            }

            if (interrupt && tween.Delay == 0)
            {
                StopInstanceTargetType(tween.targetInstanceID, tween.tweenType);
            }

            Instance.ExecuteTween(tween);
        }
        
        #endregion
    }
}
using UnityEngine;

namespace ZigZaggle.Core.Math
{
    public static class Easing
    {
        
        private static AnimationCurve _easeIn;
        private static AnimationCurve _easeInStrong;
        private static AnimationCurve _easeOut;
        private static AnimationCurve _easeOutStrong;
        private static AnimationCurve _easeInOut;
        private static AnimationCurve _easeInOutStrong;
        private static AnimationCurve _easeInBack;
        private static AnimationCurve _easeOutBack;
        private static AnimationCurve _easeInOutBack;
        private static AnimationCurve _easeSpring;
        private static AnimationCurve _easeBounce;
        private static AnimationCurve _easeWobble;
        
        public static AnimationCurve EaseLinear => null;

        public static AnimationCurve EaseIn =>
            _easeIn ?? (_easeIn = new AnimationCurve(new Keyframe(0, 0, 0, 0), new Keyframe(1, 1, 1, 0)));

        public static AnimationCurve EaseInStrong =>
            _easeInStrong ?? (_easeInStrong = new AnimationCurve(new Keyframe(0, 0, .03f, .03f),
                new Keyframe(0.45f, 0.03f, 0.2333333f, 0.2333333f),
                new Keyframe(0.7f, 0.13f, 0.7666667f, 0.7666667f),
                new Keyframe(0.85f, 0.3f, 2.233334f, 2.233334f),
                new Keyframe(0.925f, 0.55f, 4.666665f, 4.666665f),
                new Keyframe(1, 1, 5.999996f, 5.999996f)));

        public static AnimationCurve EaseOut =>
            _easeOut ?? (_easeOut = new AnimationCurve(new Keyframe(0, 0, 0, 1), new Keyframe(1, 1, 0, 0)));

        public static AnimationCurve EaseOutStrong =>
            _easeOutStrong ?? (_easeOutStrong = new AnimationCurve(new Keyframe(0, 0, 13.80198f, 13.80198f),
                new Keyframe(0.04670785f, 0.3973127f, 5.873408f, 5.873408f),
                new Keyframe(0.1421811f, 0.7066917f, 2.313627f, 2.313627f),
                new Keyframe(0.2483539f, 0.8539293f, 0.9141542f, 0.9141542f),
                new Keyframe(0.4751028f, 0.954047f, 0.264541f, 0.264541f), new Keyframe(1, 1, .03f, .03f)));

        public static AnimationCurve EaseInOut => _easeInOut ?? (_easeInOut = AnimationCurve.EaseInOut(0, 0, 1, 1));

        public static AnimationCurve EaseInOutStrong =>
            _easeInOutStrong ?? (_easeInOutStrong = new AnimationCurve(new Keyframe(0, 0, 0.03f, 0.03f),
                new Keyframe(0.5f, 0.5f, 3.257158f, 3.257158f), new Keyframe(1, 1, .03f, .03f)));

        public static AnimationCurve EaseInBack =>
            _easeInBack ?? (_easeInBack =
                new AnimationCurve(new Keyframe(0, 0, -1.1095f, -1.1095f), new Keyframe(1, 1, 2, 2)));

        public static AnimationCurve EaseOutBack =>
            _easeOutBack ?? (_easeOutBack =
                new AnimationCurve(new Keyframe(0, 0, 2, 2), new Keyframe(1, 1, -1.1095f, -1.1095f)));

        public static AnimationCurve EaseInOutBack =>
            _easeInOutBack ?? (_easeInOutBack = new AnimationCurve(
                new Keyframe(1, 1, -1.754543f, -1.754543f),
                new Keyframe(0, 0, -1.754543f, -1.754543f)));

        public static AnimationCurve EaseSpring =>
            _easeSpring ?? (_easeSpring = new AnimationCurve(
                new Keyframe(0f, -0.0003805831f, 8.990726f, 8.990726f),
                new Keyframe(0.35f, 1f, -4.303913f, -4.303913f),
                new Keyframe(0.55f, 1f, 1.554695f, 1.554695f),
                new Keyframe(0.7730452f, 1f, -2.007816f, -2.007816f),
                new Keyframe(1f, 1f, -1.23451f, -1.23451f)));

        public static AnimationCurve EaseBounce =>
            _easeBounce ?? (_easeBounce = new AnimationCurve(new Keyframe(0, 0, 0, 0),
                new Keyframe(0.25f, 1, 11.73749f, -5.336508f),
                new Keyframe(0.4f, 0.6f, -0.1904764f, -0.1904764f),
                new Keyframe(0.575f, 1, 5.074602f, -3.89f),
                new Keyframe(0.7f, 0.75f, 0.001192093f, 0.001192093f),
                new Keyframe(0.825f, 1, 4.18469f, -2.657566f), new Keyframe(0.895f, 0.9f, 0, 0),
                new Keyframe(0.95f, 1, 3.196362f, -2.028364f), new Keyframe(1, 1, 2.258884f, 0.5f)));

        public static AnimationCurve EaseWobble =>
            _easeWobble ?? (_easeWobble = new AnimationCurve(new Keyframe(0f, 0f, 11.01978f, 30.76278f),
                new Keyframe(0.08054394f, 1f, 0f, 0f), new Keyframe(0.3153235f, -0.75f, 0f, 0f),
                new Keyframe(0.5614113f, 0.5f, 0f, 0f), new Keyframe(0.75f, -0.25f, 0f, 0f),
                new Keyframe(0.9086903f, 0.1361611f, 0f, 0f), new Keyframe(1f, 0f, -4.159244f, -1.351373f)));

        public static float EvaluateCurve (AnimationCurve curve, float percentage)
        {
            return curve.Evaluate ((curve [curve.length - 1].time) * percentage);
        }
    }
}
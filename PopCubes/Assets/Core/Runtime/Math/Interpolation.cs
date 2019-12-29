﻿using UnityEngine;

namespace ZigZaggle.Core.Math
{
    public static class Interpolation
    {
        public static float LinearInterpolate(float from, float to, float percentage)
        {
            return (to - from) * percentage + from;
        }

        public static Vector2 LinearInterpolate(Vector2 from, Vector2 to, float percentage)
        {
            return new Vector2(LinearInterpolate(from.x, to.x, percentage),
                LinearInterpolate(from.y, to.y, percentage));
        }

        public static Vector3 LinearInterpolate(Vector3 from, Vector3 to, float percentage)
        {
            return new Vector3(LinearInterpolate(from.x, to.x, percentage), LinearInterpolate(from.y, to.y, percentage),
                LinearInterpolate(from.z, to.z, percentage));
        }

        public static Vector3 LinearInterpolateRotational(Vector3 from, Vector3 to, float percentage)
        {
            return new Vector3(CylindricalLerp(from.x, to.x, percentage), CylindricalLerp(from.y, to.y, percentage),
                CylindricalLerp(from.z, to.z, percentage));
        }

        public static Vector4 LinearInterpolate(Vector4 from, Vector4 to, float percentage)
        {
            return new Vector4(LinearInterpolate(from.x, to.x, percentage), LinearInterpolate(from.y, to.y, percentage),
                LinearInterpolate(from.z, to.z, percentage), LinearInterpolate(from.w, to.w, percentage));
        }

        public static Rect LinearInterpolate(Rect from, Rect to, float percentage)
        {
            return new Rect(LinearInterpolate(from.x, to.x, percentage), LinearInterpolate(from.y, to.y, percentage),
                LinearInterpolate(from.width, to.width, percentage),
                LinearInterpolate(from.height, to.height, percentage));
        }

        public static Color LinearInterpolate(Color from, Color to, float percentage)
        {
            return new Color(LinearInterpolate(from.r, to.r, percentage), LinearInterpolate(from.g, to.g, percentage),
                LinearInterpolate(from.b, to.b, percentage), LinearInterpolate(from.a, to.a, percentage));
        }

        public static float CylindricalLerp(float from, float to, float percentage)
        {
            const float min = 0f;
            const float max = 360f;
            var half = Mathf.Abs((max - min) * .5f);
            float calculation;
            float difference;

            if ((to - from) < -half)
            {
                difference = ((max - from) + to) * percentage;
                calculation = from + difference;
            }
            else if ((to - from) > half)
            {
                difference = -((max - to) + from) * percentage;
                calculation = from + difference;
            }
            else
            {
                calculation = from + (to - from) * percentage;
            }

            return calculation;
        }
    }
}
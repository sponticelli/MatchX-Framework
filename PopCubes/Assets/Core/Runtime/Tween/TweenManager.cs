using System.Collections;
using UnityEngine;

namespace ZigZaggle.Core.Tweening
{
    public class TweenManager : MonoBehaviour
    {
        public void ExecuteTween(TweenOperation tween)
        {
            StartCoroutine(RunTween(tween));
        }

        private static IEnumerator RunTween(TweenOperation tween)
        {
            Tween.ActiveTweens.Add(tween);
            while (true)
            {
                if (!tween.Tick())
                {
                    Tween.ActiveTweens.Remove(tween);
                    yield break;
                }

                yield return null;
            }
        }
    }
}
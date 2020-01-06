using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using ZigZaggle.Core.Tweening;

namespace ZigZaggle.Core.UI
{
    public class AnimatedCounter : MonoBehaviour
    {
        private int currentValue = int.MinValue;
        private int previousValue = int.MinValue;
        [SerializeField] private float seconds = 0.25f;
        [SerializeField] private TMP_Text text;
        private TweenOperation tween;

        public int CurrentValue
        {
            get => currentValue;
            set
            {
                previousValue = currentValue;
                currentValue = value;
                UpdateCounter();
            }
        }

        private void UpdateCounter()
        {
            if (previousValue == int.MinValue)
            {
                text.text = currentValue.ToString();
                return;
            }

            if (tween != null && tween.Status == Tween.TweenStatus.Running)
            {
                tween.Then(Tween.Value(previousValue, currentValue, UpdateText, seconds, 0));
            }
            else
            {
                tween = Tween.Value(previousValue, currentValue, UpdateText, seconds, 0);
                tween.Start();
            }
        }

        private void UpdateText(int val)
        {
            text.text = val.ToString();
        }

        private void Awake()
        {
            if (text == null)
            {
                text = GetComponent<TMP_Text>();
            }

            text.text = "0";
        }
    }
}
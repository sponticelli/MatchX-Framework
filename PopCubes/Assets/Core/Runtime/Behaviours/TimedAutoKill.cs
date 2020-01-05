using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZigZaggle.Core.Behaviours
{
    public class TimedAutoKill : MonoBehaviour
    {
        [SerializeField] private float duration = 1.0f;

        private float accTime;
        private void OnEnable()
        {
            accTime = 0;
        }

        private void Update()
        {
            accTime += Time.deltaTime;
            if (accTime >= duration)
            {
                Destroy(gameObject);
            }
        }
    }
}

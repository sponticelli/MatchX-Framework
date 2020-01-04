using UnityEngine;
using ZigZaggle.Core.Math;
using ZigZaggle.Core.Tweening;

namespace ZigZaggle.Collapse.Components
{
    public class ColoredBlock : BaseBlock
    {
        private TweenOperation movement;
        public override void Explode()
        {
            base.Explode();
            Tween.LocalScale(transform, Vector3.zero, 0.25f, 0, 
                easeCurve: Easing.EaseInOutStrong,
                completeCallback: () =>
            {
                Destroy(this.gameObject);
            });
            
        }

        public override void Move(Vector3 newPosition, float seconds)
        {
            if (movement != null && movement.Status == Tween.TweenStatus.Running)
            {
                movement = Tween.Position(transform, newPosition, seconds, movement.Duration);
            }
            else
            {
                movement = Tween.Position(transform, newPosition, seconds, 0);
            }
        }
    }
}
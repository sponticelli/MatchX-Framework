using UnityEngine;
using ZigZaggle.Core.Math;
using ZigZaggle.Core.Tweening;

namespace ZigZaggle.Collapse.Components
{
    public class ColoredBlock : BaseBlock
    {
        [SerializeField] private GameObject explosionPrefab;
        private TweenOperation movement;
        public override void Explode()
        {
            base.Explode();
            if (explosionPrefab != null)
            {
                var explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            }
            Tween.LocalScale(transform, Vector3.zero, 0.25f, 0, 
                easeCurve: Easing.EaseInOutStrong,
                completeCallback: () =>
            {
                Destroy(gameObject);
            }).Start();
            
        }

        public override void Move(Vector3 newPosition, float seconds)
        {
            if (movement != null && movement.Status == Tween.TweenStatus.Running)
            {
                movement.Then(Tween.Position(transform, newPosition, seconds, 0));
            }
            else
            {
                movement = Tween.Position(transform, newPosition, seconds, 0);
                movement.Start();
            }
        }
    }
}
using UnityEngine;

namespace ZigZaggle.Collapse.Components
{
    public class ColoredBlock : BaseBlock
    {
        public override void Explode()
        {
            base.Explode();
            Destroy(this.gameObject);
        }

        public override void Move(Vector3 newPosition, float seconds)
        {
            transform.position = newPosition;
        }
    }
}
using UnityEngine;
using UnityEngine.Events;

namespace ZigZaggle.Collapse.Components
{
    public class BaseBlock : MonoBehaviour
    {
        public UnityEvent onExplode;
        public Vector2Int Position { get; set; }

        public virtual void Explode()
        {
            onExplode.Invoke();
        }

        public virtual void Move(Vector3 newPosition, float seconds)
        {
            
        }
    }
}
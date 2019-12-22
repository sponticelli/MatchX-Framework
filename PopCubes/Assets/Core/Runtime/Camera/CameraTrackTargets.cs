using System.Linq;
using UnityEngine;
using ZigZaggle.UnityExtensions;

namespace ZigZaggle.Core.Cameras
{
    public class CameraTrackTargets : MonoBehaviour
    {
        public GameObject[] targets;
        [SerializeField] 
        private float boundingBoxPadding = 2f;

        [Header("Runtime Update Params")] 
        [SerializeField]
        private bool realTimeUpdate;

        [SerializeField] 
        private float zoomSpeed = 20f;

        private UnityEngine.Camera camera;

        public void SetTargets(GameObject[] transformTargets)
        {
            targets = transformTargets;
        }

        private void Awake()
        {
            camera = gameObject.GetComponent<Camera>();
            camera.orthographic = true;
        }

        public void UpdateCamera()
        {
            if (targets.Length == 0) return;
            var boundingBox = CalculateTargetsBoundingBox();
            transform.position = CalculateCameraPosition(boundingBox);
            camera.orthographicSize = CalculateOrthographicSize(boundingBox);
        }

        void LateUpdate()
        {
            if (!realTimeUpdate) return;
            var boundingBox = CalculateTargetsBoundingBox();
            transform.position = CalculateCameraPosition(boundingBox);
            camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, CalculateOrthographicSize(boundingBox),
                Time.deltaTime * zoomSpeed);
        }

        private Rect CalculateTargetsBoundingBox()
        {
            var minX = Mathf.Infinity;
            var maxX = Mathf.NegativeInfinity;
            var minY = Mathf.Infinity;
            var maxY = Mathf.NegativeInfinity;

            foreach (var target in targets)
            {
                var position = target.transform.position;

                minX = Mathf.Min(minX, position.x);
                minY = Mathf.Min(minY, position.y);
                maxX = Mathf.Max(maxX, position.x);
                maxY = Mathf.Max(maxY, position.y);
            }

            return Rect.MinMaxRect(minX - boundingBoxPadding,
                maxY + boundingBoxPadding,
                maxX + boundingBoxPadding,
                minY - boundingBoxPadding);
        }

        private Vector3 CalculateCameraPosition(Rect boundingBox)
        {
            var boundingBoxCenter = boundingBox.center;
            return new Vector3(boundingBoxCenter.x, boundingBoxCenter.y, -10f);
        }

        float CalculateOrthographicSize(Rect boundingBox)
        {
            float orthographicSize;
            var topRight = new Vector3(boundingBox.x + boundingBox.width, boundingBox.y, 0f);
            var (x, y, _) = camera.WorldToViewportPoint(topRight);

            if (x >= y)
                orthographicSize = Mathf.Abs(boundingBox.width) / camera.aspect / 2f;
            else
                orthographicSize = Mathf.Abs(boundingBox.height) / 2f;

            return orthographicSize;
        }
    }
}
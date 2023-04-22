using UnityEngine;
using Zenject;

namespace SkiFree2.Cursor
{
    public class CursorTracker : MonoBehaviour
    {
        private Camera _camera;
        private float _camOffsetZ;

        private static Transform _skierTransform;

        public static Vector3 Direction { get; private set; }

        [Inject]
        public void Construct(Skier skier)
        {
            _skierTransform = skier.transform;
        }

        private void Awake()
        {
            _camera = Camera.main;
            _camOffsetZ = _camera.transform.position.z;
        }

        private void FixedUpdate()
        {
            var cursorPos = _camera.ScreenToWorldPoint(Input.mousePosition - Vector3.one * _camOffsetZ);

            Direction = Vector3.Normalize(cursorPos - _skierTransform.position);
        }
    }
}

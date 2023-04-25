using UnityEngine;

namespace Helper
{
    public class Boundary : MonoBehaviour
    {
        public static float Left { get; private set; }
        public static float Right { get; private set; }
        public static float Top { get; private set; }
        public static float Bottom { get; private set; }

        private void Awake()
        {
            Setup();
        }

        private static void Setup()
        {
            var cam = Camera.main;
            var borderHeightHalf = cam.orthographicSize;
            var borderWidthHalf = cam.orthographicSize * cam.aspect;
            Left = -borderWidthHalf;
            Right = borderWidthHalf;
            Top = borderHeightHalf - cam.transform.position.y;
            Bottom = -borderHeightHalf + cam.transform.position.y;
        }
    }
}

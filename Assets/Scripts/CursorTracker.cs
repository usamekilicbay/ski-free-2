using UnityEngine;

public class CursorTracker : MonoBehaviour
{
    private Camera _camera;
    private float _camOffsetZ;

    private static Transform _skierTransform;

    public static Vector3 Direction { get; private set; }

    private void Awake()
    {
        _skierTransform = GameObject.Find("Player").transform;

        _camera = Camera.main;
        _camOffsetZ = _camera.transform.position.z;
    }

    private void FixedUpdate()
    {
        var cursorPos = _camera.ScreenToWorldPoint(Input.mousePosition - Vector3.one * _camOffsetZ);

        Direction = Vector3.Normalize(cursorPos - _skierTransform.position);
    }
}

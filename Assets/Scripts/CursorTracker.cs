using UnityEngine;

public class CursorTracker : MonoBehaviour
{
    [Range(1f, 50f)]
    [SerializeField] private float maxSpeed;
    [Range(0.01f, 1f)]
    [SerializeField] private float acceleration;
    [Range(0.1f, 3f)]
    [SerializeField] private float defaultSpeed;

    private Camera _camera;
    private float _camOffsetZ;
    private float _speed;

    [SerializeField]
    private Gremlin _gremlin;
    private bool _isChased;

    public static Vector3 Velocity;
    public static Vector3 Direction;

    private static float _TimeCounter;

    public const float GlitchTimerLimit = 3f;
    public static float GlitchTimer;

    private void Awake()
    {
        _gremlin = FindAnyObjectByType<Gremlin>();   

        _camera = Camera.main;

        _camOffsetZ = _camera.transform.position.z;

        _speed = defaultSpeed;
    }

    void FixedUpdate()
    {
        var cursorPos = _camera.ScreenToWorldPoint(Input.mousePosition - Vector3.one * _camOffsetZ);

        if (GlitchTimer > 0f)
        {
            GlitchTimer -= Time.deltaTime;
            return;
        }

        _TimeCounter += Time.deltaTime;

        if (cursorPos.y >= transform.position.y)
        {
            Velocity = Vector3.zero;
            _speed = defaultSpeed;
            return;
        }

        if (_speed < maxSpeed)
            _speed += acceleration * Time.deltaTime;
        else
            _speed = maxSpeed;

        //speedText.SetText(_speed.ToString("F1"));

        Direction = Vector3.Normalize(cursorPos - transform.position);
        Velocity = -Direction * _speed;
    }

    private void LateUpdate()
    {
        if (!_isChased && _TimeCounter > 3f)
        {
            _isChased = true;
            _gremlin.Chase();
        }
    }
}

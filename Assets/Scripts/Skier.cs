using TMPro;
using UnityEngine;

public class Skier : MonoBehaviour
{
    [Range(1f, 50f)]
    [SerializeField] private float maxSpeed;
    [Range(0.01f, 1f)]
    [SerializeField] private float acceleration;
    [Range(0.1f, 3f)]
    [SerializeField] private float defaultSpeed;

    [SerializeField] private TextMeshProUGUI speedText;

    private Transform skierArtTransform;
    private Rigidbody2D _rb;
    private Camera _camera;
    private float _camOffsetZ;
    private float _speed;

    private const float _GlitchTimerLimit = 3f;
    private static float _GlitchTimer;

    [SerializeField]
    private Gremlin _gremlin;
    private bool _isChased;

    private void Awake()
    {
        _gremlin = GameObject.Find("Gremlin").GetComponent<Gremlin>();

        _rb = GetComponent<Rigidbody2D>();

        skierArtTransform = transform.GetChild(0);
        _camera = Camera.main;

        _camOffsetZ = _camera.transform.position.z;

        _speed = defaultSpeed;
    }

    void FixedUpdate()
    {
        var cursorPos = _camera.ScreenToWorldPoint(Input.mousePosition - Vector3.one * _camOffsetZ);

        if (_GlitchTimer > 0f)
        {
            _GlitchTimer -= Time.deltaTime;
            return;
        }

        if (cursorPos.y >= transform.position.y)
        {
            _rb.velocity = Vector3.zero;
            _speed = defaultSpeed;
            return;
        }

        if (_speed < maxSpeed)
            _speed += acceleration * Time.deltaTime;
        else
            _speed = maxSpeed;

        speedText.SetText(_speed.ToString("F1"));

        var dir = Vector3.Normalize(cursorPos - transform.position);
        var velocity = dir * _speed;

        _rb.velocity = velocity;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.AngleAxis(angle + 90f, Vector3.forward);

        skierArtTransform.rotation = rotation;
    }

    private void LateUpdate()
    {
        if (!_isChased && transform.position.y <= -15f)
        {
            _isChased = true;
            _gremlin.Chase(_rb);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var layer = collision.gameObject.layer;

        switch (layer)
        {
            case 7:
                _rb.velocity = Vector3.zero;
                _GlitchTimer = _GlitchTimerLimit;
                break;
            case 9:
                // TODO: Game Over
                break;
        }
    }
}

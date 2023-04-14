using UnityEngine;

public class Gremlin : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Rigidbody2D _rb;

    private static Transform _PlayerTransform;

    private void Awake()
    {
        _PlayerTransform = GameObject.Find("Player").transform;
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        var dir = Vector3.Normalize(_PlayerTransform.position - transform.position);

        var velocity = dir * _speed;

        _rb.velocity = velocity + CursorTracker.Velocity;
    }

    public void Chase()
    {
        var spawnPos = _PlayerTransform.position + new Vector3(-5f, 2f);
        transform.position = spawnPos;

        gameObject.SetActive(true);

        //transform.position = new Vector2
        //{
        //    x = playerRb.position.x - 5f,
        //    y = playerRb.position.y + 2f
        //};

        //var dir = Vector3.Normalize(playerRb.position - _rb.position);

        //var velocity = dir * _speed;

        //_rb.velocity = velocity;
    }
}

using UnityEngine;

public class Gremlin : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Rigidbody2D _rb;
    private Rigidbody2D _playerRb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        var dir = Vector3.Normalize(_playerRb.position - _rb.position);

        var velocity = dir * _speed;

        _rb.velocity = velocity;
    }

    public void Chase(Rigidbody2D playerRb)
    {
        _playerRb = playerRb;
        
        transform.position = new Vector2
        {
            x = _playerRb.position.x - 5f,
            y = _playerRb.position.y + 2f
        };

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

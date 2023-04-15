using UnityEngine;

public enum GremlinBehaviourState
{
    ReadyToChase,
    Chase,
    Disappear,
}

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
        if (Skier.IsDead)
            gameObject.SetActive(false);

        var dir = Vector3.Normalize(_PlayerTransform.position - transform.position);

        var velocity = dir * _speed;

        _rb.velocity = velocity + WorldDriver.Velocity;
    }

    public void Chase()
    {
        var rangeX = Random.Range(-5f, 5f);
        var spawnPos = _PlayerTransform.position + new Vector3(rangeX, 4f);
        transform.position = spawnPos;

        gameObject.SetActive(true);
    }
}

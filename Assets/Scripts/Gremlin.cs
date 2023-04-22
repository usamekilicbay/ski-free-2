using SkiFree2.Abstracts;
using UnityEngine;
using Zenject;

namespace SkiFree2
{
    public class Gremlin : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private Rigidbody2D _rb;
        private BoxCollider2D _boxCollider;
        private SpriteRenderer _spriteRenderer;

        public bool isChasing { get; private set; }

        private static Transform _skierTransform;


        private Skier _skier;

        [Inject]
        public void Construct(Skier skier)
        {
            _skier = skier;
            _skierTransform = _skier.transform;
        }

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _boxCollider = GetComponent<BoxCollider2D>();
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        private void Start()
        {
            Vanish();
        }

        private void OnEnable()
        {
            _skier.OnDeath += Feast;
        }

        private void OnDisable()
        {
            _skier.OnDeath -= Feast;
        }

        private void FixedUpdate()
        {
            if (!isChasing)
                return;

            var dir = Vector3.Normalize(_skierTransform.position - transform.position);
            var velocity = dir * _speed;
            _rb.velocity = velocity + WorldDriver.Velocity;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.TryGetComponent(out IKillable killable))
                killable.Kill();
        }

        public void Chase()
        {
            var rangeX = Random.Range(-5f, 5f);
            var spawnPos = _skierTransform.position + new Vector3(rangeX, 4f);
            transform.position = spawnPos;

            Appear();
        }

        public void StopChasing()
        {
            Vanish();
        }

        public void Feast()
        {
            StopChasing();
            //Play Anim
        }

        public void ResetGame()
        {
            Vanish();
        }

        private void Appear()
        {
            isChasing = true;
            _boxCollider.enabled = true;
            _spriteRenderer.enabled = true;
        }

        private void Vanish()
        {
            isChasing = false;
            _boxCollider.enabled = false;
            _spriteRenderer.enabled = false;
        }
    }
}

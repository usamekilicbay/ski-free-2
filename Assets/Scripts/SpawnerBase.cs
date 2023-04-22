using SkiFree2;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace SkiFree2.Spawn
{
    public abstract class SpawnerBase : MonoBehaviour
    {
        [Range(1f, 50f)]
        [SerializeField] private int spawnDensity;
        [Range(0f, 10f)]
        [SerializeField] private float spawnRate;
        [Space(10)]
        [SerializeField] private GameObject prefab;
        [SerializeField] private Transform spawnTransform;

        private List<Rigidbody2D> _spawnedObjects;
        private float _spawnTimer;
        private bool _isActive;
        private int _index = 0;

        private static Transform _skierTransform;

        [Inject]
        public void Construct(Skier skier)
        {
            _skierTransform = skier.transform;
        }

        private void Awake()
        {
            Generate();
        }

        private void FixedUpdate()
        {
            if (!_isActive)
            {
                _spawnedObjects.ForEach(x => x.velocity = Vector2.zero);
                return;
            }

            _spawnedObjects.ForEach(x => x.velocity = WorldDriver.Velocity);

            if (_spawnTimer > 0f)
            {
                _spawnTimer -= Time.deltaTime;
                return;
            }

            if (_index == spawnDensity)
                _index = 0;

            var spawnedObject = _spawnedObjects[_index];
            var spawnPos = GetRandomSpawnPos();

            spawnedObject.transform.position = spawnPos;

            _index++;

            _spawnTimer = spawnRate;
        }

        private void Generate()
        {
            _spawnedObjects = new List<Rigidbody2D>();

            for (var i = 0; i < spawnDensity; i++)
            {
                var obstacle = Instantiate(prefab, spawnTransform);
                _spawnedObjects.Add(obstacle.GetComponent<Rigidbody2D>());
            }
        }

        private Vector3 GetRandomSpawnPos()
        {
            var rangeX = Random.Range(-10f, 10f);
            var rangeY = Random.Range(-20f, -50f);
            var spawnPos = _skierTransform.position + new Vector3(rangeX, rangeY);

            return spawnPos;
        }

        public void ResetGame()
        {
            _isActive = false;

            foreach (var spawnItem in _spawnedObjects)
            {
                spawnItem.gameObject.SetActive(false);
                spawnItem.transform.position = GetRandomSpawnPos();
            }
        }

        public void StartGame()
        {
            _isActive = true;

            foreach (var spawnItem in _spawnedObjects)
                spawnItem.gameObject.SetActive(true);
        }

        public void StopGame()
        {
            _isActive = false;
        }
    }
}

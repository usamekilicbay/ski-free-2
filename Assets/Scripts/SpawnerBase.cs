using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnerBase : MonoBehaviour
{
    [Range(1f, 50f)]
    [SerializeField] private int spawnDensity;
    [Range(0f, 10f)]
    [SerializeField] private float spawnTimeRate;

    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform spawnTransform;

    private float _spawnTimer;
    private List<Rigidbody2D> _spawnedObjects;
    private int _index = 0;

    private static Transform _playerTransform;

    private void Awake()
    {
        _playerTransform = GameObject.Find("Player").transform;
        _spawnedObjects = new List<Rigidbody2D>();

        for (var i = 0; i < spawnDensity; i++)
        {
            var obstacle = Instantiate(prefab, spawnTransform);
            _spawnedObjects.Add(obstacle.GetComponent<Rigidbody2D>());
        }
    }

    private void FixedUpdate()
    {
        if (CursorTracker.Direction.y >= 0f)
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
        var playerPos = _playerTransform.position;
        var rangeX = Random.Range(-10f, 10f);
        var rangeY = Random.Range(-20f, -50f);
        var spawnPos = playerPos + new Vector3(rangeX, rangeY);

        spawnedObject.transform.position = spawnPos;

        _index++;

        _spawnTimer = spawnTimeRate;
    }
}

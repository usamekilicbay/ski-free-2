using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Range(1f, 50f)]
    [SerializeField] private int spawnDensity;
    [Range(0f, 10f)]
    [SerializeField] private float spawnTimeRate;

    [SerializeField] GameObject obstaclePrefab;

    private Transform _playerTransform;

    private static float spawnTimer;

    private static List<Rigidbody2D> obstacles;

    private static int index = 0;

    private void Awake()
    {
        _playerTransform = GameObject.Find("Player").transform;
        obstacles = new List<Rigidbody2D>();

        for (var i = 0; i < spawnDensity; i++)
        {
            var obstacle = Instantiate(obstaclePrefab, transform);
            obstacles.Add(obstacle.GetComponent<Rigidbody2D>());
        }
    }

    private void FixedUpdate()
    {
        obstacles.ForEach(x => x.velocity = CursorTracker.Velocity);

        if (spawnTimer > 0f)
        {
            spawnTimer -= Time.deltaTime;
            return;
        }

        if (index == spawnDensity)
            index = 0;

        var obstacle = obstacles[index];
        var playerPos = _playerTransform.transform.position;
        var rangeX = Random.Range(-10f, 10f);
        var rangeY = Random.Range(-20f, -50f);
        var spawnPos = playerPos + new Vector3(rangeX, rangeY);

        obstacle.transform.position = spawnPos;

        index++;

        spawnTimer = spawnTimeRate;
    }
}

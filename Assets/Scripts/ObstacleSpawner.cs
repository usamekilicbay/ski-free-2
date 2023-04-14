using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] GameObject obstaclePrefab;

    void Start()
    {
        for (int i = 0; i < 3000; i++)
        {
            var obstacle = Instantiate(obstaclePrefab);
            obstacle.transform.position = new Vector2
            {
                x = Random.Range(-100f, 100f),
                y = Random.Range(10f, -1000f)
            };
        }
    }
}


//using System.Collections.Generic;
//using UnityEditor;
//using UnityEngine;
//using UnityEngine.UIElements;

//public class ObstacleSpawner : MonoBehaviour
//{
//    [Range(1f, 50f)]
//    [SerializeField] private int spawnDensity;
//    [Range(0f, 10f)]
//    [SerializeField] private float spawnTimeRate;

//    [SerializeField] GameObject obstaclePrefab;

//    private Transform _playerTransform;

//    private static float spawnTimer;

//    private static List<GameObject> obstacles;

//    private void Awake()
//    {
//        _playerTransform = GameObject.Find("Player").transform;
//        obstacles = new List<GameObject>();

//        for (var i = 0; i < spawnDensity; i++)
//        {
//            var obstacle = Instantiate(obstaclePrefab, transform);
//            obstacles.Add(obstacle);
//        }
//    }

//    private void Update()
//    {
//        //obstacles

//        if (spawnTimer > 0f)
//        {
//            spawnTimer -= Time.deltaTime;
//            return;
//        }

//        var playerPos = _playerTransform.transform.position;

//        var rangeX = Random.Range(-10f, 10f);
//        var rangeY = Random.Range(5f, 30f);
//        var spawnPos = playerPos + new Vector3(rangeX, rangeY);

//        obstacles.ForEach(x => x.transform.position = spawnPos);

//        spawnTimer = spawnTimeRate;
//    }
//}

using Helper;
using SkiFree2;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace SkiFree2.Spawn
{
    public abstract class SpawnerBase : MonoBehaviour
    {
        [Range(1f, 500f)]
        [SerializeField] private int spawnDensity;
        [Space(10)]
        [Header("Offset")]
        [SerializeField] private float horizontalOffset;
        [SerializeField] private float topOffset;
        [SerializeField] private float bottomOffset;
        [Space(10)]
        [SerializeField] private GameObject prefab;
        [SerializeField] private Transform spawnTransform;

        private float spawnBorderLeft;
        private float spawnBorderRight;
        private float spawnBorderTop;
        private float spawnBorderBottom;

        private List<Rigidbody2D> _spawnedObjects;

        private static bool _isActive;
        private static bool _isReseted;
        private static Transform _skierTransform;

        [Inject]
        public void Construct(Skier skier)
        {
            _skierTransform = skier.transform;
        }

        private void Start()
        {
            spawnBorderLeft = Boundary.Left - horizontalOffset;
            spawnBorderRight = Boundary.Right + horizontalOffset;
            spawnBorderTop = Boundary.Bottom;
            spawnBorderBottom = Boundary.Bottom + bottomOffset;
            Generate();
        }

        private void FixedUpdate()
        {
            if (!_isActive)
            {
                _spawnedObjects.ForEach(x => x.velocity = Vector2.zero);
                return;
            }

            foreach (var spawnedObject in _spawnedObjects)
            {
                spawnedObject.velocity = WorldDriver.Velocity;

                if (spawnedObject.transform.position.y > Boundary.Top + 1f)
                    spawnedObject.transform.position = GetRandomSpawnPos();
            }
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
            var rangeX = Random.Range(spawnBorderLeft, spawnBorderRight);
            var rangeY = Random.Range(spawnBorderBottom, spawnBorderTop);
            var spawnPos = _skierTransform.position + new Vector3(rangeX, rangeY);

            return spawnPos;
        }

        public void ResetGame()
        {
            _isActive = false;
            _isReseted = true;

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
            _isReseted = true;
        }

        private void OnDrawGizmos()
        {
            //var leftTopCorner = new Vector2(spawnBorderLeft, spawnBorderTop);
            //var rightTopCorner = new Vector2(spawnBorderRight, spawnBorderTop);
            //var rightBottomCorner = new Vector2(spawnBorderRight, spawnBorderBottom);
            //var leftBottomCorner = new Vector2(spawnBorderLeft, spawnBorderBottom);

            //Gizmos.color = Color.red;
            //Gizmos.DrawLine(leftTopCorner, rightTopCorner);
            //Gizmos.color = Color.cyan;
            //Gizmos.DrawLine(rightTopCorner, rightBottomCorner);
            //Gizmos.color = Color.black;
            //Gizmos.DrawLine(rightBottomCorner, leftBottomCorner);
            //Gizmos.color = Color.blue;
            //Gizmos.DrawLine(leftBottomCorner, leftTopCorner);
        }
    }
}

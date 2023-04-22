using UnityEngine;
using Zenject;

namespace SkiFree2
{
    public class MajinGremlin : MonoBehaviour
    {
        private const float _chaseRange = 25f;
        private const float _chaseTriggerDistance = 500f;

        private static float _chaseBeginDistanceCounter;
        private static float _chaseDistanceCounter;

        private Gremlin _gremlin;

        [Inject]
        public void Construct(Gremlin gremlin)
        {
            _gremlin = gremlin;
        }

        private void Update()
        {
            if (_gremlin.isChasing)
            {
                _chaseDistanceCounter = WorldDriver.Distance - _chaseBeginDistanceCounter;

                if (_chaseDistanceCounter > _chaseRange)
                    _gremlin.StopChasing();

                return;
            }

            _chaseBeginDistanceCounter = WorldDriver.Distance;

            if (_chaseBeginDistanceCounter >= _chaseTriggerDistance)
                _gremlin.Chase();

        }

        public void ResetGame()
        {
            _chaseBeginDistanceCounter = 0f;
            _chaseDistanceCounter = 0f;
        }
    }
}

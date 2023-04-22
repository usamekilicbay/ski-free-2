using SkiFree2.Cursor;
using UnityEngine;
using Zenject;

namespace SkiFree2
{
    public class WorldDriver : MonoBehaviour
    {
        [Range(1f, 50f)]
        [SerializeField] private float maxSpeed;
        [Range(0.01f, 1f)]
        [SerializeField] private float acceleration;
        [SerializeField] private AnimationCurve accelerationCurve;
        [Range(0.1f, 5f)]
        [SerializeField] private float defaultSpeed;

        private bool _isRunning;
        private float accelerationCurveTimer;

        public const float BoostSpeed = 40f;

        public static Vector3 Velocity { get; private set; }
        public static float Distance { get; private set; }
        public static float Speed { get; private set; }

        private void FixedUpdate()
        {
            if (!_isRunning)
                return;

            if (CursorTracker.Direction.y >= 0f 
                || Skier.SpeedModifier == 0f)
            {
                accelerationCurveTimer = 0f;
                Speed = 0f;
            }
            else
            {
                if (Speed < maxSpeed)
                {
                    var timeSpan = Speed < defaultSpeed
                        ? 0f : 1f;
                    Speed += accelerationCurve.Evaluate(timeSpan) * 0.1f;
                }
                else
                    Speed = maxSpeed;
            }

            Velocity = -CursorTracker.Direction * (Speed * Skier.SpeedModifier);
            Distance += Velocity.y * Time.deltaTime;
        }

        public void ResetGame()
        {
            _isRunning = false;
            Speed = 0f;
            Velocity = Vector3.zero;
            Distance = 0f;
        }

        public void StartGame()
        {
            _isRunning = true;
        }

        public void StopGame()
        {
            _isRunning = false;
        }
    }
}

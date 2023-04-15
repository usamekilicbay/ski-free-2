using UnityEngine;

public class WorldDriver : MonoBehaviour
{
    [Range(1f, 50f)]
    [SerializeField] private float maxSpeed;
    [Range(0.01f, 1f)]
    [SerializeField] private float acceleration;
    [Range(0.1f, 5f)]
    [SerializeField] private float defaultSpeed;

    private Gremlin _gremlin;
    private bool _isChased;


    public const float BoostSpeed = 40f;

    public static Vector3 Velocity { get; private set; }
    public static float Distance;
    public static float Speed { get; private set; }

    private void Awake()
    {
        Speed = defaultSpeed;
        _gremlin = FindAnyObjectByType<Gremlin>();
    }

    private void FixedUpdate()
    {
        if (Skier.IsStumbled || CursorTracker.Direction.y >= 0f)
        {
            Speed = 0f;
            Velocity = Speed * Vector2.zero;
        }
        else if (Skier.IsBoosted)
        {
            Speed = BoostSpeed;
            Velocity = Speed * Vector2.up;
        }
        else
        {
            if (Speed < maxSpeed)
                Speed += acceleration * Time.deltaTime;
            else
                Speed = maxSpeed;

            Velocity = Speed * -CursorTracker.Direction;
        }

        Distance += Velocity.y * Time.deltaTime;
    }

    private void LateUpdate()
    {
        if (!_isChased && Distance > 20f)
        {
            _isChased = true;
            _gremlin.Chase();
        }
    }
}

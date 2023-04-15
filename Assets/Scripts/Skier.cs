using System.Collections;
using TMPro;
using UnityEngine;

public class Skier : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI speedText;

    private Transform skierArtTransform;

    private BoxCollider2D _boxCollider;

    public const float StumbleTimeRate = 1.2f;
    public const float BoostTimeRate = 1f;

    public static bool IsStumbled;
    public static bool IsBoosted;
    public static bool IsDead;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        skierArtTransform = transform.GetChild(0);
    }

    private void Update()
    {
        if (IsDead && Input.GetMouseButtonDown(0))
            IsDead = false;

        LookAtCursor();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var layer = collision.gameObject.layer;

        switch (layer)
        {
            case 7:
                IsStumbled = true;
                StartCoroutine(EndStumbling());
                break;
            case 8:
                _boxCollider.enabled = false;
                IsBoosted = true;
                StartCoroutine(EndBoosting());
                break;
            case 9:
                IsDead = true;
                break;
        }
    }

    private void LookAtCursor()
    {
        float angle = Mathf.Atan2(CursorTracker.Direction.y, CursorTracker.Direction.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.AngleAxis(angle + 90f, Vector3.forward);

        skierArtTransform.rotation = rotation;
    }

    private IEnumerator EndBoosting()
    {
        yield return new WaitForSeconds(BoostTimeRate);

        _boxCollider.enabled = true;
        IsBoosted = false;
    }

    private IEnumerator EndStumbling()
    {
        yield return new WaitForSeconds(BoostTimeRate);

        IsStumbled = false;
    }
}

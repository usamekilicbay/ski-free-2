using TMPro;
using UnityEngine;

public class Skier : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI speedText;

    private Transform skierArtTransform;

    private void Awake()
    {
        skierArtTransform = transform.GetChild(0);
    }

    private void Update()
    {
        LookAtCursor();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var layer = collision.gameObject.layer;

        switch (layer)
        {
            case 7:
                //_rb.velocity = Vector3.zero;
                //CursorTracker.GlitchTimer = CursorTracker.GlitchTimerLimit;
                break;
            case 9:
                // TODO: Game Over
                break;
        }
    }

    private void LookAtCursor()
    {
        float angle = Mathf.Atan2(CursorTracker.Direction.y, CursorTracker.Direction.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.AngleAxis(angle + 90f, Vector3.forward);

        skierArtTransform.rotation = rotation;
    }
}

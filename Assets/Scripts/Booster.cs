using SkiFree2.Abstracts;
using UnityEngine;

public class Booster : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out IBoostable boostable))
            boostable.Boost(2f);
    }
}

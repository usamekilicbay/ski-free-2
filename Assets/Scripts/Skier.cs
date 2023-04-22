using DG.Tweening;
using SkiFree2.Abstracts;
using SkiFree2.Cursor;
using System;
using System.Collections;
using UnityEngine;

namespace SkiFree2
{
    public class Skier : MonoBehaviour, IKillable, IBoostable
    {
        private BoxCollider2D _boxCollider;
        private Transform _skierArtTransform;

        private readonly WaitForSeconds _stumbleWait = new(StumbleTimeRate);
        private readonly WaitForSeconds _boostWait = new(BoostTimeRate);

        public const float StumbleTimeRate = 1.2f;
        public const float BoostTimeRate = 3f;

        public static float SpeedModifier { get; private set; }

        public event Action OnDeath;

        private void Awake()
        {
            _boxCollider = GetComponent<BoxCollider2D>();
            _skierArtTransform = transform.GetChild(0);
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
                    SpeedModifier = 0f;
                    StartCoroutine(EndStumbling());
                    break;
                case 8:
                    //_boxCollider.enabled = false;
                    //SpeedModifier = 2f;
                    //StartCoroutine(EndBoosting());
                    break;
            }
        }

        private void LookAtCursor()
        {
            float angle = Mathf.Atan2(CursorTracker.Direction.y, CursorTracker.Direction.x) * Mathf.Rad2Deg;

            Quaternion rotation = Quaternion.AngleAxis(angle + 90f, Vector3.forward);

            _skierArtTransform.rotation = rotation;
        }

        private IEnumerator EndStumbling()
        {
            yield return _stumbleWait;

            SpeedModifier = 1f;
        }

        //private IEnumerator EndBoosting()
        private void EndBoosting()
        {
            //yield return _boostWait;

            SpeedModifier = 1f;
            _boxCollider.enabled = true;
        }

        public void Boost(float boostedSpeed)
        {
            _boxCollider.enabled = false;
            SpeedModifier = boostedSpeed;

            var seq = DOTween.Sequence();
            seq.Append(_skierArtTransform.DOScale(2f, 0.5f));
            seq.SetEase(Ease.Linear);
            seq.Append(_skierArtTransform.DOScale(1f, BoostTimeRate - 0.5f));
            seq.OnComplete(EndBoosting);
        }

        public void ResetGame()
        {
            SpeedModifier = 1f;
        }

        public void Kill()
        {
            SpeedModifier = 0f;
            OnDeath?.Invoke();
        }
    }
}

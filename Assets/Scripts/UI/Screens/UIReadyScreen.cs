using DG.Tweening;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace SkiFree2.UI.Screens
{
    public class UIReadyScreen : UIScreenBase
    {
        [SerializeField] TextMeshProUGUI titleText;

        private Tween _pulseTween;

        private void Awake()
        {
            _pulseTween = titleText.DOColor(Color.clear, 1f);
            _pulseTween.SetLoops(-1, LoopType.Yoyo);
            _pulseTween.Pause();
        }

        public override async Task Show()
        {
            _pulseTween.Play();

            await base.Show();
        }

        public override Task Hide()
        {
            _pulseTween.Pause();

            return base.Hide();
        }
    }
}

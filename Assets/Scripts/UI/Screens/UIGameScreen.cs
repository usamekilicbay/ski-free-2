using TMPro;
using UnityEngine;

namespace SkiFree2.UI.Screens
{
    public class UIGameScreen : UIScreenBase
    {
        [SerializeField] private TextMeshProUGUI speedText;
        [SerializeField] private TextMeshProUGUI distanceText;

        private void Update()
        {
            speedText.SetText(WorldDriver.Speed.ToString("F1"));
            distanceText.SetText(WorldDriver.Distance.ToString("F1"));
        }
    }
}

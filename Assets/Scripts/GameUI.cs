using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private TextMeshProUGUI distanceText;
    [Space(10)]
    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject gameOverUI;

    private void Start()
    {
        gameUI.SetActive(true);
        gameOverUI.SetActive(false);
    }

    private void Update()
    {
        if (Skier.IsDead)
        {
            gameUI.SetActive(false);
            gameOverUI.SetActive(true);
            return;
        }

        speedText.SetText(WorldDriver.Speed.ToString("F1"));
        distanceText.SetText(WorldDriver.Distance.ToString("F1"));
    }
}

using SkiFree2;
using SkiFree2.UI;
using SkiFree2.UI.Screens;
using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    private enum GameState
    {
        Ready,
        Game,
        GameOver
    }

    private static bool _isStarted;
    private static GameState _gameState;

    private Skier _skier;
    private MajinGremlin _majinGremlin;
    private Gremlin _gremlin;
    private WorldDriver _worldDriver;
    private ObstacleManager _obstacleManager;
    private BoosterManager _boosterManager;

    private UIManagerBase _uiManager;
    private UIGameScreen _uiGameScreen;
    private UIGameOverScreen _uiGameOverScreen;
    private UIReadyScreen _uiReadyScreen;

    [Inject]
    public void Construct(Skier skier,
        MajinGremlin majinGremlin,
        Gremlin gremlin,
        WorldDriver worldDriver,
        ObstacleManager obstacleManager,
        BoosterManager boosterManager,
        UIManagerBase uIManager,
        UIReadyScreen uIReadyScreen,
        UIGameScreen uIGameScreen,
        UIGameOverScreen uIGameOverScreen)
    {
        _worldDriver = worldDriver;
        _obstacleManager = obstacleManager;
        _boosterManager = boosterManager;

        _skier = skier;
        _majinGremlin = majinGremlin;
        _gremlin = gremlin;

        _uiManager = uIManager;

        _uiReadyScreen = uIReadyScreen;
        _uiGameScreen = uIGameScreen;
        _uiGameOverScreen = uIGameOverScreen;
    }

    private void Start()
    {
        ResetGame();
    }

    private void OnEnable()
    {
        _skier.OnDeath += OnPlayerDeath;
    }

    private void OnDisable()
    {
        _skier.OnDeath -= OnPlayerDeath;
    }

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0))
            return;

        if (_gameState == GameState.Ready)
            StartGame();
        if (_gameState == GameState.GameOver)
            ResetGame();
    }

    private void ResetGame()
    {
        _gameState = GameState.Ready;
        _isStarted = false;
        _worldDriver.ResetGame();
        _obstacleManager.ResetGame();
        _boosterManager.ResetGame();
        _majinGremlin.ResetGame();
        _gremlin.ResetGame();
        _skier.ResetGame();
        _uiManager.ShowScreen(_uiReadyScreen);
    }

    private void StartGame()
    {
        _gameState = GameState.Game;
        _isStarted = true;
        _worldDriver.StartGame();
        _obstacleManager.StartGame();
        _boosterManager.StartGame();
        _majinGremlin.enabled = true;
        _uiManager.ShowScreen(_uiGameScreen);
    }

    private void OnPlayerDeath()
    {
        _gameState = GameState.GameOver;
        _isStarted = false;
        _worldDriver.StopGame();
        _obstacleManager.StopGame();
        _boosterManager.StopGame();
        _majinGremlin.enabled = false;
        _gremlin.Feast();
        _uiManager.ShowScreen(_uiGameOverScreen);
    }
}

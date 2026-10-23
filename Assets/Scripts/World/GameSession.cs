using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] private AreaCoordinator areaCoordinator;
    [SerializeField] private CamaraFollow camera;
    [SerializeField] private AreaContext initialArea;
    [SerializeField] private Health playerHealth;

    public event Action<GameSessionState> StateChanged;
    public GameSessionState State { get; private set; }

    private void OnEnable()
    {
        playerHealth.Died += EndGame;
    }

    private void OnDisable()
    {
        playerHealth.Died -= EndGame;
    }

    void Start()
    {
        StartGame();
    }

    void StartGame()
    {
        areaCoordinator.PrepareArea(initialArea);
        State = GameSessionState.Playing;
    }

    public void StartNewGame()
    {
        if (State != GameSessionState.Playing) return;
        State = GameSessionState.Playing;
    }
    public void RequestPause()
    {
        if (State != GameSessionState.Playing) return;

        Time.timeScale = 0f;
        State = GameSessionState.Paused;
        StateChanged?.Invoke(State);
    }

    public void RequestResume()
    {
        if (State != GameSessionState.Paused) return;

        Time.timeScale = 1f;
        State = GameSessionState.Playing;
        StateChanged?.Invoke(State);
    }

    public void RequestRespawn()
    {
        if (State != GameSessionState.Playing) return;
        State = GameSessionState.Transitioning;
    }
    public void EndGame()
    {
        if (State == GameSessionState.GameOver) return;

        State = GameSessionState.GameOver;
        Time.timeScale = 0f;
        StateChanged?.Invoke(State);
    }

    public void StartTransition()
    {
        if (State != GameSessionState.Playing) return;
        State = GameSessionState.Transitioning;
        StateChanged?.Invoke(State);
    }

    public void EndTransition()
    {
        if (State != GameSessionState.Transitioning) return;
        State = GameSessionState.Playing;
        StateChanged?.Invoke(State);
    }

    public void GoToMainMenu()
    {
        Debug.Log($"GoToMainMenu ejecutado. Estado: {State}");
        if (State != GameSessionState.GameOver && State != GameSessionState.Paused) return;

        if (PlayerController.instancia != null)
            Destroy(PlayerController.instancia.gameObject);

        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuPrincipal");
    }
}
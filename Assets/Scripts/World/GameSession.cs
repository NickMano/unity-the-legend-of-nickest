using System;
using UnityEditor;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    [SerializeField] private GameObject initialMap;
    [SerializeField] private CamaraFollow camera;

    public GameSessionState State { get; private set; }


    void StartGame()
    {

        Camera.main.GetComponent<CamaraFollow>().SetBound(initialMap);
    }

    public void StartNewGame()
    {
        State = GameSessionState.Playing;
    }
    public void RequestPause()
    {
        if (State != GameSessionState.Playing) return;
        State = GameSessionState.Paused;
    }
    public void RequestResume()
    {
        if (State != GameSessionState.Paused) return;
        State = GameSessionState.Playing;
    }

    public void RequestRespawn()
    {
        if (State != GameSessionState.Playing) return;
        State = GameSessionState.Transitioning;
    }
    public void EndGame()
    {
        if (State != GameSessionState.Playing) return;
        State = GameSessionState.GameOver;
    }
}
using UnityEngine;

public class GameOverView : MonoBehaviour
{
    [SerializeField] private GameSession session;
    [SerializeField] private Canvas view;

    void OnEnable()
    {
        session.StateChanged += HandleStateChanged;
    }

    void OnDisable()
    {
        session.StateChanged -= HandleStateChanged;
    }

    void Start()
    {
        HandleStateChanged(session.State);
    }

    private void HandleStateChanged(GameSessionState state)
    {
        view.enabled = state == GameSessionState.GameOver;
    }
}
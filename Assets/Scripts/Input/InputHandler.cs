using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private GameSession session;

    public event Action JumpPressed;
    public event Action AttackPressed;
    public event Action PausePressed;

    void Update()
    {
        if (session.State == GameSessionState.Playing)
        {
            ReportInputPressed(Input.GetKeyDown(KeyCode.Space), JumpPressed);
            ReportInputPressed(Input.GetKeyDown(KeyCode.E), AttackPressed);
        }

        ReportInputPressed(Input.GetKeyDown(KeyCode.Escape), PausePressed);
    }

    public float MoveAxis => Input.GetAxis("Horizontal");

    private void ReportInputPressed(bool isPressed, Action action)
    {
        if (!isPressed) return;
        action?.Invoke();
    }
}
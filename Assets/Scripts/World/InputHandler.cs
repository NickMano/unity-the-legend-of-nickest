using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public event Action onJumpPressed;
    public event Action onAttackPressed;

    void Update()
    {
        bool isJumping = Input.GetKeyDown(KeyCode.Space);
        if (isJumping)
        {
            onJumpPressed?.Invoke();
        }

        bool isAttacking = Input.GetKeyDown(KeyCode.E);
        if (isAttacking)
        {
            onAttackPressed?.Invoke();
        }

    }
}
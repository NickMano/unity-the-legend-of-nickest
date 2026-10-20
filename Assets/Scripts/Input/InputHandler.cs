using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public event Action JumpPressed;
    public event Action AttackPressed;

    void Update()
    {
        bool isJumping = Input.GetKeyDown(KeyCode.Space);
        if (isJumping)
        {
            JumpPressed?.Invoke();
        }

        bool isAttacking = Input.GetKeyDown(KeyCode.E);
        if (isAttacking)
        {
            AttackPressed?.Invoke();
        }

    }

    public float MoveAxis => Input.GetAxis("Horizontal");
}
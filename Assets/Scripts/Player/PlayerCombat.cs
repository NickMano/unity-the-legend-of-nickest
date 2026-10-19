using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerCombat : MonoBehaviour
{
    private static readonly int AttackTrigger = Animator.StringToHash("Atacando");

    [SerializeField] private CapsuleCollider2D weaponCollider;
    [SerializeField] private float hitStart = 0.12f;
    [SerializeField] private float hitEnd = 0.4f;
    private Animator animator;
    private bool attackAllowed = true;
    private bool attackRequested = false;

    void Awake()
    {
        weaponCollider.enabled = false;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        bool isAttacking = IsPlayingAttack();

        if (isAttacking)
            attackRequested = false;

        UpdateAttackCollider(isAttacking);
    }

    public void Attack()
    {
        if (!CanAttack()) return;

        attackRequested = true;
        animator.SetTrigger(AttackTrigger);
    }

    public void CancelAttack()
    {
        attackRequested = false;
        animator.ResetTrigger(AttackTrigger);
        weaponCollider.enabled = false;
    }

    public void SetAttackAllowed(bool allowed)
    {
        attackAllowed = allowed;

        if (!allowed)
            CancelAttack();
    }

    private void UpdateAttackCollider(bool isAttacking)
    {
        float time = animator
            .GetCurrentAnimatorStateInfo(0)
            .normalizedTime;

        weaponCollider.enabled =
            attackAllowed &&
            isAttacking &&
            time > hitStart &&
            time < hitEnd;
    }

    private bool CanAttack()
    {
        return attackAllowed &&
            !attackRequested &&
            !IsPlayingAttack();
    }

    private bool IsPlayingAttack()
    {
        return animator
            .GetCurrentAnimatorStateInfo(0)
            .IsName("JugadorEspada");
    }

    private void OnDisable()
    {
        SetAttackAllowed(false);
    }
}
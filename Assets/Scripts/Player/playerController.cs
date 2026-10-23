using UnityEngine;
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(PlayerMotor))]
[RequireComponent(typeof(PlayerCombat))]
[RequireComponent(typeof(Health))]
public class PlayerController : MonoBehaviour
{
	private static readonly int EnSueloHash = Animator.StringToHash("EnSuelo");
	private static readonly int VelHash = Animator.StringToHash("Vel");
	public static PlayerController instancia = null;

	[SerializeField, Min(0f)]
	private float enemyBounceImpulse = 10f;
	[SerializeField]
	private Vector2 knockbackImpulse = new(5f, 6f);

	[SerializeField] private InputHandler inputHandler;

	private PlayerCombat combat;
	private PlayerMotor motor;
	private Health health;
	private SpriteRenderer spriteRenderer;
	private Animator animator;

	public Vector3 posIni;

	public int jumpDamage { get; private set; } = 1;

	void Awake()
	{
		if (!instancia) instancia = this;
		else if (instancia != this)
		{
			Destroy(gameObject);
			return;
		}

		DontDestroyOnLoad(gameObject);

		combat = GetComponent<PlayerCombat>();
		motor = GetComponent<PlayerMotor>();
		health = GetComponent<Health>();
		animator = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	void OnEnable()
	{
		inputHandler.JumpPressed += HandleJump;
		inputHandler.AttackPressed += HandleAttack;
		combat.SetAttackAllowed(true);
		motor.SetMovementAllowed(true);
	}

	void OnDisable()
	{
		inputHandler.JumpPressed -= HandleJump;
		inputHandler.AttackPressed -= HandleAttack;

		combat.SetAttackAllowed(false);
		combat.CancelAttack();

		motor.SetMovementAllowed(false);
		motor.ResetMotion();
	}

	void Start()
	{
		posIni = transform.position;
	}

	void Update()
	{
		animator.SetFloat(VelHash, Mathf.Abs(motor.velocity.x));
		animator.SetBool(EnSueloHash, motor.isGrounded);
	}

	private void FixedUpdate()
	{
		motor.SetMoveInput(inputHandler.MoveAxis);
	}

	public void SetTransitionLocked(bool isLocked)
	{
		motor.ResetMotion();
		motor.SetMovementAllowed(!isLocked);
		motor.enabled = !isLocked;
		combat.SetAttackAllowed(!isLocked);
	}

	public void SpawnTo(Vector3 position) => transform.position = position;

	private void SeCayo()
	{
		motor.ResetMotion();
		transform.position = posIni;
		health.TakeDamage(1);
	}

	public void EnemyJump()
	{
		motor.RequestBounce(enemyBounceImpulse);
	}

	public void EnemyKnock(float enemyX)
	{
		float direction = transform.position.x >= enemyX ? 1f : -1f;

		Vector2 force = new(direction * knockbackImpulse.x, knockbackImpulse.y);
		motor.RequestKnockback(force);

		spriteRenderer.color = Color.red;
		health.TakeDamage(1);
		Invoke(nameof(ActivarMove), 0.5f);
	}

	void ActivarMove()
	{
		motor.SetMovementAllowed(true); ;
		spriteRenderer.color = Color.white;
	}

	public void SetPosIni(Vector3 pos) => posIni = pos;
	private void HandleAttack() => combat.RequestAttack();
	private void HandleJump() => motor.RequestJump();
}

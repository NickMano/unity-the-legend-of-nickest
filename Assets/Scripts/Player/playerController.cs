using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
	private static readonly int EnSueloHash = Animator.StringToHash("EnSuelo");
	private static readonly int VelHash = Animator.StringToHash("Vel");
	public static PlayerController instancia = null;

	[SerializeField, Min(0f)]
	private float enemyBounceImpulse = 10f;
	[SerializeField]
	private Vector2 knockbackImpulse = new(5f, 6f);

	[SerializeField] private GameObject camara;
	[SerializeField] private InputHandler inputHandler;
	[SerializeField] private PlayerCombat combat;
	[SerializeField] private PlayerMotor motor;

	private SpriteRenderer spriteRenderer;
	private Animator animator;

	public Vector3 posIni;
	public HealthController healthController;
	public GameObject initialMap;
	public GameObject initialEnemyPreb;

	void Awake()
	{
		if (!instancia) instancia = this;
		else if (instancia != this)
		{
			Destroy(gameObject);
			return;
		}

		DontDestroyOnLoad(gameObject);

		animator = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		Camera.main.GetComponent<CamaraFollow>().SetBound(initialMap);
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
		if (initialEnemyPreb) Instantiate(initialEnemyPreb);
		// Go to checkpoint if fall 
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

	private void SeCayo()
	{
		motor.ResetMotion();
		transform.position = posIni;
		healthController.TakeDamage(1);
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
		healthController.TakeDamage(1);
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

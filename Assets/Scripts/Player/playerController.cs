using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
	private static readonly int EnSueloHash = Animator.StringToHash("EnSuelo");
	private static readonly int VelHash = Animator.StringToHash("Vel");
	public static PlayerController instancia = null;

	[SerializeField] private GameObject camara;
	[SerializeField] private InputHandler inputHandler;
	[SerializeField] private PlayerCombat combat;

	private Rigidbody2D rg;
	private SpriteRenderer spriteRenderer;
	private Animator animator;

	//Acciones-------//
	private bool jumpRequested;
	private bool doubleJumpEnable;
	private bool movimiento = true;

	//---------------//

	public float speed = 20f;
	public float maxSpeed = 5f;
	public bool enSuelo;
	public float poderSalto = 120f;
	public Vector3 posIni;
	public VidaController vida;
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

		rg = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		Camera.main.GetComponent<CamaraFollow>().SetBound(initialMap);
	}

	void OnEnable()
	{
		inputHandler.JumpPressed += Jump;
		inputHandler.AttackPressed += HandleAttack;
		combat.SetAttackAllowed(true);
	}

	void OnDisable()
	{
		inputHandler.JumpPressed -= Jump;
		inputHandler.AttackPressed -= HandleAttack;

		combat.SetAttackAllowed(false);
		jumpRequested = false;
	}

	void Start()
	{
		if (initialEnemyPreb) Instantiate(initialEnemyPreb);
		// Go to checkpoint if fall 
		posIni = transform.position;
	}


	void Update()
	{
		animator.SetFloat(VelHash, Mathf.Abs(rg.linearVelocity.x));
		animator.SetBool(EnSueloHash, enSuelo);
	}

	private void FixedUpdate()
	{
		Vector3 fixedVelocity = rg.linearVelocity;
		fixedVelocity.x *= 0.75f;

		if (enSuelo)
			rg.linearVelocity = fixedVelocity;

		float h = Input.GetAxis("Horizontal");
		if (!movimiento) h = 0;

		float velLimite = Mathf.Clamp(rg.linearVelocity.x, -maxSpeed, maxSpeed);
		rg.AddForce(h * speed * Vector2.right);

		rg.linearVelocity = new Vector2(velLimite, rg.linearVelocity.y);
		if (h > 0.1f)
			transform.localScale = new Vector3(1f, 1f, 1f);
		if (h < -0.1f)
			transform.localScale = new Vector3(-1f, 1f, 1f);

		if (jumpRequested && doubleJumpEnable) // First jump
		{
			rg.linearVelocity = new Vector2(rg.linearVelocity.x, 0);
			rg.AddForce(Vector2.up * poderSalto, ForceMode2D.Impulse);
			jumpRequested = false;
		}
		else if (jumpRequested)
		{
			rg.linearVelocity = new Vector2(rg.linearVelocity.x, 0);
			rg.AddForce(Vector2.up * poderSalto * .75f, ForceMode2D.Impulse);
			jumpRequested = false;
		}
	}

	private void SeCayo()
	{
		transform.position = posIni;
		vida.RestarVida();
	}

	public void EnemyJump()
	{
		jumpRequested = true;
	}

	public void EnemyKnock(float posX)
	{
		jumpRequested = true;
		float side = Mathf.Sign(posX - transform.position.x);
		rg.AddForce(Vector2.left * side * poderSalto, ForceMode2D.Impulse);

		movimiento = false;
		spriteRenderer.color = Color.red;
		vida.RestarVida();
		Invoke("ActivarMove", 0.5f);

	}

	void ActivarMove()
	{
		movimiento = true;
		spriteRenderer.color = Color.white;
	}

	public void SetPosIni(Vector3 pos)
	{
		posIni = pos;
	}

	private void Jump()
	{
		if (enSuelo)
		{
			jumpRequested = true;
			doubleJumpEnable = true;
		}
		else if (doubleJumpEnable)
		{
			jumpRequested = true;
			doubleJumpEnable = false;
		}
	}

	private void HandleAttack()
	{
		combat.Attack();
	}
}

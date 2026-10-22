using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Health))]
public class EnemyController : MonoBehaviour, IDamageable
{
	private static WaitForSeconds _waitForSeconds_5 = new(.5f);
	[SerializeField] private float vel = 1.5f;
	[SerializeField] private float maxVel = 1.5f;

	private Vector3 start;
	private Vector3 final;
	private SpriteRenderer spriteRenderer;
	private Rigidbody2D rg;
	private Health health;

	public bool showLife;
	public Transform objetivo;

	void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		rg = GetComponent<Rigidbody2D>();
		health = GetComponent<Health>();
	}

	void OnEnable()
	{
		health.HealthChanged += HandleHealthChanged;
		health.Died += HandleDead;
	}

	void OnDisable()
	{
		health.HealthChanged -= HandleHealthChanged;
		health.Died -= HandleDead;
	}

	void Start()
	{
		if (!objetivo) return;

		objetivo.parent = null;
		start = transform.position;
		final = objetivo.position;
	}

	private void FixedUpdate()
	{
		if (!objetivo)
		{
			float velLimite = Mathf.Clamp(rg.linearVelocity.x, -maxVel, maxVel);
			rg.AddForce(Vector2.right * vel);

			rg.linearVelocity = new Vector2(velLimite, rg.linearVelocity.y);

			if (rg.linearVelocity.x > -0.01f && rg.linearVelocity.x < 0.01f)
			{
				vel *= -1f;
				rg.linearVelocity = new Vector2(vel, rg.linearVelocity.y);
			}


			if (vel < 0.1f)
				transform.localScale = new Vector3(1f, 1f, 1f);
			if (vel > -0.1f)
				transform.localScale = new Vector3(-1f, 1f, 1f);
		}
		else
		{
			Patrol();
		}
	}

	public void TakeDamage(DamageInfo damage)
	{
		Debug.Log("take damage from enemyconroller: ${damage}");
		health.TakeDamage(damage.Amount);
	}

	private IEnumerator AnimAtacado()
	{
		spriteRenderer.color = Color.red;
		yield return _waitForSeconds_5;
		spriteRenderer.color = Color.white;
	}

	private void Patrol()
	{
		float fixVel = vel * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, objetivo.position, fixVel);

		if (transform.position != objetivo.position) return;
		ChangeDirection();
	}

	private void ChangeDirection()
	{
		objetivo.position = objetivo.position == start ? final : start;
		transform.localScale = new Vector3(-transform.localScale.x, 1f, 1f);
	}

	private void OnGUI()
	{
		Vector2 pos = Camera.main.WorldToScreenPoint(transform.position);
		if (showLife)
			GUI.Box(
				new Rect(
					pos.x - 20,
					Screen.height - pos.y,
					40,
					24
				), health.currentHealth + "/" + health.GetMaxHealth()
			);
	}

	private void HandleHealthChanged()
	{
		showLife = true;
		StartCoroutine(nameof(AnimAtacado));
	}

	private void HandleDead()
	{
		Destroy(gameObject);
	}
}

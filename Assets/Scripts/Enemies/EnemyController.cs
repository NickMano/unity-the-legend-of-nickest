using System.Collections;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]

[RequireComponent(typeof(SpriteRenderer))]
public class EnemyController : MonoBehaviour
{
	private static WaitForSeconds _waitForSeconds_5 = new(.5f);
	[SerializeField] private float vel = 1.5f;
	[SerializeField] private float maxVel = 1.5f;

	[Tooltip("Vida Max")]
	[SerializeField] private int maxHp;
	[SerializeField] private int hp;
	public bool mostrarVida;

	private Vector3 start;
	private Vector3 final;
	public Transform objetivo;

	private SpriteRenderer spriteRenderer;
	private Rigidbody2D rg;

	void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		rg = GetComponent<Rigidbody2D>();
	}

	void Start()
	{
		if (objetivo)
		{
			objetivo.parent = null;
			start = transform.position;
			final = objetivo.position;
		}
		hp = maxHp;
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
			float fixVel = vel * Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position, objetivo.position, fixVel);

			if (transform.position == objetivo.position)
			{
				if (objetivo.position == start) objetivo.position = final;
				else objetivo.position = start;

				transform.localScale = new Vector3(-transform.localScale.x, 1f, 1f);

			}
		}
	}
	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag("Player") || col.CompareTag("Weapon"))
		{
			mostrarVida = true;

			if (col.CompareTag("Player")) col.SendMessage("EnemyKnock", transform.position.x);
			else Atacado();
		}

	}

	public void Atacado()
	{
		if (--hp <= 0) Destroy(gameObject);
		StartCoroutine(nameof(AnimAtacado));
	}

	IEnumerator AnimAtacado()
	{
		spriteRenderer.color = Color.red;
		yield return _waitForSeconds_5;
		spriteRenderer.color = Color.white;
	}

	private void OnGUI()
	{
		Vector2 pos = Camera.main.WorldToScreenPoint(transform.position);
		if (mostrarVida)
			GUI.Box(
				new Rect(
					pos.x - 20,
					Screen.height - pos.y,
					40,
					24
				), hp + "/" + maxHp
			);
	}
}

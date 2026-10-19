using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CheckPointController : MonoBehaviour
{
	private static readonly int ActivarCheckHash = Animator.StringToHash("activarCheck");
	private Vector2 pos;
	public Sprite check;
	private Animator animator;
	private bool activarCheck;

	void Awake()
	{
		animator = GetComponent<Animator>();
	}

	void Start()
	{
		pos = new Vector2(transform.position.x, transform.position.y);
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.CompareTag("Player"))
		{
			col.SendMessage("SetPosIni", pos);
			activarCheck = true;
			animator.SetBool(ActivarCheckHash, activarCheck);
		}
	}
}

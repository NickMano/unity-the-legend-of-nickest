using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(EdgeCollider2D))]
public class PlataformaDebil : MonoBehaviour
{

	private Rigidbody2D rgbd;
	private EdgeCollider2D polic;
	private Vector3 pos;

	public float delayCaer = 0.25f;
	public float delaySpawn = 5f;

	// Use this for initialization
	void Start()
	{
		rgbd = GetComponent<Rigidbody2D>();
		polic = GetComponent<EdgeCollider2D>();
		pos = transform.position;
		rgbd.gravityScale = 1.5f;
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			Invoke(nameof(Caer), delayCaer);
			Invoke(nameof(Respawn), delaySpawn);
		}
	}

	void Caer()
	{
		rgbd.isKinematic = false;
		polic.isTrigger = true;
	}

	void Respawn()
	{
		transform.position = pos;
		rgbd.linearVelocity = Vector3.zero;
		rgbd.isKinematic = true;
		polic.isTrigger = false;
	}
}

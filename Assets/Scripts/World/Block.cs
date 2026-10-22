using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Block : MonoBehaviour
{
	protected SpriteRenderer spriteRenderer;
	public Sprite nuevo;
	protected Sprite sprite1;

	void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		sprite1 = spriteRenderer.sprite;
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("Player") && other.transform.position.y + 1.5f < transform.position.y)
			spriteRenderer.sprite = nuevo;

	}
}

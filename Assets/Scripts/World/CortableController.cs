using UnityEngine;

public class CortableController : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Weapon"))
		{
			Destroy(gameObject);
			Debug.Log("fui cortado");
		}
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.CompareTag("Weapon"))
		{
			Destroy(gameObject);
			Debug.Log("fui cortado");
		}
	}
}

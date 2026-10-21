using UnityEngine;

public class Attack : MonoBehaviour
{

	// Use this for initialization
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Enemigo") && collision.TryGetComponent<EnemyController>(out var enemy))
		{
			Debug.Log("atacado");
			enemy.TakeDamage(1);
		}
	}
}

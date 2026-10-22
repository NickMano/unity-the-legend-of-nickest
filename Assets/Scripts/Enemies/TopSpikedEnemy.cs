using UnityEngine;

public class TopSpikedEnemy : EnemyController
{
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player") && other.TryGetComponent<PlayerController>(out var player))
		{
			showLife = true;
			player.EnemyKnock(transform.position.x);
		}
	}
}

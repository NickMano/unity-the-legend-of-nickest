using UnityEngine;

public class SideSpikedEnemy : EnemyController
{
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.TryGetComponent<PlayerController>(out var player))
		{
			showLife = true;
			if ((other.transform.position.y - 0.3f) > transform.position.y)
			{
				player.EnemyJump();
				TakeDamage(new DamageInfo(player.jumpDamage));
			}
			else
				player.EnemyKnock(transform.position.x);
		}
	}
}

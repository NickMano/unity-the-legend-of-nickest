using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySimple : EnemyController
{
	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag("Player") || col.CompareTag("Weapon"))
		{
			showLife = true;
			if (col.CompareTag("Player") && col.TryGetComponent<PlayerController>(out var player))
			{
				if ((col.transform.position.y - 0.3f) > transform.position.y)
				{
					player.EnemyJump();
					TakeDamage(1);
				}
				else
					player.EnemyKnock(transform.position.x);
			}
			else
				TakeDamage(1);
		}
	}
}

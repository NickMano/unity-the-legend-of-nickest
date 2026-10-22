using UnityEngine;

public class Attack : MonoBehaviour
{
	[SerializeField, Min(1)] private int damage = 1;

	private void OnTriggerEnter2D(Collider2D other)
	{
		var target = other.GetComponentInParent<IDamageable>();
		target?.TakeDamage(new DamageInfo(damage));
	}
}

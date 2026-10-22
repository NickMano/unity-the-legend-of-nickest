using UnityEngine;

public class CortableController : MonoBehaviour, IDamageable
{
	public void TakeDamage(DamageInfo info)
	{
		Destroy(gameObject);
	}
}

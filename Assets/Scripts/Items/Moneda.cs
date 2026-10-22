using UnityEngine;

public class Moneda : MonoBehaviour
{
	[SerializeField, Min(1)] private int value;
	private bool isColleted = false;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (isColleted) return;

		if (other.gameObject.TryGetComponent<Wallet>(out var wallet))
		{
			wallet.Add(value);
			isColleted = true;

			Destroy(gameObject);
			Debug.Log("sume " + value);
		}
	}
}

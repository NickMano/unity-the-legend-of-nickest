using UnityEngine;

public class Moneda : MonoBehaviour
{

	[SerializeField] private int value;

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (!col.CompareTag("Player")) return;
		var player = col.GetComponent<PlayerController>();
		player.AddMoney(value);
		Destroy(gameObject);
		Debug.Log("sume " + value);
	}

}

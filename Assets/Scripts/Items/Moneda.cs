using UnityEngine;

public class Moneda : MonoBehaviour
{

	[SerializeField] private int value;

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (!col.CompareTag("Player")) return;

		col.SendMessage("AddMoney", value);
		Destroy(gameObject);
		Debug.Log("sume " + value);
	}

}

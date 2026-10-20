using UnityEngine;

public class GroundDetector : MonoBehaviour
{

	private PlayerController player;
	private PlayerMotor motor;

	// Use this for initialization
	void Start()
	{
		player = GetComponentInParent<PlayerController>();
		motor = GetComponentInParent<PlayerMotor>();
	}

	private void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.CompareTag("plataforma"))
		{
			motor.ResetMotion();
			player.transform.parent = col.transform;
			motor.SetIsGrounded(true);
		}
	}

	void OnCollisionStay2D(Collision2D col)
	{
		if (col.gameObject.CompareTag("plataforma"))
		{
			player.transform.parent = col.transform;
			motor.SetIsGrounded(true);

		}
		if (col.gameObject.CompareTag("suelo"))
		{
			motor.SetIsGrounded(true);
		}
	}

	void OnCollisionExit2D(Collision2D col)
	{
		if (col.gameObject.CompareTag("plataforma"))
		{
			player.transform.parent = null;
			motor.SetIsGrounded(false);

			DontDestroyOnLoad(player);
		}
		if (col.gameObject.CompareTag("suelo"))
		{
			motor.SetIsGrounded(false);
		}
	}
}

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

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("plataforma"))
		{
			motor.ResetMotion();
			player.transform.parent = other.transform;
			motor.SetIsGrounded(true);
		}
	}

	void OnCollisionStay2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("plataforma"))
		{
			player.transform.parent = other.transform;
			motor.SetIsGrounded(true);

		}
		if (other.gameObject.CompareTag("suelo"))
		{
			motor.SetIsGrounded(true);
		}
	}

	void OnCollisionExit2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("plataforma"))
		{
			player.transform.parent = null;
			motor.SetIsGrounded(false);

			DontDestroyOnLoad(player);
		}
		if (other.gameObject.CompareTag("suelo"))
		{
			motor.SetIsGrounded(false);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{

	private PlayerController player;
	private Rigidbody2D rgbd;

	// Use this for initialization
	void Start()
	{
		player = GetComponentInParent<PlayerController>();
		rgbd = GetComponentInParent<Rigidbody2D>();
	}

	private void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "plataforma")
		{
			rgbd.linearVelocity = new Vector3(0f, 0f, 0f);
			player.transform.parent = col.transform;
			player.enSuelo = true;
		}
	}

	void OnCollisionStay2D(Collision2D col)
	{
		if (col.gameObject.tag == "plataforma")
		{
			player.transform.parent = col.transform;
			player.enSuelo = true;
		}
		if (col.gameObject.tag == "suelo")
		{
			player.enSuelo = true;
		}
	}

	void OnCollisionExit2D(Collision2D col)
	{
		if (col.gameObject.tag == "plataforma")
		{
			player.transform.parent = null;
			player.enSuelo = false;
			DontDestroyOnLoad(player);
		}
		if (col.gameObject.tag == "suelo")
		{
			player.enSuelo = false;
		}
	}
}

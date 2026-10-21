using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class AreaController : MonoBehaviour
{
	private Animator animator;

	// Use this for initialization
	void Start()
	{
		animator = GetComponent<Animator>();
	}

	public IEnumerator FadeOut(string area)
	{
		animator.Play("AreaShow");

		transform.GetChild(0).GetComponent<Text>().text = area;
		transform.GetChild(1).GetComponent<Text>().text = area;

		animator.Play("AreaFadeOut");

		yield return null;
	}
}

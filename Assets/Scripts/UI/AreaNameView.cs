using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class AreaNameView : MonoBehaviour
{
	private static readonly int AreaFadeOutTrigger = Animator.StringToHash("AreaFadeOut");
	private static readonly int AreaShowTrigger = Animator.StringToHash("AreaShow");
	private Animator animator;

	void Awake()
	{
		animator = GetComponent<Animator>();
	}

	public IEnumerator FadeOut(string area)
	{
		animator.Play(AreaShowTrigger);

		transform.GetChild(0).GetComponent<Text>().text = area;
		transform.GetChild(1).GetComponent<Text>().text = area;

		animator.Play(AreaFadeOutTrigger);

		yield return null;
	}
}

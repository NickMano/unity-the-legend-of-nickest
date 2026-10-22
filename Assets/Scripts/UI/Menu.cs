using UnityEngine;

public class Menu : MonoBehaviour
{
	public static Menu instancia = null;

	public Canvas panel;
	private bool pausa;

	[SerializeField] private InputHandler inputHandler;

	private void Awake()
	{
		if (!instancia) instancia = this;
		else if (instancia != this)
			Destroy(gameObject);
	}

	void Start()
	{
		panel.enabled = false;
	}

	void OnEnable()
	{
		inputHandler.PausePressed += HandlePause;
	}

	void OnDisable()
	{
		inputHandler.PausePressed -= HandlePause;

	}

	// Update is called once per frame
	private void HandlePause()
	{
		pausa = !pausa;
		Time.timeScale = pausa ? 0f : 1f;
		panel.enabled = pausa;
	}
}

using UnityEngine;

public class Menu : MonoBehaviour
{
	public static Menu instancia = null;

	[SerializeField] private Canvas canvasPause;

	[SerializeField] private InputHandler inputHandler;
	[SerializeField] private GameSession session;

	private void Awake()
	{
		if (!instancia) instancia = this;
		else if (instancia != this)
			Destroy(gameObject);
	}

	void Start()
	{
		HandleStateChanged(session.State);
	}

	void OnEnable()
	{
		inputHandler.PausePressed += HandlePause;
		session.StateChanged += HandleStateChanged;
	}

	void OnDisable()
	{
		inputHandler.PausePressed -= HandlePause;
		session.StateChanged -= HandleStateChanged;
	}

	private void HandlePause()
	{
		if (session.State == GameSessionState.Playing)
		{
			session.RequestPause();
			return;
		}

		if (session.State == GameSessionState.Paused)
			session.RequestResume();
	}

	private void HandleStateChanged(GameSessionState state)
	{
		canvasPause.enabled = state == GameSessionState.Paused;
	}
}

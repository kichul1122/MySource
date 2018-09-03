using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KC_SceneController : FSM<KC_SceneController.State>
{
	public enum State
	{
		LOGO,
		LOGIN,
		ADVENTURE,
		BATTLEFIELD,
		TEST,
		MAX
	}
	#region Private Variables
	[SerializeField]
	private FadeScene fadeScenePrefab;

	[SerializeField]
	private State state;
	#endregion


	#region Public Methods
	[ContextMenu("SetSceneState")]
	public void SetSceneState()
	{
		SetState(this.state);
	}

	public void Fade(bool isIn)
	{
		FadeScene fadeScene = Instantiate(fadeScenePrefab);
		if (fadeScene != null)
		{
			fadeScene.Fade(isIn);
		}
	}

	public IEnumerator FadeCoroutine(bool isIn)
	{
		FadeScene fadeScene = Instantiate(fadeScenePrefab);
		if (fadeScene != null)
		{
			yield return fadeScene.FadeCoroutine(isIn);
		}
	}

	public void LoadScene(State state)
	{
		SceneManager.LoadScene(state.ToString());
	}

	public void LoadSceneAsync(State state, LoadSceneMode loadSceneMode)
	{
		SceneManager.LoadSceneAsync(state.ToString(), loadSceneMode);
	}
	#endregion

	private void Awake()
	{
		SetState(State.LOGO);

		DontDestroyOnLoad(this);
	}

	#region LOGO
	private IEnumerator LOGO_Enter()
	{
		//LoadScene(ActiveCurrentState);
		yield return FadeCoroutine(true);

		yield return new WaitForSeconds(0f);

		SetState(State.LOGIN);
	}

	private void LOGO_Update()
	{

	}

	private IEnumerator LOGO_Exit()
	{
		yield return FadeCoroutine(false);
	}
	#endregion

	#region LOGIN
	private IEnumerator LOGIN_Enter()
	{
		//LoadScene(ActiveCurrentState);
		LoadSceneAsync(ActiveCurrentState, LoadSceneMode.Single);

		yield return FadeCoroutine(true);
	}

	private void LOGIN_Update()
	{

	}

	private IEnumerator LOGIN_Exit()
	{
		yield return FadeCoroutine(false);
	}
	#endregion

	#region ADVENTURE
	private IEnumerator ADVENTURE_Enter()
	{
		LoadScene(ActiveCurrentState);

		yield return FadeCoroutine(true);
	}

	private void ADVENTURE_Update()
	{

	}

	private IEnumerator ADVENTURE_Exit()
	{
		yield return FadeCoroutine(false);
	}
	#endregion

	#region BATTLEFIELD
	private IEnumerator BATTLEFIELD_Enter()
	{
		LoadScene(ActiveCurrentState);

		yield return FadeCoroutine(true);
	}

	private void BATTLEFIELD_Update()
	{

	}

	private IEnumerator BATTLEFIELD_Exit()
	{
		yield return FadeCoroutine(false);
	}
	#endregion

	#region TEST
	private IEnumerator TEST_Enter()
	{
		LoadScene(ActiveCurrentState);

		yield return FadeCoroutine(true);
	}

	private void TEST_Update()
	{

	}

	private IEnumerator TEST_Exit()
	{
		yield return FadeCoroutine(false);
	}
	#endregion


}
/*
	#region LOGIN
	void LOGIN_Enter()
	{

	}

	void LOGIN_Update()
	{

	}

	void LOGIN_Exit()
	{

	}
	#endregion
	*/

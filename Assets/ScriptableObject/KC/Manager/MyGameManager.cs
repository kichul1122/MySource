using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;

[CreateAssetMenu(menuName ="Manager/Game")]
public class MyGameManager : ScriptableObject
{
	//public StringVariableRx googleID;

	//public UserDatabase userDatabase;

	//public DesignManager designManager;

	//public DatabasePacket databasePacket;

	//public StringVariableRx logoState;

	//public GameEvent onLogoTweenStart;

	//public void Initialize()
	//{
	//	logoState.Value = "Load UserData";
	//	databasePacket.Login(googleID.Value, () =>
	//	{
	//		logoState.Value = "Load Design";
	//		designManager.Initialize();

	//		MainThreadDispatcher.StartCoroutine(SettingScenes());
			
	//		Debug.Log("Complete");
	//	});
	//}

	//public IEnumerator SettingScenes()
	//{
	//	logoState.Value = "Load Scene";
	//	yield return SceneManager.LoadSceneAsync("UI", LoadSceneMode.Additive);
	//	yield return SceneManager.LoadSceneAsync("NewMain", LoadSceneMode.Additive);

	//	logoState.Value = "Game Start";
	//	onLogoTweenStart?.Raise();
	//}

	//public void OnLogoTweenCompelte()
	//{
	//	SceneManager.UnloadSceneAsync("Logo");
	//}
}

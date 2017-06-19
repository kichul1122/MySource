using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
	private void Start()
	{
		ShowSceneManager();
	}

	public void ShowSceneManager()
	{
		Scene scene = SceneManager.GetActiveScene();
		//Debug.Log(scene.buildIndex);
		//Debug.Log(scene.isDirty);
		//Debug.Log(scene.isLoaded);
		//Debug.Log(scene.name);
		Debug.Log(scene.path);
		//Debug.Log(scene.rootCount);

		//foreach (GameObject gameObject in scene.GetRootGameObjects())
		//{
		//	Debug.Log(gameObject.name);
		//}

		//Debug.Log(scene.IsValid());
		
		//Debug.Log(SceneManager.sceneCount.ToString());
		//Debug.Log(SceneManager.sceneCountInBuildSettings.ToString());

		SceneManager.activeSceneChanged += activeSceneChanged;
		SceneManager.sceneLoaded += sceneLoaded;
		SceneManager.sceneUnloaded += sceneUnloaded;

		//SceneManager.CreateScene("CreateScene");

		//scene = SceneManager.GetActiveScene();
		//Debug.Log(scene.name);

		//scene = SceneManager.GetSceneAt(0);
		//Debug.Log(scene.name);

		//scene = SceneManager.GetSceneByBuildIndex(0);
		//Debug.Log(scene.name);

		scene = SceneManager.GetSceneByName("Test");
		Debug.Log(scene.name);

		//scene = SceneManager.GetSceneByPath("Assets/Scene/AdditiveTest.unity");
		//Debug.Log(scene.name);

		SceneManager.LoadScene("AdditiveTest", LoadSceneMode.Additive);

		Scene sceneAdditive = SceneManager.GetSceneByName("AdditiveTest");

		StartCoroutine(MergeScenes(scene, sceneAdditive));
		//SceneManager.SetActiveScene(scene);
		//StartCoroutine(async(asyncOperation));

		//AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("AdditiveTest", LoadSceneMode.Additive);

		//StartCoroutine(async(asyncOperation));

		//SceneManager.UnloadSceneAsync(0);

		//asyncOperation = SceneManager.LoadSceneAsync("AdditiveSphere", LoadSceneMode.Single);

		//StartCoroutine(async(asyncOperation));

		//SceneManager.LoadScene("Test2");
	}

	IEnumerator MergeScenes(Scene source, Scene destination)
	{
		yield return new WaitForSeconds(5);

		SceneManager.MergeScenes(source, destination);
	}

	IEnumerator async(AsyncOperation asyncOperation)
	{
		while (!asyncOperation.isDone)
		{
			Debug.Log(asyncOperation.progress);
			yield return null;
		}

		Debug.Log("async End");
	}

	public void activeSceneChanged(Scene scene1, Scene scene2)//scene1 : 현재씬 //scene2 : 바뀔신
	{
		Debug.Log("activeSceneChanged Scene 1 : " + scene1.name + "  activeSceneChanged Scene 2 : " + scene2.name);
	}

	public void sceneLoaded(Scene scene1, LoadSceneMode sceneMode)
	{
		Debug.Log("sceneLoaded Scene 1 : " + scene1.name + "  sceneLoaded SceneMode : " + sceneMode.ToString());
	}

	public void sceneUnloaded(Scene scene)
	{
		Debug.Log("sceneUnloaded Scene 1 : " + scene.name);
	}
}

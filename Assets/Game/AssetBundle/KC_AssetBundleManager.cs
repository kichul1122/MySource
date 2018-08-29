/////////////////////
/////KC_3D PROJECT///
/////////////////////

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using AssetBundles;

///// <summary>
///// 에셋번들만들기
///// 1.Build AssetBundles하기, Assset Bundle Version 파일도 생성하기
///// 1.서버에 올릴 경우 : Build AssetBundles해서 생성된 AssetBundles폴더를 올리면된다.
///// 2.Asset/StreamingAssets폴더 에 올릴 경우 : Build AssetBundles해서 생성된 AssetBundles폴더를 StreamingAssets로 복사한다.
///// </summary>
//[RequireComponent(typeof(AssetBundleVersion))]
//public class KC_AssetBundleManager : SingletonPersistant<KC_AssetBundleManager>
//{
//	#region Private Variables
//	private bool isStreamingAssets = true;

//	private AssetBundleVersion assetBundleVersion;
//	#endregion

//	#region Public Variables

//	#endregion

//	#region Properties
//	#endregion

//	#region Protected Variables
//	protected override void GameSetup()
//	{
//		base.GameSetup();

//		assetBundleVersion = GetComponent<AssetBundleVersion>();
//	}
//	#endregion

//	#region Public Methods
//	public IEnumerator Initialize()
//	{
//		Debug.Log("AssetBundle Init");

		
//		if (isStreamingAssets)//버전정보를 Resource폴더에서 가져옴
//		{
//			//Assets/StreamingAssets를 사용 할경우 에셋번들 URL설정

//			AssetBundleManager.SetSourceAssetBundleURL(GetStreamingAssetsPath());
//		}
//		else//서버에서 가져옴
//		{
//			//https://s3.ap-northeast-2.amazonaws.com/kichul1122/AssetBundles/
//			//AssetBundleManager/Resources/AssetBundleServerURL에 에셋번들 URL설정
//			AssetBundleManager.SetDevelopmentAssetBundleServer();
//		}
		

//		// Initialize AssetBundleManifest which loads the AssetBundleManifest object.
//		var request = AssetBundleManager.Initialize();
//		if (request != null)
//		{
//			yield return StartCoroutine(request);
//		}
//	}

//	public void LoadSaveAssetBundleInfo()
//	{
//		assetBundleVersion.LoadSaveAssetBundleInfo(isStreamingAssets);
//		if(assetBundleVersion.HaveDownloadAssetBundles())
//		{
//			//다운로드하시겠습니까?
//		}
//	}

//	public bool HaveDownloadAssetBundles()
//	{
//		return assetBundleVersion.HaveDownloadAssetBundles();
//	}

//	public IEnumerator DownLoadAssetBundles()
//	{
//		yield return StartCoroutine(assetBundleVersion.DownLoadAssetBundles());
//	}

//	public bool IsCompleteDownLoadAssetBundles()
//	{
//		return assetBundleVersion.IsCompleteDownLoadAssetBundles();
//	}

//	public string GetDownLoadPercent()
//	{
//		return string.Format("{0}/{1}", assetBundleVersion.downloadedAssetBundleFileSize, assetBundleVersion.downloadAssetBundleFileSize);
//	}
//	#endregion

//	#region Private Methods
//	private string GetStreamingAssetsPath()
//	{
//		string path = string.Empty;
//#if (UNITY_EDITOR || UNITY_STANDALONE_WIN)
//		path = "file://" + Application.streamingAssetsPath + "/AssetBundles/";
//#elif UNITY_ANDROID
//		path =  "jar:file://" + Application.dataPath + "!/assets" + "/AssetBundles/";
//#elif UNITY_IOS
//		path = "file:///" + Application.streamingAssetsPath + "/AssetBundles/";
//#endif
//		return path;
//	}
//	#endregion

//	[ContextMenu("CleanCache")]
//	public void CleanCache()
//	{
//		PlayerPrefs.DeleteKey(AssetBundleVersion.savePlayerPrefsKey);
//		Caching.ClearCache();
//	}

//	public IEnumerator InstantiateGameObjectAsync(string assetBundleName, string assetName)
//	{
//		// This is simply to get the elapsed time for this phase of AssetLoading.
//		float startTime = Time.realtimeSinceStartup;

//		// Load asset from assetBundle.
//		AssetBundleLoadAssetOperation request = AssetBundleManager.LoadAssetAsync(assetBundleName, assetName, typeof(GameObject));
//		if (request == null)
//			yield break;
//		yield return StartCoroutine(request);

//		// Get the asset.
//		GameObject prefab = request.GetAsset<GameObject>();

//		if (prefab != null)
//			GameObject.Instantiate(prefab);

//		// Calculate and display the elapsed time.
//		float elapsedTime = Time.realtimeSinceStartup - startTime;
//		Debug.Log(assetName + (prefab == null ? " was not" : " was") + " loaded successfully in " + elapsedTime + " seconds");
//	}

//	public IEnumerator LoadAsset<T>(string assetBundleName, string assetName, System.Action<T> action) where T : UnityEngine.Object
//	{
//		AssetBundleLoadAssetOperation request = AssetBundleManager.LoadAssetAsync(assetBundleName, assetName, typeof(T));
//		if (request == null)
//		{
//			yield break;
//		}

//		yield return StartCoroutine(request);

//		T requestAsset = request.GetAsset<T>();

//		if (requestAsset != null)
//		{
//			action(requestAsset);
//		}

//		AssetBundleManager.UnloadAssetBundle(assetBundleName);
//	}

//	public IEnumerator LoadAsset<T1, T2>(string assetBundleName, string assetName, System.Action<T1, T2> action, T2 actionParameter) where T1 : UnityEngine.Object
//	{
//		AssetBundleLoadAssetOperation request = AssetBundleManager.LoadAssetAsync(assetBundleName, assetName, typeof(T1));
//		if (request == null)
//		{
//			yield break;
//		}

//		yield return StartCoroutine(request);

//		T1 requestAsset = request.GetAsset<T1>();

//		if (requestAsset != null)
//		{
//			action(requestAsset, actionParameter);
//		}

//		AssetBundleManager.UnloadAssetBundle(assetBundleName);
//	}

//	//public IEnumerator LoadTextAsset<T>(string assetBundleName, string assetName, System.Action<List<T>, string> action, List<T> actionParameter)
//	//{
//	//	float startTime = Time.realtimeSinceStartup;

//	//	AssetBundleLoadAssetOperation request = AssetBundleManager.LoadAssetAsync(assetBundleName, assetName, typeof(TextAsset));
//	//	if (request == null)
//	//		yield break;
//	//	yield return StartCoroutine(request);

//	//	TextAsset textAsset = request.GetAsset<TextAsset>();

//	//	if(textAsset != null)
//	//	{
//	//		action(actionParameter, textAsset.ToString());
//	//	}

//	//	float elapsedTime = Time.realtimeSinceStartup - startTime;
//	//}
//}


////TextAsset urlFile = Resources.Load("AssetBundleServerURL") as TextAsset;
////string url = (urlFile != null) ? urlFile.text.Trim() : null;
////if (url == null || url.Length == 0)
////{
////	Debug.LogError("Development Server URL could not be found.");
////	//AssetBundleManager.SetSourceAssetBundleURL("http://localhost:7888/" + UnityHelper.GetPlatformName() + "/");
////}
////else
////{
////	AssetBundleManager.SetSourceAssetBundleURL(url);
////}
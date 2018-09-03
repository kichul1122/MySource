/////////////////////
/////KC_3D PROJECT///
/////////////////////

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System.IO;
//using AssetBundles;

///// <summary>
///// 
///// </summary>
//public class AssetBundleVersion : MonoBehaviour
//{
//	#region Public Variables
//	public static readonly string savePlayerPrefsKey = "SaveAssetBundleInfo";

//	public string downloadAssetBundleFileSize;
//	public string downloadedAssetBundleFileSize;

//	private double downloadAssetBundleFileSizeCount;
//	private double downloadedAssetBundleFileSizeCount;

//	public SaveAssetBundleInfo saveAssetBundleInfo = new SaveAssetBundleInfo();

	
//	#endregion

//	#region Public Methods
//	public void LoadSaveAssetBundleInfo(bool isStreamingAssets)
//	{
//		Load();

//		if (saveAssetBundleInfo == null)
//		{
//			saveAssetBundleInfo = new SaveAssetBundleInfo();
//		}

//		if (isStreamingAssets)
//		{
//			saveAssetBundleInfo.UpdateDownloadAssetBundleList_StreamingAssets();
//		}
//		else
//		{
//			saveAssetBundleInfo.UpdateDownloadAssetBundleList_Server();
//		}

//		downloadAssetBundleFileSizeCount = saveAssetBundleInfo.GetDownloadAssetBundleFileSize();
//		downloadAssetBundleFileSize = saveAssetBundleInfo.GetFileSize(downloadAssetBundleFileSizeCount);
//	}

//	public IEnumerator DownLoadAssetBundles()
//	{
//		yield return StartCoroutine(LoadFromCacheOrDownLoad());
//	}
	
//	public bool IsCompleteDownLoadAssetBundles()
//	{
//		if (saveAssetBundleInfo == null) return false;

//		return saveAssetBundleInfo.m_UpdateAssetBundleNameList.Count == 0;
//	}

//	public bool HaveDownloadAssetBundles()
//	{
//		if (saveAssetBundleInfo == null) return false;

//		return saveAssetBundleInfo.m_UpdateAssetBundleNameList.Count > 0;
//	}
//	#endregion

//	#region Private Methods
//	private void Save()
//	{
//		PlayerPrefs.SetString(savePlayerPrefsKey, JsonUtility.ToJson(saveAssetBundleInfo, true));
//	}

//	private void Load()
//	{
//		saveAssetBundleInfo = JsonUtility.FromJson<SaveAssetBundleInfo>(PlayerPrefs.GetString(savePlayerPrefsKey));
//	}

//	private IEnumerator LoadFromCacheOrDownLoad()
//	{
//		for (int i = 0; i < saveAssetBundleInfo.m_UpdateAssetBundleNameList.Count;)
//		{
//			string assetBundleName = saveAssetBundleInfo.m_UpdateAssetBundleNameList[i].assetBundleName;

//			Debug.Log(assetBundleName);

//			using (WWW download = WWW.LoadFromCacheOrDownload(AssetBundleManager.BaseDownloadingURL + assetBundleName, AssetBundleManager.AssetBundleManifestObject.GetAssetBundleHash(assetBundleName), 0))
//			{
//				double oriDownloadedAssetBundleFileSize = downloadedAssetBundleFileSizeCount;
//				long fileSize = long.Parse(saveAssetBundleInfo.m_UpdateAssetBundleNameList[i].length);

//				while (!download.isDone)
//				{
//					float progress = download.progress;

//					double addFileSize = (double)fileSize * progress;

//					downloadedAssetBundleFileSizeCount = oriDownloadedAssetBundleFileSize + addFileSize;
//					downloadedAssetBundleFileSize = saveAssetBundleInfo.GetFileSize(downloadedAssetBundleFileSizeCount);
//					yield return null;
//				}

//				if (download.assetBundle == null)
//				{
//					Debug.LogError("No AssetBundle");
//				}
//				else
//				{
//					Debug.Log("CompleteDownLoad : " + saveAssetBundleInfo.m_UpdateAssetBundleNameList[i]);

//					saveAssetBundleInfo.CompleteDownloadAssetBundle(saveAssetBundleInfo.m_UpdateAssetBundleNameList[i]);

//					Save();

//					download.assetBundle.Unload(false);

//					downloadedAssetBundleFileSizeCount = oriDownloadedAssetBundleFileSize + fileSize;
//					downloadedAssetBundleFileSize = saveAssetBundleInfo.GetFileSize(downloadedAssetBundleFileSizeCount);
//				}
//			}
//		}
//	}

//	private long GetDownLoadAssetBundleFileSize()
//	{
//		long fileSize = 0L;

//		for(int i = 0; i < saveAssetBundleInfo.m_UpdateAssetBundleNameList.Count; ++i)
//		{
//			fileSize += long.Parse(saveAssetBundleInfo.m_UpdateAssetBundleNameList[i].length);
//		}

//		return fileSize;
//	}
//	#endregion

//	#region Public Static Methods
//	public static void CreateFile()
//	{
//		DirectoryInfo buildDir = new DirectoryInfo(System.Environment.CurrentDirectory.Replace("\\", "/") + "/AssetBundles/Android");
//		FileInfo[] newAssetBundleFiles = buildDir.GetFiles("*.*", SearchOption.AllDirectories);

//		AssetBundleVersionFile assetBundleVersionFile = new AssetBundleVersionFile();
//		for (int i = 0; i < newAssetBundleFiles.Length; ++i)
//		{
//			if (ExceptionFile(newAssetBundleFiles[i])) continue;

//			if (Path.GetExtension(newAssetBundleFiles[i].Name).Equals(""))//이름,용량저장
//			{
//				AssetBundleInfo assetBundleInfo = assetBundleVersionFile.m_UpdateAssetBundleNameList.Find(x => x.assetBundleName.Equals(newAssetBundleFiles[i].Name));
//				if (assetBundleInfo != null)
//				{
//					assetBundleInfo.length = newAssetBundleFiles[i].Length.ToString();
//				}
//				else
//				{
//					assetBundleVersionFile.m_UpdateAssetBundleNameList.Add(new AssetBundleInfo(newAssetBundleFiles[i].Name, "", newAssetBundleFiles[i].Length.ToString()));
//				}
//			}
//			else//".manifest"//이름/해쉬저장
//			{
//				AssetBundleInfo assetBundleInfo = assetBundleVersionFile.m_UpdateAssetBundleNameList.Find(x => x.assetBundleName.Equals(newAssetBundleFiles[i].Name.Replace(".manifest", "")));
//				if (assetBundleInfo != null)
//				{
//					assetBundleInfo.hash128 = FindHash(newAssetBundleFiles[i].FullName);
//				}
//				else
//				{
//					assetBundleVersionFile.m_UpdateAssetBundleNameList.Add(new AssetBundleInfo(newAssetBundleFiles[i].Name.Replace(".manifest", ""),
//																			FindHash(newAssetBundleFiles[i].FullName), ""));
//				}
//			}
//		}

//		File.WriteAllText(Application.dataPath + "/AssetBundleManager/Resources/AssetBundleVersion.bytes", JsonUtility.ToJson(assetBundleVersionFile, true));
//	}

//	public static string FindHash(string fileFullPath)
//	{
//		string key = "Hash: ";
//		string[] lines = File.ReadAllLines(fileFullPath);
//		int pos = System.Array.FindIndex(lines, row => row.Contains(key));

//		return lines[pos].Replace(key, "").Trim();
//	}

//	public static bool ExceptionFile(FileInfo fileInfo)
//	{
//		if (Path.GetExtension(fileInfo.Name).Equals(".meta")) return true;
//		if (Path.GetFileName(fileInfo.Name).Equals("Android")) return true;
//		if (Path.GetFileName(fileInfo.Name).Equals("Android.manifest")) return true;

//		return false;
//	}
//	#endregion
//}

//[System.Serializable]
//public class SaveAssetBundleInfo
//{
//	public List<AssetBundleInfo> m_AssetBundleNameList = new List<AssetBundleInfo>();//기존 에셋 목록
//	public List<AssetBundleInfo> m_UpdateAssetBundleNameList = new List<AssetBundleInfo>();//추가/수정된 에셋 목록 - 새로 다운로드 받을 에셋 목록

//	public void UpdateDownloadAssetBundleList_Server()
//	{
//		m_UpdateAssetBundleNameList.Clear();

//		m_UpdateAssetBundleNameList = LoadUpdateAssetBundle_Server();
//		if (m_UpdateAssetBundleNameList == null) return;

//		UpdateDownloadAssetBundleList();
//	}

//	public void UpdateDownloadAssetBundleList_StreamingAssets()
//	{
//		m_UpdateAssetBundleNameList.Clear();

//		m_UpdateAssetBundleNameList = LoadUpdateAssetBundle_Streaming();
//		if (m_UpdateAssetBundleNameList == null) return;

//		UpdateDownloadAssetBundleList();
//	}

//	private void UpdateDownloadAssetBundleList()
//	{
//		for (int i = 0; i < m_UpdateAssetBundleNameList.Count;)
//		{
//			bool isUpdate = true;
//			for (int j = 0; j < m_AssetBundleNameList.Count; ++j)
//			{
//				if (m_UpdateAssetBundleNameList[i].assetBundleName.Equals(m_AssetBundleNameList[j].assetBundleName))
//				{
//					if (m_UpdateAssetBundleNameList[i].hash128.Equals(m_AssetBundleNameList[j].hash128))
//					{
//						isUpdate = false;
//						break;
//					}
//				}
//			}

//			if (!isUpdate)
//			{
//				m_UpdateAssetBundleNameList.RemoveAt(i);
//			}
//			else
//			{
//				++i;
//			}
//		}
//	}

//	private List<AssetBundleInfo> LoadUpdateAssetBundle_Server()
//	{
//		//TextAsset assetBundleVersionTextAsset = Resources.Load("AssetBundleVersion") as TextAsset;

//		//string assetBundleVersion = (assetBundleVersionTextAsset != null) ? assetBundleVersionTextAsset.text : null;

//		//if (assetBundleVersion != null && assetBundleVersion.Length != 0)
//		//{
//		//	return JsonUtility.FromJson<AssetBundleVersionFile>(assetBundleVersion).m_UpdateAssetBundleNameList;
//		//}
//		//else
//		//{
//		//	return null;
//		//}

//		return null;
//	}


//	private List<AssetBundleInfo> LoadUpdateAssetBundle_Streaming()
//	{
//		TextAsset assetBundleVersionTextAsset = Resources.Load("AssetBundleVersion") as TextAsset;

//		string assetBundleVersion = (assetBundleVersionTextAsset != null) ? assetBundleVersionTextAsset.text : null;

//		if (assetBundleVersion != null && assetBundleVersion.Length != 0)
//		{
//			return JsonUtility.FromJson<AssetBundleVersionFile>(assetBundleVersion).m_UpdateAssetBundleNameList;
//		}
//		else
//		{
//			return null;
//		}
//	}

//	public void CompleteDownloadAssetBundle(AssetBundleInfo assetBundleInfo)
//	{
//		m_AssetBundleNameList.Add(assetBundleInfo);
//		m_UpdateAssetBundleNameList.Remove(assetBundleInfo);
//	}

//	public double GetDownloadAssetBundleFileSize()
//	{
//		long allFileSize = 0;

//		for (int i = 0; i < m_UpdateAssetBundleNameList.Count; ++i)
//		{
//			long fileSize = 0;
//			if (long.TryParse(m_UpdateAssetBundleNameList[i].length, out fileSize))
//			{
//				allFileSize += fileSize;
//			}
//		}

//		return (double)allFileSize;
//	}

//	public string GetFileSize(double byteCount)
//	{
//		//string size = "0 Bytes";
//		//if (byteCount >= 1073741824.0)
//		//    size = String.Format("{0:##.##}", byteCount / 1073741824.0) + " GB";
//		//else if (byteCount >= 1048576.0)
//		//    size = String.Format("{0:##.##}", byteCount / 1048576.0) + " MB";
//		//else if (byteCount >= 1024.0)
//		//    size = String.Format("{0:##.##}", byteCount / 1024.0) + " KB";
//		//else if (byteCount > 0 && byteCount < 1024.0)
//		//    size = byteCount.ToString() + " Bytes";


//		return string.Format("{0:F2} MB", byteCount / 1048576.0);
//	}
//}

//[System.Serializable]
//public class AssetBundleInfo
//{
//	public string assetBundleName = string.Empty;
//	public string hash128 = string.Empty;
//	public string length = string.Empty;

//	public AssetBundleInfo(string assetBundleName, string hash128, string length)
//	{
//		this.assetBundleName = assetBundleName;
//		this.hash128 = hash128;
//		this.length = length;
//	}
//}

//[System.Serializable]
//public class AssetBundleVersionFile
//{
//	public List<AssetBundleInfo> m_UpdateAssetBundleNameList = new List<AssetBundleInfo>();
//}
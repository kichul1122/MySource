///////////////////
///KC_3D PROJECT///
///////////////////

using UnityEditor;
using UnityEngine;
/// <summary>
/// 
/// </summary>
/// 
[InitializeOnLoad]
public class UtilEditor
{
	//[MenuItem("Tools/Util/Csv To Json")]
	public static void CreateCsvToJson()
	{
		//Util.CreateCsvToJson();
	}

	[MenuItem("Tools/PlayerPrefs DeleteAll")]
	public static void DeleteAll()
	{
		PlayerPrefs.DeleteAll();
	}

	[MenuItem("Tools/Time Scale x0")]
	public static void TimeScale_x0()
	{
		Time.timeScale = 0;
	}

	[MenuItem("Tools/Time Scale x1")]
	public static void TimeScale_x1()
	{
		Time.timeScale = 1;
	}

	[MenuItem("Tools/Time Scale x2")]
	public static void TimeScale_x2()
	{
		Time.timeScale = 2;
	}

	[MenuItem("Tools/Time Scale x5")]
	public static void TimeScale_x5()
	{
		Time.timeScale = 5;
	}

	[MenuItem("Tools/Time Scale x10")]
	public static void TimeScale_x10()
	{
		Time.timeScale = 10;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System;
using System.Linq;
using Keiwando.BigInteger;
//using Sirenix.OdinInspector;

public static class Utility
{
	public static int ChooseProbability(float[] probs)
	{
		float total = 0;

		foreach (float elem in probs)
		{
			total += elem;
		}

		float randomPoint = UnityEngine.Random.value * total;

		for (int i = 0; i < probs.Length; i++)
		{
			if (randomPoint < probs[i])
			{
				return i;
			}
			else
			{
				randomPoint -= probs[i];
			}
		}

		return probs.Length - 1;
	}

	public static void LogPath()
	{
		Debug.Log(System.Environment.CurrentDirectory.Replace("\\", "/"));
		Debug.Log(Application.dataPath);
		Debug.Log(Application.persistentDataPath);
		Debug.Log(Application.streamingAssetsPath);
		Debug.Log(Application.temporaryCachePath);
	}

	public static string NowTime()
	{
		//return System.DateTime.Now.ToString("yy/MM/dd-HH:mm:ss");
		return System.DateTime.Now.ToShortTimeString();
	}

	public static T[] ShuffleArray<T>(T[] array, int seed)
	{
		System.Random prng = new System.Random(seed);

		for (int i = 0; i < array.Length - 1; ++i)
		{
			int randomIndex = prng.Next(i, array.Length);
			T temItem = array[randomIndex];
			array[randomIndex] = array[i];
			array[i] = temItem;
		}

		return array;
	}

	public static IEnumerator WaitForSecondsIgnoreTimeScale(float time)
	{
		float targetTime = Time.realtimeSinceStartup + time;

		while (Time.realtimeSinceStartup < targetTime)
		{
			yield return new WaitForEndOfFrame();
		}
	}
	
	public static bool IsNumber(string value)
	{
		long number1 = 0;
		double number2 = 0;

		return long.TryParse(value, out number1) || double.TryParse(value, out number2);
	}

	public static void DeleteAll()
	{
		PlayerPrefs.DeleteAll();
	}

	public static T CreateInstance<T>(string className) where T : class
	{
		System.Type type = System.Type.GetType(className);

		if (type != null)
		{
			return System.Activator.CreateInstance(type) as T;
		}
		else
		{
			return null;
		}
	}

	public static readonly string[] NUMBER_UNIT = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"
	,"AA", "BB", "CC", "DD", "EE", "FF", "GG", "HH", "II", "JJ", "KK", "LL", "MM", "NN", "OO", "PP", "QQ", "RR", "SS", "TT", "UU", "VB", "WW", "XX", "YY", "ZZ" };

	public static string GetShortNumber(double value)
	{
		int index = -1;

		if (value < 1000.0)
		{
			return string.Format("{0:F0}", value);
		}
		else
		{
			while (value >= 1000.0)
			{
				value = value / 1000.0;
				index++;
			}

			return string.Format("{0:F2}{1}", value, NUMBER_UNIT[index]);
		}
	}

	/// <summary>
	/// 을/를 , 이/가, 은/는
	/// </summary>
	/// <param name="name"></param>
	/// <param name="firstValue"></param>
	/// <param name="secondValue"></param>
	/// <returns></returns>
	public static string GetComleteWordByJongsung(string name, string firstValue, string secondValue)
	{
		char lastName = name[name.Length - 1]; // 한글의 제일 처음과 끝의 범위밖일 경우는 오류 

		if (lastName < 0xAC00 || lastName > 0xD7A3)
		{
			//return name;
			return firstValue;
		}

		string seletedValue = (lastName - 0xAC00) % 28 > 0 ? firstValue : secondValue;
		return seletedValue;
	}

	public static void DrawString(string text, Vector3 worldPos, Color? colour = null)
	{
#if UNITY_EDITOR
		UnityEditor.Handles.BeginGUI();

		var restoreColor = GUI.color;

		if (colour.HasValue) GUI.color = colour.Value;
		var view = UnityEditor.SceneView.currentDrawingSceneView;
		Vector3 screenPos = view.camera.WorldToScreenPoint(worldPos);

		if (screenPos.y < 0 || screenPos.y > Screen.height || screenPos.x < 0 || screenPos.x > Screen.width || screenPos.z < 0)
		{
			GUI.color = restoreColor;
			UnityEditor.Handles.EndGUI();
			return;
		}

		Vector2 size = GUI.skin.label.CalcSize(new GUIContent(text));
		GUI.Label(new Rect(screenPos.x - (size.x / 2), -screenPos.y + view.position.height + 4, size.x, size.y), text);
		GUI.color = restoreColor;
		UnityEditor.Handles.EndGUI();
#endif
	}

	public static void InitTransform(Transform t)
	{
		t.localPosition = Vector3.zero;
		t.localRotation = Quaternion.identity;
		t.localScale = Vector3.one;
	}

	public static string GetDateTimeString(int i = 0)
	{
		return System.DateTime.Now.AddDays(i).ToString("yyyy/MM/dd");
	}

	public static int GetIndexByPercent(int count, float percent)
	{
		if (percent == 1f)
		{
			return count - 1;
		}
		else
		{
			return (int)((float)count * percent);
		}
	}

	public static bool HasElementByIndex<T>(List<T> list, int index)
	{
		return 0 <= index && index < list.Count ? true : false;
	}

	public static bool HasElementByCount<T>(List<T> list, int count)
	{
		return count <= list.Count ? true : false;
	}

	public static string GetCurrency(double number)
	{
		return number.ToString("#,##0");
	}

	public static DateTime TimeStampToDateTime(long unixTimeStamp)
	{
		DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
		dateTime = dateTime.AddMilliseconds(unixTimeStamp).ToLocalTime();
		return dateTime;
	}

	public static T GetRandomElement<T>(params T[] elements)
	{
		List<T> list = new List<T>(elements);

		return list[UnityEngine.Random.Range(0, list.Count)];
	}

	//public static Character FindNearestYEnemyInCharacterBoundary(Character character)
	//{
	//	Character nearestYEnemy = character.Stage.GetEnemyList().OrderBy(enemy => Vector3.Distance(character.Position, new Vector3(character.Position.x, enemy.Position.y, 0))).FirstOrDefault();

	//	return nearestYEnemy && CharacterBoundaryManager.Instance.IsInCharacterBoundary(nearestYEnemy.Position) ? nearestYEnemy : null;
	//}

	//public static Character FindNearestYEnemyInCharacterBoundaryByPosition(Character character, Vector3 position)
	//{
	//	Character nearestYEnemy = character.Stage.GetEnemyList().OrderBy(enemy => Vector3.Distance(enemy.Position, position)).FirstOrDefault();

	//	return nearestYEnemy && CharacterBoundaryManager.Instance.IsInCharacterBoundary(nearestYEnemy.Position) ? nearestYEnemy : null;
	//}
	
	public static Vector3 Rotate(this Vector3 v, float degrees)
	{
		return v.RotateRadians(degrees * Mathf.Deg2Rad);
	}

	public static Vector3 RotateRadians(this Vector3 v, float radians)
	{
		float ca = Mathf.Cos(radians);
		float sa = Mathf.Sin(radians);
		return new Vector3(ca * v.x - sa * v.y, sa * v.x + ca * v.y, 0);
	}

	private static System.Random rng = new System.Random();

	public static void Shuffle<T>(this List<T> list)
	{
		int n = list.Count;
		while (n > 1)
		{
			n--;
			int k = rng.Next(n + 1);
			T value = list[k];
			list[k] = list[n];
			list[n] = value;
		}
	}

	public static List<T> GetShuffleList<T>(List<T> list)
	{
		List<T> returnList = new List<T>(list);

		returnList.Shuffle();

		return returnList;
	}

	public static T NextEnum<T>(this T src) where T : struct
	{
		if (!typeof(T).IsEnum) throw new ArgumentException(String.Format("Argumnent {0} is not an Enum", typeof(T).FullName));

		T[] Arr = (T[])Enum.GetValues(src.GetType());
		int j = Array.IndexOf<T>(Arr, src) + 1;
		return (Arr.Length == j) ? Arr[0] : Arr[j];
	}

	public static void SendEmail(string email, string subject, string body)
	{
		//string mailto = "myapp.support@gmail.com";
		//string subject = EscapeURL("버그 리포트 / 기타 문의사항");
		//string body = EscapeURL
		//	(
		//	 "이 곳에 내용을 작성해주세요.\n\n\n\n" +
		//	 "________" +
		//	 "Device Model : " + SystemInfo.deviceModel + "\n\n" +
		//	 "Device OS : " + SystemInfo.operatingSystem + "\n\n" +
		//	 "________"
		//	);

		//Application.OpenURL("mailto:" + mailto + "?subject=" + subject + "&body=" + body);

		subject = MyEscapeURL(subject);
		body = MyEscapeURL(body);
		Application.OpenURL("mailto:" + email + "?subject=" + subject + "&body=" + body);
	}

	public static string MyEscapeURL(string url)
	{
		return WWW.EscapeURL(url).Replace("+", "%20");
	}

	public static Rect GetScreenRect2D(Camera cam)
	{
		float height = 2f * cam.orthographicSize;
		float width = height * cam.aspect;
		return new Rect(cam.transform.position.x - width / 2f, cam.transform.position.y - height / 2f, width, height);
	}

	public static bool IsInScreenRect2D(Camera cam, Vector3 worldPosition)
	{
		Rect rect = GetScreenRect2D(cam);

		return worldPosition.x >= rect.x && worldPosition.x <= rect.x + rect.width
			&& worldPosition.y >= rect.y && worldPosition.y <= rect.y + rect.height;
	}

	public static bool IsInScreenRect2DByOffset(Camera cam, Vector3 worldPosition, float offset)
	{
		Rect rect = GetScreenRect2D(cam);

		return worldPosition.x >= rect.x - offset && worldPosition.x <= rect.x + rect.width + offset
			&& worldPosition.y >= rect.y - offset && worldPosition.y <= rect.y + rect.height + offset;
	}

	public static Quaternion GetTrackingRotation2D(Vector3 direction)
	{
		//Unity default 2D forward == Vector3.right;
		//local forward = Vector3.up ==> -90
		return Quaternion.AngleAxis(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90, Vector3.forward);
	}

	public static void DrawDebugCircularSector(Transform transform, float angle, float count, float range, Color color)
	{
		var dt = Mathf.PI / count;
		var radian = angle * Mathf.Deg2Rad;
		if (angle < 360)
		{
			Debug.DrawLine(transform.position, GetPosByRadian(transform, -radian / 2f, range), color);
			Debug.DrawLine(transform.position, GetPosByRadian(transform, radian / 2f, range), color);
		}

		for (float t = -radian / 2f; t < radian / 2f; t += dt)
		{
			Debug.DrawLine(GetPosByRadian(transform, t, range), GetPosByRadian(transform, Mathf.Min(t + dt, radian / 2f), range), color);
		}

		Debug.DrawLine(transform.position, transform.position + transform.forward, color);
	}

	public static void DrawGizmosCircularSector(Transform transform, float angle, float count, float range)
	{
		var dt = Mathf.PI / count;
		var radian = angle * Mathf.Deg2Rad;

		if (angle < 360)
		{
			Gizmos.DrawLine(transform.position, GetPosByRadian(transform, -radian / 2f, range));
			Gizmos.DrawLine(transform.position, GetPosByRadian(transform, radian / 2f, range));
		}

		for (float t = -radian / 2f; t < radian / 2f; t += dt)
		{
			Gizmos.DrawLine(GetPosByRadian(transform, t, range), GetPosByRadian(transform, Mathf.Min(t + dt, radian / 2f), range));
		}

		Gizmos.DrawLine(transform.position, transform.position + transform.forward);
	}

	public static Vector3 GetPosByRadian(Transform transform, float radian, float range)
	{
		return transform.position + range * transform.forward * Mathf.Cos(radian) + range * transform.right * Mathf.Sin(radian);
	}
}



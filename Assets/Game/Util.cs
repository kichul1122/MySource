using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System;

public class Util : MonoBehaviour
{
	private void Start()
	{
		ChooseProbability();
	}

	#region ChooseProbability
	float[] testProbability = new float[] { 1f, 2f, 3f, 4f };
	int[] testProbabilityCount = new int[] { 0, 0, 0, 0 };
	int loopCount = 10000;

	private void ChooseProbability()
	{
		string log = string.Empty;
		for (int i = 0; i < loopCount; ++i)
		{
			int selectedIndex = Util.ChooseProbability(testProbability);
			testProbabilityCount[selectedIndex]++;
		}

		foreach (int count in testProbabilityCount)
		{
			float perect = (float)count / (float)loopCount * 100f;
			log += string.Format(" {0}% ", perect);
		}
		Debug.Log("ChooseProbability : " + log);
	}

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
	#endregion



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

	public static string AESEncrypt256(string InputText, string password)
	{
		string Password = password;

		RijndaelManaged RijndaelCipher = new RijndaelManaged();

		// 입력받은 문자열을 바이트 배열로 변환  
		byte[] PlainText = System.Text.Encoding.Unicode.GetBytes(InputText);

		// 딕셔너리 공격을 대비해서 키를 더 풀기 어렵게 만들기 위해서   
		// Salt를 사용한다.  
		byte[] Salt = Encoding.ASCII.GetBytes(Password.Length.ToString());

		PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(Password, Salt);

		// Create a encryptor from the existing SecretKey bytes.  
		// encryptor 객체를 SecretKey로부터 만든다.  
		// Secret Key에는 32바이트  
		// Initialization Vector로 16바이트를 사용  
		ICryptoTransform Encryptor = RijndaelCipher.CreateEncryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));

		MemoryStream memoryStream = new MemoryStream();

		// CryptoStream객체를 암호화된 데이터를 쓰기 위한 용도로 선언  
		CryptoStream cryptoStream = new CryptoStream(memoryStream, Encryptor, CryptoStreamMode.Write);

		cryptoStream.Write(PlainText, 0, PlainText.Length);

		cryptoStream.FlushFinalBlock();

		byte[] CipherBytes = memoryStream.ToArray();

		memoryStream.Close();
		cryptoStream.Close();

		string EncryptedData = Convert.ToBase64String(CipherBytes);

		return EncryptedData;
	}

	//AES_256 복호화  
	public static string AESDecrypt256(string InputText, string password)
	{
		string Password = password;

		RijndaelManaged RijndaelCipher = new RijndaelManaged();

		byte[] EncryptedData = Convert.FromBase64String(InputText);
		byte[] Salt = Encoding.ASCII.GetBytes(Password.Length.ToString());

		PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(Password, Salt);

		// Decryptor 객체를 만든다.  
		ICryptoTransform Decryptor = RijndaelCipher.CreateDecryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));

		MemoryStream memoryStream = new MemoryStream(EncryptedData);

		// 데이터 읽기 용도의 cryptoStream객체  
		CryptoStream cryptoStream = new CryptoStream(memoryStream, Decryptor, CryptoStreamMode.Read);

		// 복호화된 데이터를 담을 바이트 배열을 선언한다.  
		byte[] PlainText = new byte[EncryptedData.Length];

		int DecryptedCount = cryptoStream.Read(PlainText, 0, PlainText.Length);

		memoryStream.Close();
		cryptoStream.Close();

		string DecryptedData = Encoding.Unicode.GetString(PlainText, 0, DecryptedCount);

		return DecryptedData;
	}
}

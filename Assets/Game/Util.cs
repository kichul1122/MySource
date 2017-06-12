using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

		float randomPoint = Random.value * total;

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


}

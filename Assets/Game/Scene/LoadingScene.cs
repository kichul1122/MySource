using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
	public Text loadingText;

	public string loadingFixedText;
	public string loadingMoveText;

	public float duration;
	
	IEnumerator Start()
	{
		while(true)
		{
			int index = (int)Mathf.PingPong(Time.time / duration, (float)loadingMoveText.Length + 1);

			loadingText.text = string.Format("{0}{1}", loadingFixedText, loadingMoveText.Substring(0, Mathf.Clamp(index, 0, loadingMoveText.Length)));
			yield return null;
		}
	}
}

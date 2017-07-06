using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KC_WWW : MonoBehaviour {

	public string url = "http://localhost:3000/template";
	IEnumerator Start()
	{
		WWW www = new WWW(url);
		yield return www;
		Debug.Log(www.text);
	}


}

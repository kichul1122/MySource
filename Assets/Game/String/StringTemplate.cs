///////////////////
///KC_3D PROJECT///
///////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
/// 

//키값의 대소문자를 구분기준으로 하지않는다.
[System.Serializable]
public class StringTemplate 
{
	[SerializeField]
	private string key;
	public string english;
	public string korean;

	public string Key { get { return key.ToLower(); } }
}

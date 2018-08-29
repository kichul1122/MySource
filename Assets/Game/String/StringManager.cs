///////////////////
///KC_3D PROJECT///
///////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;
using KC;
/// <summary>
/// 
/// </summary>
public class StringManager : Singleton<StringManager>
{
	#region Private Variables
	#endregion

	#region Public Variables
	public SystemLanguage systemLanguage;
	public SystemLanguage SYSTEMLANGUAGE
	{
		get
		{
			string language = PlayerPrefs.GetString("systemLanguage");

			if (String.IsNullOrEmpty(language))
			{
				return systemLanguage = Application.systemLanguage;
			}
			else
			{
				systemLanguage = (SystemLanguage)Enum.Parse(typeof(SystemLanguage), language);
				return systemLanguage;
			}
		}

		set
		{
			systemLanguage = value;
			PlayerPrefs.SetString("systemLanguage", value.ToString());
			ChangeLanguage();
		}
	}

	public event System.Action RefreshStringEventHandler;

	[ShowInInspector]
	public StringDDList StringData { get; set; }
	#endregion

	#region Properties
	#endregion

	#region Unity Methods
	#endregion

	#region Interfaces
	#endregion

	#region Public Methods

	public SystemLanguage test;

	[Button]
	public void Test()
	{
		SYSTEMLANGUAGE = test;
	}

	private void Awake()
	{
		StringData = Resources.Load<StringDDList>(nameof(StringDDList));
		//StringData.Init();
	}

	public StringDesignData GetStringeDesignData(string key)
	{
		foreach (var stringDesignData in StringData.designDataDic)
		{
			if (stringDesignData.Value.key == key)
			{
				return stringDesignData.Value;
			}
		}

		return null;
	}

	public string GetString(string key)
	{
		StringDesignData stringDesignData = GetStringeDesignData(key);

		if(stringDesignData != null)
		{
			switch (SYSTEMLANGUAGE)
			{
				case SystemLanguage.Korean:
					return stringDesignData.Korean;
				case SystemLanguage.English:
					return stringDesignData.English;
				case SystemLanguage.Japanese:
					return stringDesignData.Japan;
				default:
					return stringDesignData.English;
			}
		}

		return key;
	}

	public string GetString(params string[] keys)
	{
		System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
		foreach (string key in keys)
		{
			stringBuilder.Append(GetString(key));
		}

		return stringBuilder.ToString();
	}

	public string GetStringByJongSung(string name, string firstValue, string secondValue)
	{
		return GetComleteWordByJongsung(GetString(name), GetString(firstValue), GetString(secondValue));
	}

	/// <summary>
	/// 을/를 , 이/가, 은/는
	/// </summary>
	/// <param name="name"></param>
	/// <param name="firstValue"></param>
	/// <param name="secondValue"></param>
	/// <returns></returns>
	private string GetComleteWordByJongsung(string name, string firstValue, string secondValue)
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

	[ContextMenu("ChangeLanguage")]
	public void ChangeLanguage()
	{
		if(RefreshStringEventHandler != null)
		{
			RefreshStringEventHandler();
		}
	}
		
	#endregion

	#region Private Methods
	#endregion
}

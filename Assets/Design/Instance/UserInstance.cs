using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserInstance
{
	public AccountInstance account;
	public CharacterInstance character;
	public ItemInstance haveItem;

	//private UserInstance() { }
	//private UserInstance(string ID)
	//{
	//	account.accountData.ID = ID;
	//}

	//public static UserInstance Create(string ID)
	//{
	//	return new UserInstance(ID);
	//}

	public void SaveInstance()
	{
		PlayerPrefs.SetString(this.account.accountData.ID.ToString(), JsonUtility.ToJson(this));

		//System.IO.File.WriteAllText(Application.dataPath , JsonUtility.ToJson(this));
	}

	public static UserInstance LoadInstance(string ID)
	{
		return JsonUtility.FromJson<UserInstance>(PlayerPrefs.GetString(ID));
	}
}

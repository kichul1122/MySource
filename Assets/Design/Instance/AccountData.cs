using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AccountData : BaseData
{
	public string ID;
	public string password;

	public string createTime;
	public string recentAccessTime;

	public AccountData()
	{
		
	}
}

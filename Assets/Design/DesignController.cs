using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesignController : MonoBehaviour
{
	public string ID;
	public UserInstance user;
	
	void Start ()
	{
		
	}

	[ContextMenu("SaveUserInstance")]
	public void SaveUserInstance()
	{
		user.account.accountData.ID = this.ID;
		user.SaveInstance();
	}

	[ContextMenu("LoadUserInstance")]
	public void LoadUserInstance()
	{
		user = UserInstance.LoadInstance(ID);
	}

	[ContextMenu("ClearUserInstance")]
	public void ClearUserInstance()
	{
		user = new UserInstance();
	}
}

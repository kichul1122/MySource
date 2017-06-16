using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseData
{
	#region Private Variables
	[SerializeField]
	private string uuid;
	#endregion

	#region Properties
	public string UUID { get { return uuid; } set { uuid = value; }  }
	#endregion

	#region Public Constructors
	public BaseData()
	{
		this.uuid = System.Guid.NewGuid().ToString();
	}
	#endregion
}

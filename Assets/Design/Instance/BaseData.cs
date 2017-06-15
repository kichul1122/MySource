using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseData
{
	protected string uuid;

	//public static BaseData Create()
	//{
	//	return new BaseData();
	//}

	public BaseData()
	{
		this.uuid = System.Guid.NewGuid().ToString();
	}
}

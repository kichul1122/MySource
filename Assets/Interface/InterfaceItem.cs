using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInterfaceItem
{
	void InterfaceTest();
}

public class InterfaceItem : MonoBehaviour, IInterfaceItem
{

	public void InterfaceTest()
	{
		Debug.Log("InterfaceItem");
	}
}

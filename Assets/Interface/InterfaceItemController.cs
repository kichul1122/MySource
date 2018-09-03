using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceItemController : MonoBehaviour
{
	private IInterfaceItem interfaceItem;
	void Start ()
	{
		interfaceItem = GetComponent<IInterfaceItem>();
		if(interfaceItem != null)
		{
			interfaceItem.InterfaceTest();
		}
	}
	
	
}

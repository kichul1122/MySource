using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reference : MonoBehaviour
{
	private void Start()
	{
		Product2 item = new Product2("Kichul", 1111);
		Debug.Log(string.Format("Name: {0}, ID: {1}\n", item.ItemName, item.ItemID));

		ChangeByReference(item);
		Debug.Log(string.Format("Name: {0}, ID: {1}\n", item.ItemName, item.ItemID));

		ChangeByReference(ref item);
		Debug.Log(string.Format("Name: {0}, ID: {1}\n", item.ItemName, item.ItemID));
	}

	void ChangeByReference(Product2 itemRef)
	{
		itemRef.ItemID = 2222;
		itemRef = new Product2("NoRef_Kichul", 3333);
	}

	void ChangeByReference(ref Product2 itemRef)
	{
		itemRef.ItemID = 4444;
		itemRef = new Product2("Ref_Kichul", 5555);
	}
}

class Product2
{
	public Product2(string name, int newID)
	{
		ItemName = name;
		ItemID = newID;
	}

	public string ItemName { get; set; }
	public int ItemID { get; set; }
}


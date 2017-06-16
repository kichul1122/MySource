using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemInstance
{
	#region Private Variables
	[SerializeField]
	private List<ItemData> itemDataList = new List<ItemData>();
	#endregion

	#region Properties
	public List<ItemData> ItemDataList { get { return itemDataList; } }
	#endregion

	#region Public Events
	public event System.Action<ItemData> AddItemDataEventHandler = delegate { };
	public event System.Action<ItemData> RemoveItemDataEventHandler = delegate { };
	public event System.Action RefreshItemInstanceEventHandler = delegate { };
	#endregion

	#region Public Methods

	public void AddItemData(ItemData itemData)
	{
		itemDataList.Add(itemData);

		AddItemDataEventHandler(itemData);
	}

	public void RemoveItemData(ItemData itemData)
	{
		itemDataList.Remove(itemData);

		RemoveItemDataEventHandler(itemData);
	}

	public void RemoveItemData(int index)
	{
		ItemData itemData = itemDataList[index];
		itemDataList.RemoveAt(index);

		RemoveItemDataEventHandler(itemData);
	}

	public ItemData FindItemData(string name)
	{
		return itemDataList.Find(itemrData => itemrData.Name.Equals(name));
	}
	#endregion
}

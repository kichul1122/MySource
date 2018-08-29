using UnityEngine;
using System.Collections.Generic;
using UniRx;
using System;

[CreateAssetMenu(menuName = "DB/ItemDatabase")]
public class ItemDatabase : ScriptableObject
{
	public ReactiveDictionary<string, ItemData> itemDataDic = new ReactiveDictionary<string, ItemData>();
	//public List<ItemData> itemDataList = new List<ItemData>();

	public void AddItemData(ItemData itemData)
	{
		itemDataDic.Add(itemData.ItemID, itemData);
	}

	#region Create
	public ItemData createItemData;

	//[Button]
	public void AddItemData()
	{
		AddItemData((ItemData)createItemData.Clone());
	}
	#endregion
}

[System.Serializable]
public class ItemData : System.ICloneable
{
	//[ShowInInspector]
	public string ItemID { get { return itemID.Value; } set { itemID.Value = value; } }
	//[ShowInInspector]
	public string Desc { get { return desc.Value; } set { desc.Value = value; } }
	
	[HideInInspector]
	public ReactiveProperty<string> itemID = new ReactiveProperty<string>();
	[HideInInspector]
	public ReactiveProperty<string> desc = new ReactiveProperty<string>();

	public object Clone()
	{
		ItemData itemData = new ItemData();
		itemData.itemID = new ReactiveProperty<string>(this.ItemID);
		itemData.desc = new ReactiveProperty<string>(this.Desc);

		return itemData;
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryController : MonoBehaviour
{
	#region Private Variables
	[SerializeField]
	private Grid grid;
	[SerializeField]
	private InventoryGridItem inventoryGridItemPrefab;

	[SerializeField]
	private List<InventoryGridItem> inventoryGridItemList = new List<InventoryGridItem>();

	private ItemInstance inventoryItems;
	#endregion

	#region Private Methods
	private void OnEnable()
	{
		inventoryItems = UserManager.Instance.User.inventoryItem;
		
		SubscribeToInventoryItems();

		Refresh();
	}

	private void OnDisable()
	{
		inventoryGridItemList.Clear();
		for (int i = 0; i < grid.transform.childCount; ++i)
		{
			Destroy(grid.transform.GetChild(i).gameObject);
		}

		UnSubscribeToInventoryItems();

		inventoryItems = null;
	}

	private void Refresh()
	{
		foreach(ItemData itemData in inventoryItems.ItemDataList)
		{
			AddGridItem(itemData);
		}
	}

	private void AddGridItem(ItemData itemData)
	{
		InventoryGridItem inventoryGridItem = Instantiate(inventoryGridItemPrefab, grid.transform);
		inventoryGridItem.Init(itemData);
		//inventoryGridItem.SubscribeToItemData();

		inventoryGridItemList.Add(inventoryGridItem);

		RefreshGridItem();
	}

	private void RemoveGridItem(ItemData itemData)
	{
		InventoryGridItem inventoryGridItem = inventoryGridItemList.Find(gridItem => gridItem.itemData == itemData);
		//inventoryGridItem.UnSubscribeToItemData();
		inventoryGridItemList.Remove(inventoryGridItem);

		Destroy(inventoryGridItem.gameObject);

		RefreshGridItem();
	}

	private void RefreshGridItem()
	{       
		grid.Reposition(inventoryGridItemList);
	}

	private void SubscribeToInventoryItems()
	{
		inventoryItems.AddItemDataEventHandler += AddGridItem;
		inventoryItems.RemoveItemDataEventHandler += RemoveGridItem;
		inventoryItems.RefreshItemInstanceEventHandler += Refresh;
	}

	private void UnSubscribeToInventoryItems()
	{
		inventoryItems.AddItemDataEventHandler -= AddGridItem;
		inventoryItems.RemoveItemDataEventHandler -= RemoveGridItem;
		inventoryItems.RefreshItemInstanceEventHandler -= Refresh;
	}
	#endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserManager : MonoBehaviour
{
	private static UserManager instance;
	public static UserManager Instance { get { return instance; } }

	public string ID;

	[SerializeField]
	private UserInstance user;
	public UserInstance User { get { return user; } }
	
	private void Awake()
	{
		instance = this;
		LoadUserInstance();
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
		if(user == null)
		{
			CreateUserInstance();
		}
	}

	[ContextMenu("CreateUserInstance")]
	public void CreateUserInstance()
	{
		Debug.Log("CreateUserInstance");
		user = new UserInstance();
	}

	private void OnApplicationQuit()
	{
		SaveUserInstance();
	}

	[ContextMenu("InitData")]
	public void InitData()
	{
		if(user != null)
		{
			ItemInstance inventoryItem = new ItemInstance();
			inventoryItem.AddItemData(new ItemData(10001, "RedPotion", 10));
			inventoryItem.AddItemData(new ItemData(10002, "YellowPotion", 9));
			inventoryItem.AddItemData(new ItemData(10003, "Sword", 101));
			inventoryItem.AddItemData(new ItemData(10004, "Hat", 1068));
			inventoryItem.AddItemData(new ItemData(10005, "Glass", 1080));
			inventoryItem.AddItemData(new ItemData(10005, "Shoes", 1078));

			user.inventoryItem = inventoryItem;
		}
	}

	#region Test   
	private string itemName;

	public void AddRandomGridItem()
	{
		int templateID = Random.Range(10008, 10050);
		User.inventoryItem.AddItemData(new ItemData(templateID, templateID.ToString(), Random.Range(0, 10)));
	}

	public void SetItemName(string name)
	{
		itemName = name;
	}

	public void RemoveRandomGridItem()
	{
		ItemData itemData = User.inventoryItem.FindItemData(itemName);
		if (itemData != null)
		{
			User.inventoryItem.RemoveItemData(itemData);
		}
	}
		
	public void RefreshItemData()
	{
		ItemData itemData = User.inventoryItem.FindItemData(itemName);
		if (itemData != null)
		{
			itemData.Count--;
		}
	}
	#endregion
}

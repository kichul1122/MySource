using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserManager : SingletonPersistant<UserManager>
{
	#region Private Variables
	[SerializeField]
	private string ID;

	[SerializeField]
	private UserInstance user;
	#endregion

	#region Properties
	public UserInstance User { get { return user; } }
	#endregion

	#region Private Methods
	public static int loopCount;
	private void Update()
	{
		loopCount++;
	}
	#endregion

	#region Public Methods
	public void SetID(string ID)
	{
		this.ID = ID;
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
		if (!string.IsNullOrEmpty(ID))
		{
			user = UserInstance.LoadInstance(ID);
			if (user == null)
			{
				CreateUserInstance();
			}

			KC_SceneManager.Instance.sceneController.SetState(KC_SceneController.State.TEST);
		}
	}

	[ContextMenu("CreateUserInstance")]
	public void CreateUserInstance()
	{
		Debug.Log("CreateUserInstance");
		user = new UserInstance();
	}

	public override void SaveState()
	{
		SaveUserInstance();
	}

	[ContextMenu("InitData")]
	public void InitData()
	{
		if (user != null)
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
	#endregion
	
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KC
{
	public class InventoryGridItem : MonoBehaviour
	{
		#region Public Variables
		public ItemData itemData;
		#endregion

		#region Private Variables
		[SerializeField]
		private Text gridItemName;
		[SerializeField]
		private Text count;
		#endregion

		#region Public Methods
		public void Init(ItemData itemData)
		{
			this.itemData = itemData;

			SubscribeToItemData();

			Refresh();
		}
		#endregion

		#region Private Methods
		private void Refresh()
		{
			gridItemName.text = string.Format("Name : {0}", itemData.Name);
			count.text = string.Format("Count : {0}", itemData.Count);
		}

		private void OnDisable()
		{
			UnSubscribeToItemData();
		}

		private void SubscribeToItemData()
		{
			itemData.RefreshItemData += Refresh;
		}

		private void UnSubscribeToItemData()
		{
			itemData.RefreshItemData -= Refresh;
		}
		#endregion
	}

}


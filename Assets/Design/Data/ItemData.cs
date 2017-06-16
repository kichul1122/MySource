using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData : BaseData
{
	#region Private Variables
	[SerializeField]
	private int templateID;
	[SerializeField]
	private string name;
	[SerializeField]
	private int count;
	#endregion


	#region Public Events
	public event System.Action RefreshItemData = delegate { };
	#endregion

	#region Properties
	public int TemplateID { get { return templateID; } set { templateID = value; RefreshItemData(); } }
	public string Name { get { return name; } set { name = value; RefreshItemData(); } }
	public int Count { get { return count; } set { count = value; RefreshItemData(); } }
	#endregion

	#region Public Constructors
	public ItemData() { }
	public ItemData(int templateID, string name, int count)
	{
		this.templateID = templateID;
		this.name = name;
		this.count = count;
	}
	#endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterData : BaseData
{
	#region Private Variables
	[SerializeField]
	public int templateID;
	[SerializeField]
	public string name;
	#endregion

	#region Properties
	public int TemplateID { get { return templateID; } set { templateID = value; } }
	public string Name { get { return name; } set { name = value; } }
	#endregion
}

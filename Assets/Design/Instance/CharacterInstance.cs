using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class CharacterInstance
{
	#region Private Variables
	[SerializeField]
	private List<CharacterData> characterDataList = new List<CharacterData>();
	#endregion

	#region Properties
	public List<CharacterData> CharacterDataList { get { return characterDataList; } }
	#endregion

	#region Public Methods

	public void AddCharacterData(CharacterData characterData)
	{
		characterDataList.Add(characterData);
	}

	public CharacterData FindCharacterData(string name)
	{
		return characterDataList.Find(characterData => characterData.name.Equals(name));
	}
	#endregion
}

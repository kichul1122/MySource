using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/DDList/StringDDList")]
public class StringDDList : SerializedScriptableObject
{
	public Dictionary<long, StringDesignData> designDataDic = new Dictionary<long, StringDesignData>();

	public StringDesignData this[long designID]
	{
		get
		{
			return designDataDic[designID];
		}

		set
		{
			designDataDic.Add(designID, value);
		}
	}
}

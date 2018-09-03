using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Manager/Design")]
public class DesignManager : ScriptableObject
{
	public Dictionary<string, DesignPlayer> characterDic = new Dictionary<string, DesignPlayer>();
	//public Dictionary<string, DesignSkill> skillDic = new Dictionary<string, DesignSkill>();

	//[Button]
	public void Initialize()
	{
#if UNITY_EDITOR
		characterDic = EditorUtil.FindAssetDicOfType<DesignPlayer>();
		//skillDic = EditorUtil.FindAssetDicOfType<DesignSkill>();
#endif
	}
}
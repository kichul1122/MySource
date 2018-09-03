using UnityEngine;

[CreateAssetMenu(menuName = "DB/GameSetting")]
public class GameSetting : ScriptableObject
{
	public Version version;

	[System.Serializable]
	public class Version
	{
		public int major;
		public int minor;
		public int patch;
	}
}
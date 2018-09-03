using UnityEngine;
using System.IO;

[CreateAssetMenu(menuName = "DB/Database")]
public class Database : ScriptableObject
{
	public UserDatabase userDatabase;
	public CharacterDatabase characterDatabase;
	public ItemDatabase itemDatabase;
	
    public void SaveDB()
	{
		// byte[] bytes = SerializationUtility.SerializeValue(saveData, DataFormat.JSON);
		// var jsonString = System.Text.Encoding.UTF8.GetString(bytes);
        // PlayerPrefs.SetString("SaveData", jsonString);

        PlayerPrefs.SetString(userDatabase.ID + "userDatabase", JsonUtility.ToJson(userDatabase));
        PlayerPrefs.SetString(userDatabase.ID + "characterDatabase", JsonUtility.ToJson(characterDatabase));
        PlayerPrefs.SetString(userDatabase.ID + "itemDatabase", JsonUtility.ToJson(itemDatabase));
	}

	public void SaveDBToTextFile()
	{
		File.WriteAllText(Application.streamingAssetsPath + "/userDatabase.txt", JsonUtility.ToJson(userDatabase, true));
		File.WriteAllText(Application.streamingAssetsPath + "/characterDatabase.txt", JsonUtility.ToJson(characterDatabase, true));
		File.WriteAllText(Application.streamingAssetsPath + "/itemDatabase.txt", JsonUtility.ToJson(itemDatabase, true));
	}

	public void LoadDB()
	{
		// var jsonString = PlayerPrefs.GetString("SaveData");
		// var bytes = System.Text.Encoding.UTF8.GetBytes(jsonString);
		// saveData = SerializationUtility.DeserializeValue<SaveData>(bytes, DataFormat.JSON);

        JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(userDatabase.ID + "userDatabase"), userDatabase);
        JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(userDatabase.ID + "characterDatabase"), characterDatabase);
        JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(userDatabase.ID + "itemDatabase"), itemDatabase);
	}

	public void LoadDBFromTextFile()
	{
		JsonUtility.FromJsonOverwrite(File.ReadAllText(Application.streamingAssetsPath + "/userDatabase.txt"), userDatabase);
		JsonUtility.FromJsonOverwrite(File.ReadAllText(Application.streamingAssetsPath + "/characterDatabase.txt"), characterDatabase);
		JsonUtility.FromJsonOverwrite(File.ReadAllText(Application.streamingAssetsPath + "/itemDatabase.txt"), itemDatabase);
	}

	public void Clear()
	{
		PlayerPrefs.DeleteAll();
	}

	public bool HasDB(string ID)
	{
		return !string.IsNullOrEmpty(PlayerPrefs.GetString(ID + "userDatabase")) ? true : false;
	}
}
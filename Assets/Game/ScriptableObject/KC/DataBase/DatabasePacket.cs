using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

[CreateAssetMenu(menuName = "DB/DatabasePacket")]
public class DatabasePacket : ScriptableObject
{
	[SerializeField]
	private Database database;

	public void Login(string ID, System.Action endAction)
	{
		if (database.HasDB(ID))
		{
			database.LoadDB();
			//ConsoleProDebug.LogToFilter("Load DB", "Packet");
		}
		else
		{
			database.userDatabase.ID = ID;
			//케릭터도 생성
			//기본아이템도 생성
			//ConsoleProDebug.LogToFilter("Create DB", "Packet");
			database.SaveDB();
		}

		endAction?.Invoke();
	}
}
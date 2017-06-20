using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonPersistant<T> : Singleton<T> where T : Component
{
	protected override void GameSetup()
	{
		DontDestroyOnLoad(gameObject);
		base.GameSetup();
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KC
{
	public class SingletonPersistant<T> : Singleton<T> where T : UnityEngine.Component
	{
		protected override void GameSetup()
		{
			DontDestroyOnLoad(gameObject);
			base.GameSetup();
		}
	}
}


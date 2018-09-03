using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KC
{
	public class Singleton<T> : MonoBehaviour where T : UnityEngine.Component
	{
		public static T Instance { get; private set; }

		public string TypeName { get; private set; }

		public static bool IsActive
		{
			get { return Instance != null; }
		}

		private void Awake()
		{
			TypeName = typeof(T).FullName;

			if (Instance != null)
			{
				Destroy(gameObject);
				return;
			}

			Instance = this as T;

			GameSetup();
		}

		private void OnDestroy()
		{
			if (Instance == this)
			{
				SaveState();
				GameDestroy();
			}
		}

		protected virtual void OnApplicationQuit()
		{
			SaveState();
		}

		protected virtual void OnApplicationPause(bool pauseStatus)
		{
			SaveState();
		}

		protected virtual void GameSetup()
		{
			gameObject.name = TypeName;
			Debug.Log(TypeName);
		}

		public virtual void SaveState()
		{

		}

		protected virtual void GameDestroy()
		{
		}
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
using UnityEngine.Events;

namespace KC
{
	public class UIBackController : SerializedMonoBehaviour, IUIBack
	{
		//public UIToggle toggle;



		public UnityAction backAction;
		public UnityAction refreshAction;

		public bool isAlive { get; set; }

		public UnityEngine.Object Object
		{
			get
			{
				return gameObject;
			}
		}

		public void OnEnable()
		{
			UIManager.Instance.AddIUIBack(this);
		}

		public void OnDisable()
		{
			//if (!UIManager.IsAlive) return;

			UIManager.Instance.RemoveIUIBack(this);
		}

		public void Back()
		{
			backAction?.Invoke();
		}

		public void Refresh()
		{
			refreshAction?.Invoke();
		}

		public void OnClick_Close()
		{
			//toggle.Set(false, true);
		}
	}

}

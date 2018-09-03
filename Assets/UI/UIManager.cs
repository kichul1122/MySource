using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace KC
{

	public interface IUIBack
	{
		bool isAlive { get; set; }
		Object Object { get; }
		void Back();
		void Refresh();
	}

	public class UIManager : Singleton<UIManager>
	{
		public Camera uiCamera;

		public Transform uiGoldTransform;

		public List<IUIBack> iUIBackList = new List<IUIBack>();

		//public UIApplicationQuitController uiApplicationQuitController;

		public List<Collider> onOffColliderList = new List<Collider>();

		public void Awake()
		{
			//uiCamera = NGUITools.FindCameraForLayer(LayerMask.NameToLayer("UI"));
		}

		public void AddIUIBack(IUIBack iUIBack)
		{
			if (!iUIBackList.Exists(_iUIBack => _iUIBack.Object == iUIBack.Object))
			{
				iUIBack.isAlive = true;
				iUIBackList.Add(iUIBack);
			}
		}

		public void RemoveIUIBack(IUIBack iUIBack)
		{
			if (iUIBack.isAlive && iUIBackList.Exists(_iUIBack => _iUIBack.Object == iUIBack.Object))
			{
				iUIBack.isAlive = false;
				iUIBackList.Remove(iUIBack);
			}
		}

		public void PopIUIBack()
		{
			if (iUIBackList.Count > 0)
			{
				IUIBack iUIBack = iUIBackList[iUIBackList.Count - 1];
				iUIBack?.Back();

				if (iUIBack.isAlive)
				{
					iUIBack.isAlive = false;
					iUIBackList.Remove(iUIBack);
				}

				if (iUIBackList.Count > 0)
				{
					iUIBackList[iUIBackList.Count - 1].Refresh();
				}
			}
			else
			{
				//uiApplicationQuitController.gameObject.SetActive(true);
			}
		}

		public void ClearIUI()
		{
			for (int i = iUIBackList.Count - 1; i >= 0; --i)
			{
				IUIBack iUIBack = iUIBackList[i];
				iUIBack?.Back();

				if (iUIBack.isAlive)
				{
					iUIBack.isAlive = false;
					iUIBackList.Remove(iUIBack);
				}
			}
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Escape) /*&& !UICamera.ignoreAllEvents*/)
			{
				PopIUIBack();
			}
		}

		public void UpdateCollider(bool isOnOff)
		{
			foreach (Collider coll in onOffColliderList)
			{
				coll.enabled = isOnOff;
			}
		}
	}

}
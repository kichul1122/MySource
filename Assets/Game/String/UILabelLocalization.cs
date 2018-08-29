///////////////////
///KC_3D PROJECT///
///////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class UILabelLocalization : MonoBehaviour
{
	#region Private Variables
	//private UILabel label;
	#endregion

	#region Public Variables
	public string key;
	#endregion

	#region Properties
	#endregion

	#region Unity Methods
	private void Awake()
	{
		//label = GetComponent<UILabel>();
	}

	private void Start()
	{
		StringManager.Instance.RefreshStringEventHandler += SetText;
	}

	private void OnEnable()
	{
		SetText();
	}
	#endregion

	#region Interfaces
	#endregion

	#region Public Methods
	#endregion

	#region Private Methods

	public void SetText()
	{
		//if (this.label)
		//{
		//	if(string.IsNullOrEmpty(key))
		//	{
		//		key = this.label.text;
		//	}

		//	this.label.text = StringManager.Instance.GetString(key);
		//}
	}

	#endregion
}

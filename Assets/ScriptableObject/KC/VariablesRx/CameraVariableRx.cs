using System.Collections;
using System.Collections.Generic;
//using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "VariablesRx/Camera")]
public class CameraVariableRx : ScriptableObject
{
	[HideInInspector]
	public ReactiveProperty<Camera> AsObservable = new ReactiveProperty<Camera>();

	//[ShowInInspector]
	public Camera Value
	{
		get
		{
			return AsObservable.Value;
		}

		set
		{
			this.AsObservable.Value = value;
		}
	}
}
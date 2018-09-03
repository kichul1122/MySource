using System.Collections;
using System.Collections.Generic;
//using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using UniRx.Triggers;
using System;

[CreateAssetMenu(menuName = "VariablesRx/Bool")]
public class BoolVariableRx : ScriptableObject
{
	[HideInInspector]
	public ReactiveProperty<bool> AsObservable = new ReactiveProperty<bool>();

	//[ShowInInspector]
	public bool Value
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
using System.Collections;
using System.Collections.Generic;
//using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "VariablesRx/String")]
public class StringVariableRx : ScriptableObject
{
	[HideInInspector]
	public ReactiveProperty<string> AsObservable = new ReactiveProperty<string>();

	//[ShowInInspector]
	public string Value
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
using System.Collections;
using System.Collections.Generic;
//using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using UniRx.Triggers;
using System;

[CreateAssetMenu(menuName = "VariablesRx/Int")]
public class IntVariableRx : ScriptableObject
{
	[HideInInspector]
	public ReactiveProperty<int> AsObservable = new ReactiveProperty<int>();

	//[ShowInInspector]
	public int Value
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
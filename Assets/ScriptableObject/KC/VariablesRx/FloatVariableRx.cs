using System.Collections;
using System.Collections.Generic;
//using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "VariablesRx/Float")]
public class FloatVariableRx : ScriptableObject
{
	[HideInInspector]
	public ReactiveProperty<float> AsObservable = new ReactiveProperty<float>();

	//[ShowInInspector]
	public float Value
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
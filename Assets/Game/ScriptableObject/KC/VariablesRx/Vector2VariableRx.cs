using System.Collections;
using System.Collections.Generic;
//using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "VariablesRx/Vector2")]
public class Vector2VariableRx : ScriptableObject
{
	[HideInInspector]
	public ReactiveProperty<Vector2> AsObservable = new ReactiveProperty<Vector2>();

	//[ShowInInspector]
	public Vector2 Value
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
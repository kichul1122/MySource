using System.Collections;
using System.Collections.Generic;
//using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "VariablesRx/Vector3")]
public class Vector3VariableRx : ScriptableObject
{
	[HideInInspector]
	public ReactiveProperty<Vector3> AsObservable = new ReactiveProperty<Vector3>();

	//[ShowInInspector]
	public Vector3 Value
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
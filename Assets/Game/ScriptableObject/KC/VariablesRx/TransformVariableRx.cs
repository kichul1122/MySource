using System.Collections;
using System.Collections.Generic;
//using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "VariablesRx/Transform")]
public class TransformVariableRx : ScriptableObject
{
	[HideInInspector]
	public ReactiveProperty<Transform> AsObservable = new ReactiveProperty<Transform>();

	//[ShowInInspector]
	public Transform Value
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
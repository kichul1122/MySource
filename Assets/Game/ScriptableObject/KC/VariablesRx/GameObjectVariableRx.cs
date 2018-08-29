using System.Collections;
using System.Collections.Generic;
//using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "VariablesRx/GameObject")]
public class GameObjectVariableRx : ScriptableObject
{
	[HideInInspector]
	public ReactiveProperty<GameObject> AsObservable = new ReactiveProperty<GameObject>();

	//[ShowInInspector]
	public GameObject Value
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
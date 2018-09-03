using System.Collections;
using System.Collections.Generic;
//using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "VariablesRx/Trigger")]
public class TriggerVariableRx : ScriptableObject
{
	[HideInInspector]
	private Subject<Unit> onTrigger = new Subject<Unit>();

	public IObservable<Unit> AsObservable => onTrigger;

	//[Button]
	public void Invoke()
	{
		onTrigger.OnNext(Unit.Default);
	}
}
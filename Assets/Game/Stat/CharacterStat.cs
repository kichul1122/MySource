using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UniRx;
//using Sirenix.OdinInspector;

[Serializable]
public class CharacterStat
{
	[UnityEngine.HideInInspector]
	public ReactiveProperty<float> AsObservable = new ReactiveProperty<float>();

	private readonly List<StatModifier> statModifiers;

	//[ShowInInspector]
	public float Value
	{
		get
		{
			return AsObservable.Value;
		}
	}

	public CharacterStat()
	{
		statModifiers = new List<StatModifier>();
		UpdateValue();
	}

	public CharacterStat(StatModifier mod) : this()
	{
		AddModifier(mod);
	}

	public void AddModifier(StatModifier mod)
	{
		statModifiers.Add(mod);
		statModifiers.Sort(CompareModifierOrder);
		UpdateValue();
	}

	public bool RemoveModifier(StatModifier mod)
	{
		if (statModifiers.Remove(mod))
		{
			UpdateValue();
			return true;
		}
		return false;
	}

	public bool RemoveAllModifiersFromSource(object source)
	{
		bool didRemove = false;

		for (int i = statModifiers.Count - 1; i >= 0; i--)
		{
			if (statModifiers[i].Source == source)
			{
				didRemove = true;
				statModifiers.RemoveAt(i);
			}
		}

		if (didRemove)
		{
			UpdateValue();
		}

		return didRemove;
	}

	private int CompareModifierOrder(StatModifier a, StatModifier b)
	{
		if (a.Order < b.Order)
			return -1;
		else if (a.Order > b.Order)
			return 1;
		return 0;
	}

	private float CalculateFinalValue()
	{
		float finalValue = 0;
		float sumPercentAdd = 0;

		for (int i = 0; i < statModifiers.Count; i++)
		{
			StatModifier mod = statModifiers[i];

			if (mod.Type == StatModType.Flat)
			{
				finalValue += mod.Value;
			}
			else if (mod.Type == StatModType.PercentAdd)
			{
				sumPercentAdd += mod.Value;

				if (i + 1 >= statModifiers.Count || statModifiers[i + 1].Type != StatModType.PercentAdd)
				{
					finalValue *= 1 + sumPercentAdd;
					sumPercentAdd = 0;
				}
			}
			else if (mod.Type == StatModType.PercentMult)
			{
				finalValue *= 1 + mod.Value;
			}
		}

		return (float)Math.Round(finalValue, 4);
	}

	private void UpdateValue()
	{
		AsObservable.Value = CalculateFinalValue();
	}
}

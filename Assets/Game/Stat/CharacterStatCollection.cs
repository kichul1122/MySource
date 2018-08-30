using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Sirenix.Serialization;

public class CharacterStatCollection
{
	[OdinSerialize]
	private Dictionary<EStat, CharacterStat> statDic = new Dictionary<EStat, CharacterStat>();

	public CharacterStat GetCharacterStat(EStat eStat)
	{
		CharacterStat characterStat;
		if(statDic.TryGetValue(eStat, out characterStat))
		{
			return characterStat;
		}

		return new CharacterStat();
	}

	public void Add(StatCollection statCollection, object source)
	{
		foreach (var stat in statCollection.statList)
		{
			Add(stat.eStat, new StatModifier(stat.value, stat.type, source));
		}
	}

	public void Add(EStat eStat, StatModifier statModifier)
	{
		CharacterStat characterStat;
		if (statDic.TryGetValue(eStat, out characterStat))
		{
			characterStat.AddModifier(statModifier);
		}
		else
		{
			statDic.Add(eStat, new CharacterStat(statModifier));
		}
	}

	public void Remove(StatCollection statCollection, object source)
	{
		foreach (var stat in statCollection.statList)
		{
			Remove(stat.eStat, source);
		}
	}

	public void Remove(EStat eStat, object source)
	{
		CharacterStat characterStat;
		if (statDic.TryGetValue(eStat, out characterStat))
		{
			characterStat.RemoveAllModifiersFromSource(source);
		}
	}
}
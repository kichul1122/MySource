using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Stat
{
	public EStat eStat;
	public float value;

	[EnumToggleButtons]
	public StatModType type = StatModType.Flat;
}
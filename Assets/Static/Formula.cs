using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Formula
{
	//public static Damage GetDamage(CharacterStatCollection characterStatCollection)
	//{
	//	float damage = characterStatCollection.GetCharacterStat(EStat.Attack).Value * (1 + characterStatCollection.GetCharacterStat(EStat.Str).Value * 0.01f);
	//	float randomValue = Random.Range(0.8f, 1.2f);
	//	float averageDamage = damage * randomValue;

	//	bool isCritical = characterStatCollection.GetCharacterStat(EStat.CriticalChance).Value != 0 ? Random.value <= characterStatCollection.GetCharacterStat(EStat.CriticalChance).Value : false;

	//	if (isCritical)
	//	{
	//		averageDamage *= (1 + characterStatCollection.GetCharacterStat(EStat.CriticalDamage).Value);
	//	}

	//	averageDamage = Mathf.Round(Mathf.Clamp(averageDamage, 1, averageDamage));
		
	//	bool isGetHit = isCritical && randomValue >= 1.1f;

	//	return new Damage((long)averageDamage, isCritical : isCritical, isGetHit : isGetHit);
	//}

	//public static Damage GetDamageByPosition(CharacterStatCollection characterStatCollection, Vector3 position)
	//{
	//	Damage damage = GetDamage(characterStatCollection);
	//	damage.position = position;

	//	return damage;
	//}
}
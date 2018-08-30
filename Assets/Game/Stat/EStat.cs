using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EStat
{
	Str,    //공격력 주스탯
	Attack, //공격력		
	Def,    //방어력
	MaxHp,	//전체생명력

	AttackSpeed,//공속
	MoveSpeed,  //이속

	LifePerHit,//적중당 생명력회복

	CooldownReduction,//재사용대기시간감소
	WhirlWindDamage,	//휠윈드피해

	CriticalChance,//극대화확률
	CriticalDamage,//극댇화피해

	MagicFind,//매직찬스(마법아이템발견)
	GoldFind,//골드발견 
	BonusExperience, //경험치발견

}
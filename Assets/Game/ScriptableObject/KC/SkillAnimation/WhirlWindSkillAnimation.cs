using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

[CreateAssetMenu(menuName = "SkillAnimation/WhirlWind")]
public class WhirlWindSkillAnimation : SkillAnimation 
{
	public override void SetParameter(Animator animator)
	{
		animator.SetTrigger("WhirlWindTrigger");
	}
}

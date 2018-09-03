using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

[CreateAssetMenu(menuName = "SkillAnimation/Roll")]
public class RollSkillAnimation : SkillAnimation 
{
	public override void SetParameter(Animator animator)
	{
		animator.SetTrigger("RollTrigger");
	}
}

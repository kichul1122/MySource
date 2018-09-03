using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

[CreateAssetMenu(menuName = "SkillAnimation/Attack")]
public class CleaveSkillAnimation : SkillAnimation 
{
	public override void SetParameter(Animator animator)
	{
		animator.SetInteger("AttackAction", Random.Range(0, 10));
		animator.SetTrigger("AttackTrigger");
	}
}

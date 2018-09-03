using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

[System.Flags]
public enum EEvent
{
	None = 0,
	OnClick = 1,
	OnPress = 2,
	OnRelese = 4,
}

public abstract class SkillAnimation : ScriptableObject
{
	public EEvent eEvent = EEvent.OnClick;

	public abstract void SetParameter(Animator animator);
	
	public void OnClick(Animator animator)
	{
		if(eEvent.HasFlag(EEvent.OnClick))
		{
			SetParameter(animator);
		}
	}

	public void OnPressing(Animator animator)
	{
		if (eEvent.HasFlag(EEvent.OnPress))
		{
			SetParameter(animator);
		}
	}

	public void OnRelease(Animator animator)
	{
		if (eEvent.HasFlag(EEvent.OnRelese))
		{
			//Reset(animator);
		}
	}
}

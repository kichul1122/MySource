using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(CharacterController))]
public class CharacterFSM : StateMachineBehaviourEx<CharacterFSM.CharacterState> 
{
	public enum CharacterState 
	{
		Idle,
		Moving,
		Attacking
	}

	void Start() 
	{
		activeState = CharacterState.Idle;
	}

	#region Idle
	void Idle_EnterState()
	{
        
	}

	void Idle_Update()
	{
	}
    
	void Idle_ExitState()
	{
	}
	#endregion

	#region Moving
	void Moving_EnterState()
	{
	}

	void Moving_Update()
	{
    }

	void Moving_ExitState()
	{
	}
	#endregion
}

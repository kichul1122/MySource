using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMController : FSM<FSMController.FSMState>
{
	public enum FSMState
	{
		IDLE,
		ATTACK,
		PATROL,
		MAX,
	}

	private void Start()
	{
		ActiveCurrentState = FSMState.IDLE;
	}

	#region IDLE
	IEnumerator IDLE_EnterState()
	{
		Debug.Log("IDLE_EnterState " + UserManager.loopCount);

		yield break;

	}

	void IDLE_UpdateState()
	{
		Debug.Log("IDLE_UpdateState " + UserManager.loopCount);

		if (Input.GetKeyDown(KeyCode.Space))
		{
			SetState(FSMState.ATTACK); return;
		}
	}

	IEnumerator IDLE_ExitState()
	{
		Debug.Log("IDLE_ExitState " + UserManager.loopCount);

		yield break;
	}
	#endregion

	#region ATTACK
	IEnumerator ATTACK_EnterState()
	{
		Debug.Log("ATTACK_EnterState " + UserManager.loopCount);
		yield break;
	}

	void ATTACK_UpdateState()
	{
		Debug.Log("ATTACK_UpdateState " + UserManager.loopCount);
	}

	IEnumerator ATTACK_ExitState()
	{
		Debug.Log("ATTACK_ExitState " + UserManager.loopCount);
		yield break;
	}
	#endregion

}

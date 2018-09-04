using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KC
{
	public class FSMController : FSM<FSMController.FSMStates>
	{
		public enum FSMStates
		{
			IDLE,
			ATTACK,
			PATROL,
			MAX,
		}

		private void Start()
		{
			ActiveCurrentState = FSMStates.IDLE;
		}

		#region IDLE
		IEnumerator IDLE_Enter()
		{
			yield return new WaitForSeconds(0.2f);

			Debug.Log("IDLE_EnterState " + UserManager.loopCount);
		}

		void IDLE_Update()
		{

			if (Input.GetKeyDown(KeyCode.Space))
			{
				SetState(FSMStates.ATTACK); return;
			}

			Debug.Log("IDLE_UpdateState " + UserManager.loopCount);
		}

		void IDLE_Exit()
		{
			Debug.Log("IDLE_ExitState " + UserManager.loopCount);
		}
		#endregion

		#region ATTACK 
		void ATTACK_Enter()
		{
			Debug.Log("ATTACK_EnterState " + UserManager.loopCount);
		}

		void ATTACK_Update()
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				SetState(FSMStates.IDLE); return;
			}

			Debug.Log("ATTACK_UpdateState " + UserManager.loopCount);
		}

		IEnumerator ATTACK_Exit()
		{
			Debug.Log("ATTACK_ExitState " + UserManager.loopCount);
			yield return new WaitForSeconds(0.2f);
		}
		#endregion

	}

}

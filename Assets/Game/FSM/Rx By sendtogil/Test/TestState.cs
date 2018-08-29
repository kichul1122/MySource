using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Sirenix.OdinInspector;
using UniRx;
using UniRx.Triggers;

public class TestState : MonoBehaviour 
{
	public enum EState
	{
		Idle,
		Attack,
		Die
	}

	public StateMachine<EState> stateMachine;

	private void Awake()
	{
		//stateMachine = StateMachine<EState>.Initialize(this, EState.Idle);

		//stateMachine.SetMode(StateTransition.Overwrite);
		//stateMachine.SetMode(StateTransition.Safe);
		//stateMachine.SetMode(StateTransition.Blend);

		//stateMachine.SetDuration(0.5f, 0.5f);

		//stateMachine.Changed += o => Debug.Log("State Changed : " + o.ToString());

		//stateMachine.AddSubscriber(this);

		//stateMachine.ChangeState(EState.Attack);
		//stateMachine.ChangeState(EState.Attack, StateTransition.Blend);
		//stateMachine.ChangeState(EState.Attack, StateTransition.Safe, 0.5f, 0.5f);
		//stateMachine.CancelTransition();
	}
}

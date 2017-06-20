using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;

public class FSM<T> : FSM
{
    public T ActiveCurrentState
    {
        get
        {
            return (T)CurrentState;
        }
        set
        {
            CurrentState = value;
        }
    }

    public T ActiveLastState
    {
        get
        {
            return (T)LastState;
        }
    }
}

public class FSM : MonoBehaviour
{
    public class FSMState
    {
        public object currentState = null;
        public object lastState = null;

        public Action enterState = DoNothing;
        public Action exitState = DoNothing;

		public Func<IEnumerator> enterStateCoroutine = DoNothingCoroutine;
		public Func<IEnumerator> exitStateCoroutine = DoNothingCoroutine;

		public Action DoUpdate = DoNothing;
        public Action DoLateUpdate = DoNothing;
        public Action DoFixedUpdate = DoNothing;

		//public Action<Collision> DoOnCollisitonEnter = DoNothingCollision;
		//public Action<Collider> DoOnTriggerEnter = DoNothingTrigger;
	}

	private FSMState state = new FSMState();
	private Coroutine coroutine = null;

    public object CurrentState
    {
        get
        {
            return state.currentState;
        }
        set
        {
            state.lastState = value;
            state.currentState = value;

            ConfigureCurrentState();
        }
    }

    public object LastState
    {
        get
        {
            return state.lastState;
        }
    }

    public void SetState(object stateToActivate)
    {
        if (!stateToActivate.Equals(state.currentState))
        {
            state.lastState = state.currentState;
            state.currentState = stateToActivate;

            ConfigureCurrentState();
        }
    }

    void ConfigureCurrentState()
    {
		if(coroutine != null)
		{
			StopCoroutine(coroutine);
		}

		coroutine = StartCoroutine(ConfigureCurretStateRoutine());
    }

	IEnumerator ConfigureCurretStateRoutine()
	{
		GetUpdateStateDoNothingMethods();

		if (state.exitState != DoNothing)
		{
			state.exitState();
		}

		if (state.exitStateCoroutine != DoNothingCoroutine)
		{
			yield return StartCoroutine(state.exitStateCoroutine());
		}

		GetEnterExitStateMethods();
		
		if (state.enterState != DoNothing)
		{
			state.enterState();
		}

		if (state.enterStateCoroutine != DoNothingCoroutine)
		{
			yield return StartCoroutine(state.enterStateCoroutine());
		}

		GetUpdateStateMethods();
	}

    void GetEnterExitStateMethods()
    {
		if(IsReturnTypeCoroutine("Enter"))
		{
			state.enterStateCoroutine = CreateDelegate<Func<IEnumerator>>("Enter", DoNothingCoroutine);
			state.enterState = DoNothing;
		}
		else
		{
			state.enterStateCoroutine = DoNothingCoroutine;
			state.enterState = CreateDelegate<Action>("Enter", DoNothing);
		}

		if (IsReturnTypeCoroutine("Exit"))
		{
			state.exitStateCoroutine = CreateDelegate<Func<IEnumerator>>("Exit", DoNothingCoroutine);
			state.exitState = DoNothing;
		}
		else
		{
			state.exitStateCoroutine = DoNothingCoroutine;
			state.exitState = CreateDelegate<Action>("Exit", DoNothing);
		}
    }

	void GetUpdateStateDoNothingMethods()
	{
		state.DoFixedUpdate = DoNothing;
		state.DoUpdate = DoNothing;
		state.DoLateUpdate = DoNothing;

	}
	void GetUpdateStateMethods()
	{
		state.DoFixedUpdate = CreateDelegate<Action>("FixedUpdate", DoNothing);
		state.DoUpdate = CreateDelegate<Action>("Update", DoNothing);
		state.DoLateUpdate = CreateDelegate<Action>("LateUpdate", DoNothing);
	}

	Dictionary<object, Dictionary<string, Delegate>> dicCache = new Dictionary<object, Dictionary<string, Delegate>>();

	bool IsReturnTypeCoroutine(string methodRoot)
	{
		MethodInfo methodInfo = GetType().GetMethod(state.currentState.ToString() + "_" + methodRoot,
				System.Reflection.BindingFlags.Instance
				| System.Reflection.BindingFlags.Public
				| System.Reflection.BindingFlags.NonPublic
				| System.Reflection.BindingFlags.DeclaredOnly);

		return methodInfo.ReturnType == typeof(IEnumerator);
	}

    T CreateDelegate<T>(string methodRoot, T doNothing) where T : class
    {
        Dictionary<string, Delegate> dicLookup;
        if (!dicCache.TryGetValue(state.currentState, out dicLookup))
        {
            dicCache[state.currentState] = dicLookup = new Dictionary<string, Delegate>();
        }

        Delegate returnValue;
        if (!dicLookup.TryGetValue(methodRoot, out returnValue))
        {
            MethodInfo methodInfo = GetType().GetMethod(state.currentState.ToString() + "_" + methodRoot, 
				System.Reflection.BindingFlags.Instance
                | System.Reflection.BindingFlags.Public 
				| System.Reflection.BindingFlags.NonPublic 
				| System.Reflection.BindingFlags.DeclaredOnly);

            if (methodInfo != null)
            {
                returnValue = Delegate.CreateDelegate(typeof(T), this, methodInfo);
			}
            else
            {
                returnValue = doNothing as Delegate;
            }

            dicLookup[methodRoot] = returnValue;
        }

		return returnValue as T;
    }

	#region Methods
	private void FixedUpdate()
	{
		state.DoFixedUpdate();
	}

	private void Update()
    {
        state.DoUpdate();
    }

	private void LateUpdate()
	{
		state.DoLateUpdate();
	}

	//private void OnCollisionEnter(Collision collision)
	//{
	//	state.DoOnCollisitonEnter
	//}

	//private void OnTriggerEnter(Collider other)
	//{
	//	state.DoOnTriggerEnter
	//}

	static void DoNothing()
    {
    }

	static IEnumerator DoNothingCoroutine()
	{
		yield break;
	}

	static void DoNothingCollision(Collision collision)
	{
	}

	static void DoNothingTrigger(Collider collider)
	{
	}
	#endregion
}
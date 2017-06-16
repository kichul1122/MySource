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
    public class State
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
    }

    private State state = new State();
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
		if(IsReturnTypeCoroutine("EnterState"))
		{
			state.enterStateCoroutine = CreateDelegate<Func<IEnumerator>>("EnterState", DoNothingCoroutine);
		}
		else
		{
			state.enterState = CreateDelegate<Action>("EnterState", DoNothing);
		}

		if (IsReturnTypeCoroutine("ExitState"))
		{
			state.enterStateCoroutine = CreateDelegate<Func<IEnumerator>>("ExitState", DoNothingCoroutine);
		}
		else
		{
			state.enterState = CreateDelegate<Action>("ExitState", DoNothing);
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
		state.DoUpdate = CreateDelegate<Action>("UpdateState", DoNothing);
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
	void FixedUpdate()
	{
		state.DoFixedUpdate();
	}

	void Update()
    {
        state.DoUpdate();
    }

	void LateUpdate()
	{
		state.DoLateUpdate();
	}

	static void DoNothing()
    {
    }

	static IEnumerator DoNothingCoroutine()
	{
		yield break;
	}
    #endregion
}
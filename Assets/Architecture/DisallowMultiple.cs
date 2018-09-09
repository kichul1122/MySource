using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[DisallowMultipleComponent]
public abstract class ATest1 : SerializedMonoBehaviour
{
	public abstract void TestA();
}

public class DisallowMultiple : ATest1 
{
	[Button]
	public override void TestA()
	{
		Debug.Log("Wow DisallowMultiple class");
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

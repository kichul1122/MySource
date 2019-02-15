# Unity

[바람직한-유니티-코딩방법](http://ronniej.sfuh.tk/%EB%B0%94%EB%9E%8C%EC%A7%81%ED%95%9C-%EC%9C%A0%EB%8B%88%ED%8B%B0-%EC%BD%94%EB%94%A9%EB%B0%A9%EB%B2%95-good-coding-practices-unity-%EB%B2%88%EC%97%AD/)

[Unity Scripting강좌](https://unity3d.com/kr/learn/tutorials/modules/beginner/live-training-archive/scriptable-objects)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kichul2 : MonoBehaviour 
{
	private Coroutine coroutine;
	private IEnumerator qwer;
	private void Start()
	{
		//2번 경우
		//qwer = asdf(9999, 343434);
	}
	public void Update()
	{
		//1번 경우 : 멀티실행 O , 멀티종료 O, 인자값변경 O
		//if (Input.GetKeyDown(KeyCode.A))
		//{
		//	StartCoroutine("asdf", 123, 05050);
		//}

		//if (Input.GetKeyDown(KeyCode.C))	//여러개를 동시에 끔
		//{
		//	StopCoroutine("asdf");
		//}

		//2번 경우 : 멀티실행 O , 멀티종료 O, 인자값변경 X
		//if (Input.GetKeyDown(KeyCode.A))
		//{
		//	StartCoroutine(qwer);
		//}

		//if (Input.GetKeyDown(KeyCode.C))
		//{
		//	StopCoroutine(qwer);
		//}

		//3번 경우 //멀티 실행하려면 List로 관리, 멀티실행O(마지막으로 저장된 하나만 종료가능), 멀티종료X, 인자값변경 O
		//if (Input.GetKeyDown(KeyCode.A))
		//{
		//	coroutine = StartCoroutine(asdf(126463, 00099));
		//}

		//if (Input.GetKeyDown(KeyCode.C))
		//{
		//	if (coroutine != null)
		//	{
		//		StopCoroutine(coroutine);
		//		coroutine = null;
		//	}
		//}
	}

	private IEnumerator asdf(int asdf, int vcvv)
	{
		while(true)
		{
			Debug.Log("Coroutine2" + asdf + " " + vcvv);
			yield return null;
		}
	}
}

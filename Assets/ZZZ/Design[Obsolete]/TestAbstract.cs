using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KC
{
	interface IAb
	{
		void TestA();
	}

	interface IAbc
	{
		void TestB();
	}

	public abstract class TestAbstract2 : MonoBehaviour, IAb, IAbc
	{
		public abstract void TestA();
		public abstract void TestB();

		private void Start()
		{
			LinkedList<IAb> list;
			
		}
	}

	// public class TestSub : MonoBehaviour
	// {
	// 	private TestAbstract2 testAbstract;

	// 	private void Start()
	// 	{
	// 		testAbstract = GetComponent<TestAbstract2>();
	// 	}
	// }
}


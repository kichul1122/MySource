using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Linq : MonoBehaviour
{
	private void Start()
	{
		int[] numbers = { 1, 2, 3, 4, 5, 6 };

		var result = from n in numbers
					 where n % 2 == 0
					 orderby n
					 select n;

		foreach(int n in result)
		{
			Debug.Log("짝수 : " + n);
		}
	}


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KC_LayerMask : MonoBehaviour
{
	public LayerMask layerMask = -1;	//1111 1111 1111 1111 1111 1111 1111 1111

	private void Start()
	{
		//layerMask = LayerMask.GetMask("Player", "Enemy");
		//layerMask = 1 << 9 | 1 << 10;
		//layerMask = 1 << LayerMask.NameToLayer("Player") | 1 << LayerMask.NameToLayer("Enemy");

		//Player 9, Enemy 10
		Debug.Log(LayerMask.GetMask("Player", "Enemy"));
		//result : 2^9 + 2^10 = 1536

		Debug.Log(LayerMask.NameToLayer("Player"));//bit index
		Debug.Log(LayerMask.NameToLayer("Enemy"));//bit index

		Debug.Log(LayerMask.LayerToName(9));//bit index
		Debug.Log(LayerMask.LayerToName(10));//bit index
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KC_Raycast : MonoBehaviour
{

	private void Start()
	{

	}

	private void Update()
	{
		Show2D();
	}

	#region 2D
	[Header("2D")]
	public Vector2 origin2D;
	public Vector2 direction2D;
	public float distance;
	public LayerMask layerMask;

	public void Show2D()
	{
		direction2D.Normalize();
		RaycastHit2D raycastHit2D;
		//raycastHit2D = Physics2D.Raycast(origin2D, direction2D);
		//Debug.DrawLine(origin2D, origin2D + direction2D * Mathf.Infinity);

		//raycastHit2D = Physics2D.Raycast(origin2D, direction2D, distance);
		//Debug.DrawLine(origin2D, origin2D + direction2D * distance);

		//layerMask = LayerMask.GetMask("Player", "Enemy");
		//layerMask = 1 << 9 | 1 << 10;
		//layerMask = 1 << LayerMask.NameToLayer("Player") | 1 << LayerMask.NameToLayer("Enemy");
		raycastHit2D = Physics2D.Raycast(origin2D, direction2D, distance, layerMask);
		Debug.DrawLine(origin2D, origin2D + direction2D * distance);

		if (raycastHit2D)
		{
			Debug.Log("raycastHit2D");
		}
	}
	#endregion

	[Header("3D")]
	public Vector3 origin3D;
	public Vector3 direction3D;
	public void Show3D()
	{
		//Physics.Raycast
	}
}
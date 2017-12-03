using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoordinateSystem : MonoBehaviour
{
	private void Update()
	{
		Debug.Log("World2Screen" + Camera.main.WorldToScreenPoint(transform.position));
		Debug.Log("World2ViewPoint" + Camera.main.WorldToViewportPoint(transform.position));
	}

}

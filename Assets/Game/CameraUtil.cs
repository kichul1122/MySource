using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraUtil : MonoBehaviour
{
	private void Start()
	{
		Debug.Log(GetScreenRect2D(Camera.main));
	}

	//2D 카메라 영역(position : leftBottom)
	public static Rect GetScreenRect2D(Camera cam)
	{
		float height = 2f * cam.orthographicSize;
		float width = height * cam.aspect;
		return new Rect(cam.transform.position.x - width / 2f, cam.transform.position.y - height / 2f, width, height);
	}
}

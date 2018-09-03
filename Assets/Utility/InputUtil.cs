using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Sirenix.OdinInspector;

public static class InputUtils
{
	public static Vector3 InputToWorldPosition(Vector2 inputPos)
	{
		Vector3 pos = new Vector3(inputPos.x, inputPos.y,
				-Camera.main.transform.position.z);
		return Camera.main.ScreenToWorldPoint(pos);
	}

	static bool WasMouseDown(int button)
	{
		return Input.GetMouseButtonDown(button);
	}

	static bool WasFingerDown()
	{
		foreach (Touch t in Input.touches)
		{
			if (t.phase == TouchPhase.Began)
				return true;
		}
		return false;
	}

	static Vector2 FirstTouchPosition()
	{
		foreach (Touch t in Input.touches)
		{
			if (t.phase == TouchPhase.Began || t.phase == TouchPhase.Moved
					|| t.phase == TouchPhase.Stationary)
				return t.position;
		}
		throw new System.InvalidOperationException("No touch exists.");
		//return Vector2.zero;
	}

	public static bool MouseDownOrTap()
	{
#pragma warning disable 0162
#if UNITY_EDITOR
		return WasMouseDown(0);
#endif
#if UNITY_IPHONE || UNITY_ANDROID
		return WasFingerDown();
#endif
		return WasMouseDown(0);
#pragma warning restore 0162
	}

	public static bool MouseOrTapMoved()
	{
#pragma warning disable 0162
#if UNITY_EDITOR
		return Input.GetMouseButton(0);
#endif
#if UNITY_IPHONE || UNITY_ANDROID
		foreach (Touch t in Input.touches)
		{
			if (t.phase == TouchPhase.Began || t.phase == TouchPhase.Moved
					|| t.phase == TouchPhase.Stationary)
				return true;
		}
		return false;
#endif
		return Input.GetMouseButton(0);
#pragma warning restore 0162
	}

	public static Vector2 MouseOrTapPosition()
	{
#pragma warning disable 0162
#if UNITY_EDITOR
		return Input.mousePosition;
#endif
#if UNITY_IPHONE || UNITY_ANDROID
		return FirstTouchPosition();
#endif
		return Input.mousePosition;
#pragma warning restore 0162
	}

	public static bool MouseOrTapUp()
	{
#pragma warning disable 0162
#if UNITY_EDITOR
		return Input.GetMouseButtonUp(0);
#endif
#if UNITY_IPHONE || UNITY_ANDROID
		foreach (Touch t in Input.touches)
		{
			if (t.phase == TouchPhase.Ended || t.phase == TouchPhase.Canceled)
				return true;
		}
		return false;
#endif
		return Input.GetMouseButtonUp(0);
#pragma warning restore 0162
	}

}
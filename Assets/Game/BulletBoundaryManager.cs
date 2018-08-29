using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using KC;

public class BulletBoundaryManager : Singleton<BulletBoundaryManager>
{
	public new Camera camera;

	public Transform lineTransform;

	[Range(0, 1)]
	public float topPercent;
	public Color lineColor;

	[ShowInInspector]
	public float Bottom
	{
		get { return -ExtentHeight; }
	}

	[ShowInInspector]
	public float Top
	{
		get { return ExtentHeight - (ExtentHeight * 2 * (1 - topPercent)); }
	}

	[ShowInInspector]
	public float Left
	{
		get { return -ExtentWidth; }
	}

	[ShowInInspector]
	public float Right
	{
		get { return ExtentWidth; }
	}

	[ShowInInspector]
	public float ExtentHeight
	{
		get { return camera.orthographicSize; }
	}

	[ShowInInspector]
	public float Height
	{
		get { return ExtentHeight * 2.0f - (ExtentHeight * 2 * (1 - topPercent)); }
	}

	[ShowInInspector]
	public float ExtentWidth
	{
		get { return camera.aspect * camera.orthographicSize; }
	}

	[ShowInInspector]
	public float Width
	{
		get { return ExtentWidth * 2.0f; }
	}

	private void Start()
	{
		lineTransform.position = new Vector3(0, Top, 0);
	}

#if UNITY_EDITOR
	private void Update()
	{
		Vector3 _leftTop = new Vector3(Left, Top, 0);
		Vector3 _rightTop = new Vector3(Right, Top, 0);
		Vector3 _rightBottom = new Vector3(Right, Bottom, 0);
		Vector3 _leftBottm = new Vector3(Left, Bottom, 0);

		Debug.DrawLine(_leftTop, _rightTop, lineColor);
		Debug.DrawLine(_rightTop, _rightBottom, lineColor);
		Debug.DrawLine(_rightBottom, _leftBottm, lineColor);
		Debug.DrawLine(_leftBottm, _leftTop, lineColor);
	}
#endif
}

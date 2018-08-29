using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using KC;
//[ExecuteInEditMode]
public class CharacterBoundaryManager : Singleton<CharacterBoundaryManager>
{
	public new Camera camera;

	[Range(0, 1)]
	public float topPercent;
	[Range(0, 1)]
	public float bottomPercent;
	public Color lineColor;

	public List<GameObject> lineList = new List<GameObject>();

	private void Awake()
	{
		foreach(var go in lineList)
		{
			go.SetActive(false);
		}
	}

	[ShowInInspector]
	public float Bottom
	{
		get { return -ExtentHeight + (ExtentHeight * 2 * (1 - bottomPercent)); }
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
		get { return ExtentHeight * 2.0f - (ExtentHeight * 2 * (1 - bottomPercent)) - (ExtentHeight * 2 * (1 - topPercent)); }
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

	[ShowInInspector]
	public Vector3 Center
	{
		get { return new Vector3((Left + Right) / 2, (Bottom + Top) / 2, 0); }
	}

	public bool IsInCharacterBoundary(Vector3 position)
	{
		return position.x <= Right && position.x >= Left && position.y <= Top && position.y >= Bottom;
	}

	public Vector3 GetRandomPositionInBoundary()
	{
		return new Vector3(Random.Range(Left, Right), Random.Range(Bottom, Top), 0);
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

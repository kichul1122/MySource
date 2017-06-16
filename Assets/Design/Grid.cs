using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
	public float width;
	public float height;

	public int limit;

	private void Start()
	{
		Reposition();
	}

	public void Reposition<T>(List<T> list) where T : MonoBehaviour
	{
		for (int i = 0; i < list.Count; ++i)
		{
			int vertical = i / limit;
			int horizontal = i % limit;
			list[i].transform.localPosition = new Vector3(horizontal * width, -vertical * height, 0);
		}
	}

	[ContextMenu("Reposition")]
	public void Reposition()
	{
		List<Transform> transformList = GetChildList();

		for (int i = 0; i < transformList.Count; ++i)
		{
			int vertical = i / limit;
			int horizontal = i % limit;
			transformList[i].transform.localPosition = new Vector3(horizontal * width, -vertical * height, 0);
		}
	}

	public List<Transform> GetChildList()
	{
		List<Transform> childTransformList = new List<Transform>();

		for (int i = 0; i < transform.childCount; ++i)
		{
			Transform childTransform = transform.GetChild(i);
			if (childTransform && childTransform.gameObject.activeSelf)
			{
				childTransformList.Add(childTransform);
			}
		}
		return childTransformList;
	}
}

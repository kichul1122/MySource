using System.Collections;
using System.Collections.Generic;
using UniRx.Toolkit;
using UnityEngine;

public class PrefabObjectPool : ObjectPool<Transform>
{
	public GameObject prefab;
	public Transform parent;

	protected override Transform CreateInstance()
	{
		GameObject newGameObject = GameObject.Instantiate(prefab);
		if(parent)
		{
			newGameObject.transform.SetParent(parent);
		}

		return newGameObject.transform;
	}
}
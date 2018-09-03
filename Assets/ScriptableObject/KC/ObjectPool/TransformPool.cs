using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx.Toolkit;
using UnityEngine;
using UniRx;

[CreateAssetMenu(menuName = "Pool/Transform")]
public class TransformPool : ScriptableObject
{
	public GameObject prefab;
	public List<Transform> objectList = new List<Transform>();

	private PrefabObjectPool pool = new PrefabObjectPool();
	private IDisposable stopShrink = null;

	public bool IsPreLoad = false;
	public int preloadCount = 5;
	public int threshold = 1;

	public float ShirinkTime = 3f;
	public float instanceCountRatio = 0.6f;
	public int minSize = 4;
	public bool callOnBeforeRent = false;
	

	public void Initialize(Transform parent)
	{
		pool.parent = parent;
		pool.prefab = prefab;
		objectList.Clear();

		StartShrinkTimer();

		if (IsPreLoad)
		{
			PreLoadAsync();
		}
	}

	//[Button]
	public void PreLoadAsync()
	{
		pool.PreloadAsync(preloadCount, threshold).Subscribe(_ => { });
	}

	//[Button]
	public Transform Rent()
	{
		Transform newTransform = pool.Rent();
		objectList.Add(newTransform);
		return newTransform;
	}

	public void Return(Transform returnTransform)
	{
		Transform findTransform = objectList.Find(element => element == returnTransform);
		if (findTransform)
		{
			pool.Return(findTransform);
			objectList.Remove(findTransform);
		}
		else
		{
			Destroy(returnTransform.gameObject);
		}
	}

	//[Button]
	public void Shrink()
	{
		pool.Shrink(instanceCountRatio, minSize, callOnBeforeRent);
	}

	//[Button]
	public void StartShrinkTimer()
	{
		stopShrink = pool.StartShrinkTimer(System.TimeSpan.FromSeconds(ShirinkTime), instanceCountRatio, minSize, callOnBeforeRent);
	}

	//[Button]
	public void StopShrinkTimer()
	{
		stopShrink.Dispose();
	}

	//[Button]
	public void Clear()
	{
		pool.Clear();
	}
}
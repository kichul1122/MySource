using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System.Linq;

[CreateAssetMenu(menuName = "Manager/Particle")]
public class ParticleManager : ScriptableObject
{
	public List<TransformPool> transformPoolList = new List<TransformPool>();

	public void Init(Transform parent)
	{
		foreach (var pool in transformPoolList)
		{
			pool.Initialize(parent);
		}
	}

	public void Destory()
	{
		foreach (var pool in transformPoolList)
		{
			pool.Clear();
		}
	}

	//[Button]
	public void Initialize()
	{
#if UNITY_EDITOR
		transformPoolList = EditorUtil.FindAssetListOfType<TransformPool>().Where(_ => !_.name.Contains("Billboard")).ToList();
#endif
	}
}


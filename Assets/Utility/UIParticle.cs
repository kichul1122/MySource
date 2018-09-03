//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//[ExecuteInEditMode]
//[AddComponentMenu("NGUI/UI/NGUI Particle")]
//public class UIParticle : UIWidget
//{
//	int _lastQueue = 0;

//	protected override void OnUpdate()
//	{
//		base.OnUpdate();

//		if(drawCall == null)
//			return;

//		int queue = drawCall.renderQueue;

//		queue += mDepth >= 0 ? 1 : -1;

//		if (_lastQueue != queue)
//		{
//			_lastQueue = queue;

//			Renderer[] _renderers = GetComponentsInChildren<Renderer>();

//			foreach (Renderer r in _renderers)
//			{
//				r.sortingOrder = drawCall.sortingOrder;
//				r.material.renderQueue = _lastQueue;
//			}
//		}
//	}
//}
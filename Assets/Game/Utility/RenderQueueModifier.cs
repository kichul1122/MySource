//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class RenderQueueModifier : MonoBehaviour {

//	public enum RenderType
//	{
//		FRONT,
//		BACK
//	}
//	public UIPanel panel;
//	public UIWidget target = null;
//	public RenderType type = RenderType.FRONT;

//	Renderer[] renderers;

//	[SerializeField]
//	int curentQueue = 0;

//	void Start()
//	{
//		renderers = GetComponentsInChildren<Renderer>();
//	}

//	void FixedUpdate()
//	{
//		if (target != null && target.drawCall != null)
//		{
//			int queue = target.drawCall.renderQueue;

//			queue += type == RenderType.FRONT ? 1 : -1;

//			if (curentQueue != queue)
//			{
//				curentQueue = queue;

//				foreach (Renderer renderer in renderers)
//				{
//					renderer.sortingOrder = target.drawCall.sortingOrder;
//					renderer.material.renderQueue = curentQueue;
//				}
//			}
//		}
//	}
//}

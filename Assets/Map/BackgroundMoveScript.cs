using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class BackgroundMoveScript : SerializedMonoBehaviour, IMapInitialize
{
	public enum DirType { X, Y }
	public DirType dirType = DirType.X;

	//public float scrollSpeed = 1f;

	public float scrollOffset;

	Vector2 startPos;

	float newPos;

	//private IStage iStage;

	public void Initialize()
	{
		StartCoroutine(UpdateBackGround());
	}
	
	IEnumerator UpdateBackGround ()
	{
		startPos = transform.position;

		while (true)
		{
			float scrollSpeed = 0;//StageManager.PlayerCharacter?.Stage?.ScrollSpeed ?? 0;

			newPos = Mathf.Repeat(-Time.time * scrollSpeed, scrollOffset);

			switch (dirType)
			{
				case DirType.X:
					transform.position = startPos + Vector2.right * newPos;
					break;
				case DirType.Y:
					transform.position = startPos + Vector2.up * newPos;
					break;
				default:
					break;

			}

			yield return null;
		}
	}
}

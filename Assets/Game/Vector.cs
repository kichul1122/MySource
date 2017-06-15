using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vector : MonoBehaviour
{
	public Vector3 startPosition;
	public Vector3 endPosition;
	public float time;
	public float delay;

	private void Start()
	{
		StartCoroutine(DOMoveCoroutine(transform, startPosition, endPosition, time, delay));
	}

	public static IEnumerator DOMoveCoroutine(Transform transform, Vector3 startPosition, Vector3 endPosition, float time = 1f, float delay = 1f)
	{
		yield return new WaitForSeconds(delay);

		transform.position = startPosition;

		float accumulateTime = 0;
		while (accumulateTime < time)
		{
			accumulateTime += Time.deltaTime;

			transform.position = Vector3.Lerp(startPosition, endPosition, accumulateTime / time);
			yield return null;
		}

		transform.position = endPosition;
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScene : MonoBehaviour
{
	#region Private Variables
	[SerializeField]
	private RawImage fadeRawImage;
	[SerializeField]
	private float fadeInDuration;
	#endregion

	private void Awake()
	{
		DontDestroyOnLoad(this);
	}

	#region Public Methods
	public void Fade(bool isIn)
	{
		StartCoroutine(FadeInOutCoroutine(isIn));
	}

	public IEnumerator FadeCoroutine(bool isIn)
	{
		yield return StartCoroutine(FadeInOutCoroutine(isIn));
	}

	private IEnumerator FadeInOutCoroutine(bool isIn)
	{
		Color fadeColor = fadeRawImage.color;
		fadeColor.a = GetAlpha(isIn);

		fadeRawImage.color = fadeColor;

		float accumulateTime = 0;
		while (accumulateTime < fadeInDuration)
		{
			accumulateTime += Time.deltaTime;

			fadeColor.a = Mathf.Lerp(GetAlpha(isIn), GetAlpha(!isIn), accumulateTime / fadeInDuration);

			fadeRawImage.color = fadeColor;

			yield return null;
		}

		fadeColor.a = GetAlpha(!isIn);
		fadeRawImage.color = fadeColor;

		Destroy(gameObject);
	}

	private float GetAlpha(bool isIn)
	{
		return isIn ? 1f : 0f;
	}
	#endregion
}

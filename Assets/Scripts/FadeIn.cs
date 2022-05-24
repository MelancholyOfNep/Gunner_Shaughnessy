using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
	[SerializeField]
	GameObject fadeGraphic;
	[SerializeField]
	GameObject playerCapsule;

    private void Awake()
    {
		Time.timeScale = 1.0f;
		StartCoroutine(FadingIn());
	}

	IEnumerator FadingIn()
	{
		yield return new WaitForEndOfFrame();
		playerCapsule.SetActive(false);
		Color fadeColor = fadeGraphic.GetComponent<Image>().color;
		float fadeAmt;

		while (fadeGraphic.GetComponent<Image>().color.a > 0)
		{
			fadeAmt = fadeColor.a - (1f * Time.deltaTime);
			fadeColor = new Color(fadeColor.r, fadeColor.g, fadeColor.b, fadeAmt);
			fadeGraphic.GetComponent<Image>().color = fadeColor;
			yield return null;
		}
		playerCapsule.SetActive(true);
	}
}

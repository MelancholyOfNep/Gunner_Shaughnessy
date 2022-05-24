using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeQuit : MonoBehaviour
{
	[SerializeField]
	GameObject fadeGraphic;
	[SerializeField]
	LoadingCheck loadingCheck;

	private void Start()
	{
		loadingCheck = GameObject.Find("FadeIn").GetComponent<LoadingCheck>();
	}

	public void Quit()
	{
		if (loadingCheck.loading != true)
			StartCoroutine(QuitByFade());
	}

	IEnumerator QuitByFade()
	{
		loadingCheck.loading = true;
		Color fadeColor = fadeGraphic.GetComponent<Image>().color;
		float fadeAmt;

		while (fadeGraphic.GetComponent<Image>().color.a < 1)
		{
			fadeAmt = fadeColor.a + (1 * Time.deltaTime);
			fadeColor = new Color(fadeColor.r, fadeColor.g, fadeColor.b, fadeAmt);
			fadeGraphic.GetComponent<Image>().color = fadeColor;
			yield return null;
		}

		Application.Quit();
	}
}

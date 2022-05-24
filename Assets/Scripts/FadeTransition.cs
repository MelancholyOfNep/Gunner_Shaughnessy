using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeTransition : MonoBehaviour
{
	[SerializeField]
	string sceneSelected;
	[SerializeField]
	GameObject fadeGraphic;
	[SerializeField]
	LoadingCheck loadingCheck;

    private void Start()
    {
		loadingCheck = GameObject.Find("FadeIn").GetComponent<LoadingCheck>();
    }

    public void SwitchScene()
	{
		if (loadingCheck.loading != true)
		{
			Time.timeScale = 1.0f;
			StartCoroutine(AsyncSceneLoad());
		}
	}

	IEnumerator AsyncSceneLoad()
	{
		loadingCheck.loading = true;
		Color fadeColor = fadeGraphic.GetComponent<Image>().color;
		float fadeAmt;

		while(fadeGraphic.GetComponent<Image>().color.a < 1)
        {
			fadeAmt = fadeColor.a + (1 * Time.deltaTime);
			fadeColor = new Color(fadeColor.r, fadeColor.g, fadeColor.b, fadeAmt);
			fadeGraphic.GetComponent<Image>().color = fadeColor;
			yield return null;
        }

		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneSelected);

		// Wait until the scene fully loads
		while (!asyncLoad.isDone)
			yield return null;
	}
}

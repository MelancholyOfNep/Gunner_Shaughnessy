using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VictoryScript : MonoBehaviour
{
	[SerializeField]
	LevelToLoad ltlScript;
	[SerializeField]
	GameObject fadeGraphic;

    private void Start()
    {
		ltlScript = GameObject.Find("SceneSelector").GetComponent<LevelToLoad>();
	}

    private void OnTriggerEnter(Collider other)
    {
		other.gameObject.GetComponent<StarterAssets.FirstPersonController>().enabled = false;
		other.gameObject.GetComponent<StarterAssets.FirstPersonController>()._gunAnimator.SetBool("Firing", false);
		other.gameObject.GetComponent<HealthScript>().dead = true;
		StartCoroutine(Win());
    }

    IEnumerator Win()
	{
		Color fadeColor = fadeGraphic.GetComponent<Image>().color;
		float fadeAmt;

		while (fadeGraphic.GetComponent<Image>().color.a < 1)
		{
			fadeAmt = fadeColor.a + (1 * Time.deltaTime);
			fadeColor = new Color(fadeColor.r, fadeColor.g, fadeColor.b, fadeAmt);
			fadeGraphic.GetComponent<Image>().color = fadeColor;
			yield return null;
		}

		ltlScript.scene = SceneManager.GetActiveScene().name;
		SceneManager.LoadScene("VictoryScene");
	}
}

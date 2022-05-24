using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class HealthScript : MonoBehaviour
{
	int health = 100;
	[SerializeField]
	TextMeshProUGUI healthText;
	[SerializeField]
	LevelToLoad ltlScript;
	[SerializeField]
	GameObject fadeout;
	public bool dead = false;
	[SerializeField]
	StarterAssets.FirstPersonController fpc;
	[SerializeField]
	GameObject damagedUI;

	// Start is called before the first frame update
	void Start()
	{
		fpc = this.gameObject.GetComponent<StarterAssets.FirstPersonController>();
		healthText.text = "Health: " + health;
		ltlScript = GameObject.Find("SceneSelector").GetComponent<LevelToLoad>();
	}

	public void PlayerDamage(int damVal)
	{
		if (dead != true)
		{
			health -= damVal;
			StartCoroutine(DamageFlash());
			healthText.text = "Health: " + health;
			if (health <= 0)
			{
				healthText.text = "Health: 0";
				dead = true;
				fpc._gunAnimator.SetBool("Firing", false);
				fpc.enabled = false;
				StartCoroutine(Die());
			}
		}
	}

	IEnumerator DamageFlash()
    {
		damagedUI.SetActive(true);
		yield return new WaitForSeconds(.15f);
		damagedUI.SetActive(false);
    }

	IEnumerator Die()
    {
		Color fadeColor = fadeout.GetComponent<Image>().color;
		float fadeAmt;

		while (fadeout.GetComponent<Image>().color.a < 1)
		{
			fadeAmt = fadeColor.a + (1 * Time.deltaTime);
			fadeColor = new Color(fadeColor.r, fadeColor.g, fadeColor.b, fadeAmt);
			fadeout.GetComponent<Image>().color = fadeColor;
			yield return null;
		}

		ltlScript.scene = SceneManager.GetActiveScene().name;
		SceneManager.LoadScene("LossScene");
	}
}

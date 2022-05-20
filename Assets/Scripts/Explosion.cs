using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
	private void Start()
	{
		StartCoroutine(ExplosionCountdown());
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.GetComponent<EnemyScript>() != null)
		{
			other.gameObject.GetComponent<EntityClass>().Damage(694201337);
		}

		if (other.gameObject.GetComponent<HealthScript>() != null)
			other.gameObject.GetComponent<HealthScript>().PlayerDamage(694201337);
	}

	IEnumerator ExplosionCountdown()
	{
		yield return new WaitForSeconds(.25f);
		Destroy(gameObject);
	}
}

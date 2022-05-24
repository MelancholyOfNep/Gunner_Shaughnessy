using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
	[SerializeField]
	SphereCollider explosionColl;
	[SerializeField]
	Light expLight;

	private void Start()
	{
		explosionColl = this.gameObject.GetComponent<SphereCollider>();
		StartCoroutine(ExplosionCountdown());
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.GetComponent<EnemyScript>() != null)
			other.gameObject.GetComponent<EnemyScript>().Damage(694201337);
		else if (other.gameObject.GetComponent<EntityClass>() != null)
			other.gameObject.GetComponent<EntityClass>().Damage(694201337);

		if (other.gameObject.GetComponent<HealthScript>() != null)
			other.gameObject.GetComponent<HealthScript>().PlayerDamage(694201337);
	}

	IEnumerator ExplosionCountdown()
	{
		yield return new WaitForSeconds(.25f);
		explosionColl.enabled = false;
		while(expLight.intensity > 0)
        {
			expLight.intensity -= 25000f * Time.deltaTime;
			yield return null;
        }
		yield return new WaitForSeconds(.5f);
		Destroy(gameObject);
	}
}

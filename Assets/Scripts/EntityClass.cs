using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityClass : MonoBehaviour
{
	public float healthPoints;
	//public Transform gitsEffect;

	// Internal value used to keep value of speed
	protected float _Speed;

	// Property that modifies value of _speed
	public virtual float Speed
	{
		set
		{
			_Speed = value;
		}

		get
		{
			return _Speed;
		}
	}

	public virtual void Damage(float damVal)
	{
		healthPoints -= damVal;
		if (healthPoints <= 0)
			Kill();
	}

	public virtual void Kill() // virtual allows for overriding the command
	{
		healthPoints = 0;
		if (gameObject.GetComponent<ExplosiveBarrelScript>() != null)
			gameObject.GetComponent<ExplosiveBarrelScript>().BlowUp();
		//Instantiate(gitsEffect, this.transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}

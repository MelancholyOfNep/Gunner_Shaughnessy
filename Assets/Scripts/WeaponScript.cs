using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponScript : MonoBehaviour
{
	public Animator weaponAnimator;
	public PlayerInput playerInput;
	GameObject mainCamera;

	public float cooldown = 0;
	[Header("Weapon Stats")]
	[SerializeField]
	public float fireRate;
	[SerializeField]
	public float damage;

	[Header("Raycast Stuff")]
	[SerializeField]
	LayerMask layerMask;
	[SerializeField]
	float maxRaycastDist;

	int hitcount;
	

	private void Awake()
	{
		mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
	}

	void Update()
	{
		if (Time.time - cooldown > fireRate)
		{
			if (Mouse.current.leftButton.isPressed)
			{
				/*Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
				Debug.DrawRay(ray.origin, ray.direction * maxRaycastDist);
				RaycastHit hit;

				if (Physics.Raycast(ray, out hit, maxRaycastDist, layerMask))
				{
					Debug.Log(hitcount);
					hitcount++;
					//EntityClass entityClass;
					//hit.rigidbody.gameObject.GetComponent<EnemyHealth>().Damage();
					//entityClass = hit.collider.gameObject.GetComponent<EntityClass>();
					hit.collider.gameObject.GetComponent<EntityClass>().Damage(damage);
					//entityClass.Damage(damage);
					//Debug.Log(entityClass.healthPoints);
				}*/
				//weaponAnimator.SetBool("Firing", true);
			}
			else
			{
				//weaponAnimator.SetBool("Firing", false);
			}
			cooldown = Time.time;
		}
	}
}

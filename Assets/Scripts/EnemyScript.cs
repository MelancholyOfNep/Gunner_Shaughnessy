using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : EntityClass
{
	// the transform the enemy will chase
	[SerializeField]
	Transform target;
	// how often can the navigation update
	[SerializeField]
	float updateFrequency = 0.1f;
	// count how long it's been since the last update
	private float updateCounter = 0;
	// the component that controls the enemy's movement
	private NavMeshAgent agent;
	//
	[SerializeField]
	float minCombatDistance = 1;
	// enum that holds the values for the state machine
	[SerializeField]
	enum State { Idle = 0, Patrolling = 1, Combat = 2, Searching = 3, Attacking = 4, Help = 5 };
	
	// Animator controlling enemy animations
	Animator animator;

	// The current state of the enemy, determining their behavior at a given time
	[SerializeField]
	State currentState = State.Idle;
	[SerializeField]
	LayerMask layerMask;

	[Header("Idle")]
	[SerializeField]
	float idleTime = 1;
	float idleTimeCounter = 3;

	[Header("Patrol")]
	Vector3 patrolDestination;
	[SerializeField]
	private float minPatrolDistance = 1;
	[SerializeField]
	float patrolRange = 3;

	[Header("Searching")]
	Vector3 searchingDestination;

	[Header("Attacking")]
	[SerializeField]
	float attackRange;
	[SerializeField]
	float attackCooldown;
	[SerializeField]
	float attackTimer;

	//[Header("Spawn")]
	//public Transform particle;

	[Header("Debug")]
	[SerializeField]
	Vector3 targetpos;

	public Transform eyes;

	

	// Start is called before the first frame update
	void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		// or agent = transform.GetComponent<NavMeshAgent>();
		// or agent = this.transform.GetComponent<NavMeshAgent>();

		animator = GetComponent<Animator>();

		target = GameObject.Find("Capsule").transform;
	}

	// Update is called once per frame
	void Update()
	{
		animator.SetInteger("State", (int)currentState);
		targetpos = target.position;

		switch (currentState)
		{
			case State.Idle:
				Idle();
				break;
			case State.Patrolling:
				Patrolling();
				break;
			case State.Combat:
				Combat();
				break;
			case State.Searching:
				Searching();
				break;
			case State.Attacking:
				Attacking();
				break;
			case State.Help: // empty, so transitions to the next case (default)

			default:
				Debug.Log("Doing nothing");
				break;
		}

		/* MOVED TO Combat
		 *
		 * if (updateCounter >= updateFrequency)
		{
			agent.SetDestination(target.position);
			updateCounter = 0;
		}
		else
			updateCounter += Time.deltaTime;*/
	}

	void Idle()
	{
		agent.speed = 0;
		if (idleTimeCounter >= idleTime)
		{
			currentState = State.Patrolling;
			patrolDestination = this.transform.position +
				new Vector3(Random.Range(-patrolRange, patrolRange), 0, Random.Range(-patrolRange, patrolRange));
			// idleTime = 0;
			agent.SetDestination(patrolDestination);
		}
		else idleTimeCounter += Time.deltaTime;

		Ray ray = new Ray(eyes.position, (target.position - eyes.position).normalized);
		Debug.DrawRay(ray.origin, ray.direction * minCombatDistance);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, minCombatDistance, layerMask))
		{
			if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
				//if (hit.transform.tag)
				currentState = State.Combat;
		}
		/*if (Vector3.Distance(target.position, this.transform.position) <= minCombatDistance)
			currentState = State.Combat;*/
	}

	void Patrolling()
	{
		agent.speed = 0.5f;
		Ray ray = new Ray(eyes.position, target.position - eyes.position);
		Debug.DrawRay(ray.origin, ray.direction * minCombatDistance);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, minCombatDistance, layerMask))
		{
			currentState = State.Combat;
		}

		else if (Vector3.Distance(patrolDestination, this.transform.position) <= minPatrolDistance)
			currentState = State.Idle;
	}
	
	void Combat()
	{
		if (Vector3.Distance(target.position, this.transform.position) <= attackRange)
		{
			agent.speed = 0.0f;
			currentState = State.Attacking;
		}
		else agent.speed = 3.0f;

		agent.angularSpeed = 180f;
		if (updateCounter >= updateFrequency)
		{
			agent.SetDestination(target.position);
			updateCounter = 0;
		}
		else
			updateCounter += Time.deltaTime;
		
	}

	void Searching()
	{
		if (Vector3.Distance(target.position, this.transform.position) <= minCombatDistance)
			currentState = State.Combat;
	}

	void Attacking()
	{
		agent.speed = 0.0f;
		if (Vector3.Distance(target.position, this.transform.position) <= attackRange && Time.time > attackTimer + attackCooldown)
		{
			target.parent.gameObject.GetComponent<HealthScript>().PlayerDamage(10);
			attackTimer = Time.time;
		}
		currentState = State.Combat;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolAlert : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<EnemyScript>() != null)
        {
            if (other.gameObject.GetComponent<EnemyScript>().currentState != EnemyScript.State.Combat
                && other.gameObject.GetComponent<EnemyScript>().currentState != EnemyScript.State.Attacking)
            {
                other.gameObject.GetComponent<EnemyScript>().currentState = EnemyScript.State.Combat;
            }
        }
    }
}

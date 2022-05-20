using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrelScript : MonoBehaviour
{
    [SerializeField]
    GameObject explosion;

    public void BlowUp()
    {
        Instantiate(explosion, this.transform.position, Quaternion.identity);
    }
}
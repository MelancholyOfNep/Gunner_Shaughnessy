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


    // Start is called before the first frame update
    void Start()
    {
        healthText.text = "Health: " + health;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerDamage(int damVal)
    {
        health -= damVal;
        healthText.text = "Health: " + health;
        if (health <= 0)
        {
            //death stuff
        }
    }
}

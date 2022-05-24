using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    [SerializeField]
    LevelToLoad ltlScript;

    private void Start()
    {
        ltlScript = GameObject.Find("SceneSelector").GetComponent<LevelToLoad>();
    }

    public void Victory()
    {
        {
            ltlScript.scene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene("VictoryScene");
        }
    }
}

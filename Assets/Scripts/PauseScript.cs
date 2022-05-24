using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Video;

public class PauseScript : MonoBehaviour
{
    [SerializeField]
    GameObject pauseUI;
    public bool pausedToggle;
    [SerializeField]
    GameObject playerCapsule;
    [SerializeField]
    HealthScript healthScript;

    // Start is called before the first frame update
    void Start()
    {
        pauseUI.SetActive(false);
        pausedToggle = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (!healthScript.dead)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (pausedToggle == false)
                {
                    pauseUI.SetActive(true);
                    pausedToggle = true;
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.Confined;
                    playerCapsule.SetActive(false);
                    Time.timeScale = 0.0f;
                }
                else
                {
                    pauseUI.SetActive(false);
                    pausedToggle = false;
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    playerCapsule.SetActive(true);
                    Time.timeScale = 1.0f;
                }
            }
        }
    }
}

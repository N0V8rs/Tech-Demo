using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManage : MonoBehaviour
{
    private bool pauseActive = false;
    public GameObject pauseUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseActive) 
                Unpause();
            else 
                PauseOn();
        }
    }

    public void Unpause()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1.0f;
        pauseActive = false;

        GetComponent<PlayerController>().enabled = true;
    }

    public void PauseOn()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        pauseActive = true;

        GetComponent<PlayerController>().enabled = false;
    }
}

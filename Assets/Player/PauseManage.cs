using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManage : MonoBehaviour
{
    private bool pauseActive = false;
    public GameObject pauseUI;
    public GameObject quitDesktop;
    public PlayerController playerController;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !playerController.isDead)
        {
            if (pauseActive) 
                Unpause();
            else 
                PauseOn();
        }
    }

    public void Unpause()
    {
        Cursor.visible = false;
        pauseUI.SetActive(false);
        Time.timeScale = 1.0f;
        pauseActive = false;
        quitDesktop.SetActive(false);

        GetComponent<PlayerController>().enabled = true;
    }

    public void PauseOn()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        pauseActive = true;
        quitDesktop.SetActive(true);

        GetComponent<PlayerController>().enabled = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

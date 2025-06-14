using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject Pause_Menu;
    public PlayerController Player;
    bool isPaused;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }

        if (isPaused)
        {
            Pause_Menu.SetActive(true);
            Time.timeScale = 0;
            Player.enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        
        else
        {
            Pause_Menu.SetActive(false);
            Time.timeScale = 1;
            Player.enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

        }
    }
     public void ResumeGame()
    {
        isPaused = false;
    }
    public void SettingsButton()
    {

        SceneManager.LoadScene(2);

    }
    public void ExitToMainMenu()
    {

        SceneManager.LoadScene(0);

    }


  
}

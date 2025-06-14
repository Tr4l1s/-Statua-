using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadScaneMenu : MonoBehaviour

{
    public void ExitToMainMenu()
    {

        SceneManager.LoadScene(0);

    }

    public void QuitButton()
    {

        Application.Quit();


    }
}
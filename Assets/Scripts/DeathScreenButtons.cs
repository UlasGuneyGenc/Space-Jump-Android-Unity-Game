using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenButtons : MonoBehaviour
{
    public void RestartScene(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

}

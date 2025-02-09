using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void GameStart()
    {
        SceneManager.LoadScene(5);
    }
    public void GameBack()
    {
        SceneManager.LoadScene(0);
    }
    public void Setting()
    {
        SceneManager.LoadScene(6);
    }
    public void GameExit()
    {
        Application.Quit();
    }
}

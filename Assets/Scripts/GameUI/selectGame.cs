using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class selectGame : MonoBehaviour
{
    public void selectOne()
    {
        SceneManager.LoadScene(1);

    }
    public void selectTwo()
    {
        SceneManager.LoadScene(2);

    }
    public void selectThree()
    {
        SceneManager.LoadScene(3);

    }
    public void selectFour()
    {
        SceneManager.LoadScene(4);

    }

}

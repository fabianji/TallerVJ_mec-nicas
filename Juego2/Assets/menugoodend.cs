using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menugoodend : MonoBehaviour
{
    public void Volvermenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Nivelanterior()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] string mainGameScene = "";

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting Game");
    }

    public void RetryLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(mainGameScene);
    }
}

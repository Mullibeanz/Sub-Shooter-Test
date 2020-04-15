using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] string startScene;
    [SerializeField] string gameScene;
    [SerializeField] string creditsScene;
    [SerializeField] string controlsScene;
    [SerializeField] string gameOverScene;

    [SerializeField] float delayInSeconds = 2f;


    public void LoadStartScene()
    {
        SceneManager.LoadScene(startScene);
        FindObjectOfType<GameSession>().ResetScore();
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(gameScene);
        FindObjectOfType<GameSession>().ResetScore();
    }

    public void LoadCreditsScene()
    {
        SceneManager.LoadScene(creditsScene);
    }

    public void LoadControlsScene()
    {
        SceneManager.LoadScene(controlsScene);
    }

    public void LoadGameOverScene()
    {
        StartCoroutine(WaitAndLoad());
    }

    IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene(gameOverScene);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Application Quit");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene(startScene);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            Debug.Log("Application Quit");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public static Scene currentScene;
    public static float timer = 0;
    public static bool pressed_start = false;
    #region Unity_Functions

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        } else if(instance != this) {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if(pressed_start)
        {
            timer += Time.deltaTime;
            if (timer >= 3.5)
            {
                secondStart();
                timer = 0;
            }
        }
    }
    #endregion

    public void StartGame()
    {
        //Instantiate(pre_f);
        pressed_start = true;
        SceneManager.LoadScene("Opening");
    }
    private void secondStart()
    {
        pressed_start = false;
        SceneManager.LoadScene("Level1");
    }
   
    public void MainMenu()
    {
       SceneManager.LoadScene("MainMenu");
    }
    public void LoseGame()
    {
        SceneManager.LoadScene("DeathScreen");
    }

    public void WinGame()
    {
        SceneManager.LoadScene("Ending");
    }

    public void RestartLevel()
    {
        Debug.Log(GameManager.currentScene.name);
        SceneManager.LoadScene(currentScene.name);
    }

    public void SetCurrentLevel(Scene scene)
    {
        currentScene = scene;
        Debug.Log("Set: " + currentScene.name);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorOpen : MonoBehaviour
{
    private Scene currentScene;

    void Awake()
    {
        currentScene = SceneManager.GetActiveScene();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Player")
        {
            if (currentScene.name == "Level1")
            {
                SceneManager.LoadScene("Level2");
            }
        }
    }
}

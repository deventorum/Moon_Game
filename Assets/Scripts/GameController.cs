using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    private const string mainMenuScene = "MainMenu";
    private const string gameScene = "Platform";
    private const string gameOverScene = "GameOver";

    public static GameController Instance;


    // Use this for initialization
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NewGame()
    {
        SceneManager.LoadScene(gameScene);
    }

    public void GameOver()
    {
        SceneManager.LoadScene(gameOverScene);
    }
}

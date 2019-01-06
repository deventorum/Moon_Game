using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    private const string mainMenuScene = "MainMenu";
    private const string level1Scene = "Level_1_terrain";
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
        SceneManager.LoadScene(level1Scene);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GameOver()
    {
        SceneManager.LoadScene(gameOverScene);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

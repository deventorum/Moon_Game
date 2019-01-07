using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{

    public TextMeshProUGUI score;

    // Use this for initialization
    void Start()
    {
        score.text = "Score: " + PlayerPrefs.GetInt("final_score");
    }

    // Update is called once per frames
    void Update()
    {

    }

    private void OnDestroy()
    {
        // reset score counter
        PlayerPrefs.SetInt("final_score", 0);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class WinGame : MonoBehaviour
{
    AudioSource winningAudio;
    // Start is called before the first frame update
    void Start()
    {
        winningAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            winningAudio.Play(0);
            Invoke("GameCompleted", 3f);
        }
    }
    private void GameCompleted()
    {
        PlayerPrefs.SetInt("final_score", FindObjectOfType<GameManager>().GetCurrentGold());
        GameController.Instance.GameOver();
    }
}

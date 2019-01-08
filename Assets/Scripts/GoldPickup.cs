using UnityEngine;
public class GoldPickup : MonoBehaviour
{
    public int value;
    AudioSource goldAudio;

    // Start is called before the first frame update
    void Start()
    {
        goldAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            goldAudio.Play(0);
            FindObjectOfType<GameManager>().AddGold(value);

            Invoke("DestroyGold", 0.4f);
        }

    }
    private void DestroyGold()
    {
        Destroy(gameObject);
    }
}
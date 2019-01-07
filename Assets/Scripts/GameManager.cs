using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int currentGold;
    public TextMeshProUGUI goldText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddGold(int goldToAdd)
    {
        currentGold += goldToAdd;
        goldText.text = "Gold: " + currentGold;
    }

    public int GetCurrentGold()
    {
        return currentGold;
    }
}

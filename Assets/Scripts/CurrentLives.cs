using UnityEngine;
using TMPro;

public class CurrentLives : MonoBehaviour
{
    public TextMeshProUGUI currentLives;
    public int lives;

    void Update()
    {
        lives = GameObject.Find("Player").GetComponent<Player>().lives;
        currentLives.GetComponent<TextMeshProUGUI>().SetText(lives.ToString());
    }
}

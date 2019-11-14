using UnityEngine;
using TMPro;

public class CurrentScore : MonoBehaviour
{
    public TextMeshProUGUI currentScore;
    public int score;

    void Update()
    {
        score = GameObject.Find("Player").GetComponent<Player>().score;
        currentScore.GetComponent<TextMeshProUGUI>().SetText(score.ToString());
    }
}
using UnityEngine;
using TMPro;

public class CurrentLevel : MonoBehaviour
{
    public TextMeshProUGUI currentLevel;
    public int level;

    void Update()
    {
        level = GameObject.Find("GameSpawner").GetComponent<GameSpawner>().level;
        currentLevel.GetComponent<TextMeshProUGUI>().SetText(level.ToString());
    }
}

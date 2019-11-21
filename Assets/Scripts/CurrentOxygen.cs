using UnityEngine;
using TMPro;

public class CurrentOxygen : MonoBehaviour
{
    public TextMeshProUGUI currentOxygen;
    public int oxygen;
    public int totalOxygen;

    void Update()
    {
        oxygen = (int)GameObject.Find("Player").GetComponent<Player>().oxygen;
        totalOxygen = (int)GameObject.Find("Player").GetComponent<Player>().totalOxygen;
        currentOxygen.GetComponent<TextMeshProUGUI>().SetText(oxygen.ToString() + "/" + totalOxygen.ToString());
    }
}

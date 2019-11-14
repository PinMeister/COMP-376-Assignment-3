using UnityEngine;

public class Gold : MonoBehaviour
{
    [SerializeField] float goldLifespan = 15;
    float goldTimer;
    GameSpawner gameSpawnerVariable;

    void Start()
    {
        goldTimer = goldLifespan;
        gameSpawnerVariable = GameObject.Find("GameSpawner").GetComponent<GameSpawner>();
    }

    void Update()
    {
        if (transform.parent == null)
        {
            if (GameObject.Find("Player").GetComponent<Player>().alive)
            {
                goldTimer -= Time.deltaTime;
            }

            if (goldTimer <= 0)
            {
                float timePassed = Time.time;
                gameSpawnerVariable.goldXList.Add((int)(transform.position.x));
                gameSpawnerVariable.goldCounter -= 1;
                Destroy(gameObject);
            }
        }
    }
}

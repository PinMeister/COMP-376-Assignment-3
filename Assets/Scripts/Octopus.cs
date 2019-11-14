using UnityEngine;

public class Octopus : MonoBehaviour
{
    [SerializeField]
    public float moveSpeed = 1;
    [SerializeField]
    float positionOffset = 2.5f;
    [SerializeField]
    public int boundaryMax = 2;

    int boundaryCounter;
    Transform leftBoundary;
    Transform rightBoundary;
    GameSpawner gameSpawnerVariable;

    void Start()
    {
        boundaryCounter = 0;
        leftBoundary = GameObject.Find("LeftBoundary").transform;
        rightBoundary = GameObject.Find("RightBoundary").transform;
        gameSpawnerVariable = GameObject.Find("GameSpawner").GetComponent<GameSpawner>();
    }

    void Update()
    {
        if (GameObject.Find("Player").GetComponent<Player>().alive)
        {
            if (GetComponent<SpriteRenderer>().flipX == true)
            {
                transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            }
        }

        if (transform.position.x > rightBoundary.position.x - positionOffset - 1)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            boundaryCounter += 1;
        }
        if (transform.position.x < leftBoundary.position.x + positionOffset)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            boundaryCounter += 1;
        }

        if (boundaryCounter == boundaryMax)
        {
            gameSpawnerVariable.octopusPresent = false;
            gameSpawnerVariable.octopusTimer = Random.Range(0, gameSpawnerVariable.levelTimer / 2);

            Destroy(gameObject);
        }
    }
}

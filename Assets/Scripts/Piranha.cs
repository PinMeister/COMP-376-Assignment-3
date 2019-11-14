using UnityEngine;

public class Piranha : MonoBehaviour
{
    [SerializeField]
    float positionOffset = 1;
    [SerializeField]
    public int boundaryMax = 2;
    [SerializeField]
    public float moveSpeed = 1;

    int boundaryCounter;
    Transform leftBoundary;
    Transform rightBoundary;
    GameSpawner gameSpawnerVariable;
    Vector2 startPosition;

    void Start()
    {
        boundaryCounter = 0;
        leftBoundary = GameObject.Find("LeftBoundary").transform;
        rightBoundary = GameObject.Find("RightBoundary").transform;
        gameSpawnerVariable = GameObject.Find("GameSpawner").GetComponent<GameSpawner>();
        startPosition = transform.position;
    }

    void Update()
    {
        if (GameObject.Find("Player").GetComponent<Player>().alive)
        {
            if (GetComponent<SpriteRenderer>().flipX == true)
            {
                startPosition = transform.position;
                startPosition.x += -1 * moveSpeed * Time.deltaTime;
                //startPosition.y += Mathf.Sin(Time.time) * Time.deltaTime * moveSpeed;
                transform.position = startPosition;
            }
            else
            {
                startPosition = transform.position;
                startPosition.x += 1 * moveSpeed * Time.deltaTime;
                //startPosition.y += Mathf.Sin(Time.time) * Time.deltaTime * moveSpeed;
                transform.position = startPosition;
            }
        }

        if (transform.position.x > rightBoundary.position.x - positionOffset - 1)
        {
            transform.position = new Vector2(leftBoundary.position.x + positionOffset, transform.position.y);
            boundaryCounter += 1;
        }
        if (transform.position.x < leftBoundary.position.x + positionOffset)
        {
            transform.position = new Vector2(rightBoundary.position.x - positionOffset - 1, transform.position.y);
            boundaryCounter += 1;
        }

        if (boundaryCounter == boundaryMax)
        {
            gameSpawnerVariable.piranhaYList.Add((int)(transform.position.y));
            gameSpawnerVariable.piranhaCounter -= 1;
            Destroy(gameObject);
        }

    }
}

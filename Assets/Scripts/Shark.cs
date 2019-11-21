using UnityEngine;

public class Shark : MonoBehaviour
{
    [SerializeField]
    public int boundaryMax = 2;
    [SerializeField]
    public float moveSpeed = 10;

    int boundaryCounter;
    Transform leftBoundary;
    Transform rightBoundary;
    GameSpawner gameSpawnerVariable;
    Vector3 startPosition;
    Transform northBoundary;
    Transform southBoundary;
    Transform eastBoundary;
    Transform westBoundary;

    void Start()
    {
        boundaryCounter = 0;
        northBoundary = GameObject.Find("North Boundary").transform;
        southBoundary = GameObject.Find("South Boundary").transform;
        eastBoundary = GameObject.Find("East Boundary").transform;
        westBoundary = GameObject.Find("West Boundary").transform;
        gameSpawnerVariable = GameObject.Find("GameSpawner").GetComponent<GameSpawner>();
        startPosition = transform.position;
    }

    void Update()
    {
        if (GameObject.Find("Player").GetComponent<Player>().alive)
        {
            if (true)
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

        if (transform.position.z > northBoundary.position.z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, southBoundary.position.z);
            boundaryCounter += 1;
        }
        if (transform.position.z < southBoundary.position.z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, northBoundary.position.z);
            boundaryCounter += 1;
        }
        if (transform.position.x > eastBoundary.position.x)
        {
            transform.position = new Vector3(westBoundary.position.x, transform.position.y, transform.position.z);
            boundaryCounter += 1;
        }
        if (transform.position.x < westBoundary.position.x)
        {
            transform.position = new Vector3(eastBoundary.position.x, transform.position.y, transform.position.z);
            boundaryCounter += 1;
        }

        if (boundaryCounter == boundaryMax)
        {
            gameSpawnerVariable.piranhaXList.Add((int)(transform.position.x));
            gameSpawnerVariable.piranhaZList.Add((int)(transform.position.z));
            gameSpawnerVariable.piranhaCounter -= 1;
            Destroy(gameObject);
        }

    }
}

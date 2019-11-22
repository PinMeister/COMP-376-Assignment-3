using UnityEngine;

public class Shark : MonoBehaviour
{
    [SerializeField]
    public int boundaryMax = 3;
    [SerializeField]
    public float moveSpeed = 10;

    int boundaryCounter;
    GameSpawner gameSpawnerVariable;
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
    }

    void Update()
    {
        if (GameObject.Find("Player").GetComponent<Player>().alive)
        {
            transform.Translate(transform.forward * moveSpeed * Time.deltaTime, Space.World);
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
            gameSpawnerVariable.sharkYList.Add((int)(transform.position.y));
            gameSpawnerVariable.sharkCounter -= 1;
            Destroy(gameObject);
        }

    }
}

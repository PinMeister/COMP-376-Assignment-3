using UnityEngine;

public class Octopus : MonoBehaviour
{
    [SerializeField]
    public float moveSpeed = 10;
    [SerializeField]
    public int boundaryMax = 2;

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
            transform.Rotate(0, 180, 0);
            boundaryCounter += 1;
        }
        if (transform.position.z < southBoundary.position.z)
        {
            transform.Rotate(0, 180, 0);
            boundaryCounter += 1;
        }
        if (transform.position.x > eastBoundary.position.x)
        {
            transform.Rotate(0, 180, 0);
            boundaryCounter += 1;
        }
        if (transform.position.x < westBoundary.position.x)
        {
            transform.Rotate(0, 180, 0);
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

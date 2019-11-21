using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSpawner : MonoBehaviour
{
    [SerializeField] float levelDuration = 30;
    [SerializeField] float sharkSpawnInterval = 5;
    [SerializeField] GameObject shark;
    //[SerializeField] GameObject octopus;
    [SerializeField] GameObject smallGold;
    [SerializeField] GameObject mediumGold;
    [SerializeField] GameObject largeGold;
    
    public int level = 1;
    public int sharkCounter = 0;
    int sharkMax = 4;
    public int goldCounter = 0;
    int goldMax = 5;
    public float levelTimer;
    float goldTimer;
    float sharkTimer;
    public float octopusTimer;
    public bool octopusPresent = false;
    int[] octopusXZArray = { -30, -10, 10, 30 };
    int[] octopusYArray = { -2, -1, 0, 1 };
    int[] enemyXZArray = { -50, 50 };
    int[] sharkXZArray = { -40, -20, 0, 20, 40 };
    public List<int> sharkYList = new List<int> { 20, 30, 40, 50 };
    public List<int> goldXList = new List<int> { -40, -20, 0, 20, 40 };
    public List<int> goldZList = new List<int> { -40, -20, 0, 20, 40 };
    public bool flipped;

    void Start()
    {
        levelTimer = levelDuration;
        sharkTimer = sharkSpawnInterval;
        goldTimer = 1;

        if (levelTimer > 5)
        {
            octopusTimer = Random.Range(1, levelTimer / 5);
        }
        else
        {
            octopusTimer = Random.Range(0, levelTimer / 2);
        }
    }

    void Update()
    {
        if (GameObject.Find("Player").GetComponent<Player>().alive)
        {
            levelTimer -= Time.deltaTime;
            sharkTimer -= Time.deltaTime;
            octopusTimer -= Time.deltaTime;
            goldTimer -= Time.deltaTime;
        }

        if (sharkCounter < sharkMax && sharkTimer <= 0)
        {
            int sharkXZ = enemyXZArray[Random.Range(0, enemyXZArray.Length)];
            int sharkY = sharkYList[Random.Range(0, sharkYList.Count)];

            sharkYList.Remove(sharkY);

            float randomBoundary = Random.Range(0f, 1f);
            int randomXZ = sharkXZArray[Random.Range(0, sharkXZArray.Length)];

            if (sharkXZ == 50)
            {
                if (randomBoundary <= 0.5)
                {
                    GameObject newShark = Instantiate(shark, new Vector3(50, sharkY, randomXZ), Quaternion.Euler(0, -90, 0)); // e to w
                }
                else
                {
                    GameObject newShark = Instantiate(shark, new Vector3(randomXZ, sharkY, 50), Quaternion.Euler(0, -180, 0)); // n to s

                }
            }
            else
            {
                if (randomBoundary <= 0.5)
                {
                    GameObject newShark = Instantiate(shark, new Vector3(-50, sharkY, randomXZ), Quaternion.Euler(0, 90, 0)); // w to e
                }
                else
                {
                    GameObject newShark = Instantiate(shark, new Vector3(randomXZ, sharkY, -50), Quaternion.identity); // s to n
                }
            }

            float sharkSize = Random.Range(1.5f, 2.5f);
            float sharkSpeed = Random.Range(10f, 20f) + (level + 5) * 0.5f;

            shark.transform.localScale = new Vector3(sharkSize, sharkSize, sharkSize);
            shark.GetComponent<Shark>().moveSpeed = sharkSpeed;
            int sharkBoundaryMax = Random.Range(3, 5);
            shark.GetComponent<Shark>().boundaryMax = sharkBoundaryMax;

            sharkCounter += 1;
            sharkTimer = sharkSpawnInterval;
            
        }

        /*if (octopusTimer <= 0 && octopusPresent == false)
        {
            octopusPresent = true;

            int octopusX = octopusXArray[Random.Range(0, octopusXArray.Length)];
            int octopusY = octopusYArray[Random.Range(0, octopusYArray.Length)];
            
            if (octopusX == 7)
            {
                octopus.GetComponent<SpriteRenderer>().flipX = false;
            }
            else
            {
                octopus.GetComponent<SpriteRenderer>().flipX = true;
            }

            Instantiate(octopus, new Vector2(octopusX, octopusY), Quaternion.identity);

            float octopusSpeed = Random.Range(1f, 2f) + level * 0.5f;
            octopus.GetComponent<Octopus>().moveSpeed = octopusSpeed;
            int octopusBoundaryMax = 2;
            octopus.GetComponent<Octopus>().boundaryMax = octopusBoundaryMax;
        }*/

        if (goldCounter < goldMax && goldTimer <= 0)
        {
            int goldX = goldXList[Random.Range(0, goldXList.Count)];
            int goldZ = goldZList[Random.Range(0, goldXList.Count)];
            goldXList.Remove(goldX);
            goldZList.Remove(goldZ);

            float randomGoldSize = Random.Range(0f, 1f);

            if (randomGoldSize <= 0.6)
            {
                Instantiate(smallGold, new Vector3(goldX, 0.5f, goldZ), Quaternion.identity);
            }
            if (randomGoldSize > 0.6 && randomGoldSize <= 0.9)
            {
                Instantiate(mediumGold, new Vector3(goldX, 1, goldZ), Quaternion.identity);
            }
            if (randomGoldSize > 0.9)
            {
                Instantiate(largeGold, new Vector3(goldX, 5, goldZ), Quaternion.identity);
            }

            goldCounter += 1;
            goldTimer = Random.Range(5, 10);
        }

        if (levelTimer <= 0)
        {
            levelTimer = levelDuration;
            level += 1;
        }
    }
}

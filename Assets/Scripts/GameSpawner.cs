using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSpawner : MonoBehaviour
{
    [SerializeField] float levelDuration = 30;
    [SerializeField] float pirhanaSpawnInterval = 5;
    [SerializeField] GameObject piranha;
    [SerializeField] GameObject octopus;
    [SerializeField] GameObject smallGold;
    [SerializeField] GameObject mediumGold;
    [SerializeField] GameObject largeGold;
    [SerializeField] GameObject boost;
    
    public int level = 1;
    public int piranhaCounter = 0;
    int piranhaMax = 4;
    public int goldCounter = 0;
    int goldMax = 5;
    public float levelTimer;
    float goldTimer;
    float pirhanaTimer;
    public float octopusTimer;
    public bool octopusPresent = false;
    int[] octopusXArray = { -7, 7 };
    int[] octopusYArray = { -2, -1, 0, 1 };
    int[] piranhaXArray = { -9, 9 };
    public List<int> piranhaYList = new List<int> { -2, -1, 0, 1 };
    public List<int> goldXList = new List<int> { -8, -4, 0, 4, 8 };
    public bool special = false;
    public bool boostPresent = false;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Version Special")
        {
            special = true;
        }

        levelTimer = levelDuration;
        pirhanaTimer = pirhanaSpawnInterval;
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
            pirhanaTimer -= Time.deltaTime;
            octopusTimer -= Time.deltaTime;
            goldTimer -= Time.deltaTime;
        }

        if (piranhaCounter < piranhaMax && pirhanaTimer <= 0)
        {
            int piranhaX = piranhaXArray[Random.Range(0, piranhaXArray.Length)];
            int piranhaY = piranhaYList[Random.Range(0, piranhaYList.Count)];

            piranhaYList.Remove(piranhaY);

            if (piranhaX == 9)
            {
                piranha.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                piranha.GetComponent<SpriteRenderer>().flipX = false;
            }
            
            Instantiate(piranha, new Vector2(piranhaX, piranhaY), Quaternion.identity);

            float piranhaSize = Random.Range(0.25f, 0.75f);
            float piranhaSpeed = Random.Range(1f, 2f) + level * 0.5f;

            piranha.transform.localScale = new Vector2(piranhaSize, piranhaSize);
            piranha.GetComponent<Piranha>().moveSpeed = piranhaSpeed;
            int piranhaBoundaryMax = Random.Range(3, 5);
            piranha.GetComponent<Piranha>().boundaryMax = piranhaBoundaryMax;

            piranhaCounter += 1;
            pirhanaTimer = pirhanaSpawnInterval;
            
        }

        if (octopusTimer <= 0 && octopusPresent == false)
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
        }

        if (goldCounter < goldMax && goldTimer <= 0)
        {
            int goldX = goldXList[Random.Range(0, goldXList.Count)];
            goldXList.Remove(goldX);

            float randomGoldSize = Random.Range(0f, 1f);

            if (randomGoldSize <= 0.6)
            {
                Instantiate(smallGold, new Vector2(goldX, -4), Quaternion.identity);
            }
            if (randomGoldSize > 0.6 && randomGoldSize <= 0.9)
            {
                Instantiate(mediumGold, new Vector2(goldX, -4), Quaternion.identity);
            }
            if (randomGoldSize > 0.9)
            {
                Instantiate(largeGold, new Vector2(goldX, -4), Quaternion.identity);
            }

            goldCounter += 1;
            goldTimer = Random.Range(5, 10);
        }

        if (special == true && boostPresent == false)
        {
            boostPresent = true;
            int boostX = Random.Range(-8, 9);
            int boostY = Random.Range(-3, 3);
            Instantiate(boost, new Vector2(boostX, boostY), Quaternion.identity);
        }

        if (levelTimer <= 0)
        {
            levelTimer = levelDuration;
            level += 1;
        }

        if (Input.GetKey(KeyCode.RightShift) && special == false)
        {
            Application.LoadLevel("Version Special");
        }

        if (Input.GetKey(KeyCode.RightControl) && special == true)
        {
            Application.LoadLevel("Version Normal");
        }
    }
}

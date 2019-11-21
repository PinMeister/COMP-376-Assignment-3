using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]    float moveSpeed = 30;
    [SerializeField]    float rotateSpeed = 100;
    [SerializeField]    float swimForce = 50;
    [SerializeField]    float positionOffset = 0;

    Rigidbody submarine;
    GameObject gold;
    GameObject boost;
    GameSpawner gameSpawnerVariable;
    Transform northBoundary;
    Transform southBoundary;
    Transform eastBoundary;
    Transform westBoundary;
    public int score;
    public int tanks;
    public int lives;
    public bool alive = true;
    bool invincibility = false;
    bool hook = false;
    public float oxygen;
    public int totalOxygen;

    void Start()
    {
        submarine = GetComponent<Rigidbody>();
        gameSpawnerVariable = GameObject.Find("GameSpawner").GetComponent<GameSpawner>();
        northBoundary = GameObject.Find("North Boundary").transform;
        southBoundary = GameObject.Find("South Boundary").transform;
        eastBoundary = GameObject.Find("East Boundary").transform;
        westBoundary = GameObject.Find("West Boundary").transform;
        tanks = 2;
        lives = 2;
        oxygen = 100;
        totalOxygen = 100;
}

    void Update()
    {
        if (alive == true)
        {

            oxygen -= 1 * Time.deltaTime / 2;
            
            if (tanks == 1)
            {
                totalOxygen = 50;
            }

            if (Input.GetButton("Backward"))
                transform.Translate(-Vector3.forward * moveSpeed * Time.deltaTime);
            else if (Input.GetButton("Forward"))
                transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

            if (Input.GetButton("Left"))
                transform.Rotate(-Vector3.up * rotateSpeed * Time.deltaTime);
            else if (Input.GetButton("Right"))
                transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
            
            if (Input.GetKeyDown("space") || Input.GetMouseButtonDown(0))
            {
                submarine.AddForce(Vector2.up * swimForce);
            }
        }

        if (transform.position.z > northBoundary.position.z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, southBoundary.position.z);
        }
        if (transform.position.z < southBoundary.position.z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, northBoundary.position.z);
        }
        if (transform.position.x > eastBoundary.position.x)
        {
            transform.position = new Vector3(westBoundary.position.x, transform.position.y, transform.position.z);
        }
        if (transform.position.x < westBoundary.position.x)
        {
            transform.position = new Vector3(eastBoundary.position.x, transform.position.y, transform.position.z);
        }

        /*if (tanks == 0 && alive == true)
        {
            if (boostActive == true)
            {
                this.GetComponent<SpriteRenderer>().sprite = specialDead;
            }
            else
            {
                this.GetComponent<SpriteRenderer>().sprite = playerDead;
            }
            submarine.GetComponent<Rigidbody2D>().gravityScale = 1;
            Destroy(gold);
            submarine.mass = 1;
            hook = false;
            alive = false;
            lives -= 1;
            boostActive = false;
            moveSpeed = 3;
            swimForce = 50;
            boostTimer = 3;
        }

        if (Input.GetKey("return") && alive == false && lives > 0)
        {
            this.GetComponent<SpriteRenderer>().sprite = playerFull;
            submarine.GetComponent<Rigidbody2D>().gravityScale = 0.2f;
            alive = true;
            transform.position = new Vector2(0, 2);
            tanks = 2;
            if (boostHeld == true)
            {
                boostHeld = false;
                gameSpawnerVariable.boostPresent = false;
            }
        }*/
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Gold" && hook == false)
        {
            collider.transform.parent = transform;
            collider.enabled = false;
            hook = true;
            gameSpawnerVariable.goldXList.Add((int)(transform.position.x));
            gameSpawnerVariable.goldZList.Add((int)(transform.position.z));
            gold = GetChildWithTag("Gold");
            gameSpawnerVariable.goldCounter -= 1;

            if (gold.name.Contains("Small Gold"))
            {
                submarine.mass += 0.5f;
            }
            if (gold.name.Contains("Medium Gold"))
            {
                submarine.mass += 0.75f;
            }
            if (gold.name.Contains("Large Gold"))
            {
                submarine.mass += 1f;
            }
        }

        if (collider.tag == "Boat" && hook == true)
        {
            if (gold.name.Contains("Small Gold"))
            {
                score += 1;
            }
            if (gold.name.Contains("Medium Gold"))
            {
                score += 2;
            }
            if (gold.name.Contains("Large Gold"))
            {
                score += 10;
            }

            Destroy(gold);
            submarine.mass = 1;
            hook = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy" && tanks > 0 && invincibility == false)
        {
            invincibility = true;
            tanks -= 1;
        }

        if (collision.gameObject.tag == "Surface")
        {
            if (tanks == 2)
            {
                oxygen = 101;
            }
            if (tanks == 1)
            {
                oxygen = 51;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy" && invincibility == true)
        {
            invincibility = false;
        }
    }

    private GameObject GetChildWithTag(string input)
    {
        foreach (Transform child in transform)
        {
            if (child.tag == input)
            {
                return child.gameObject;
            }
        }
        return null;
    }

}   

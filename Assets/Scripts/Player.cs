using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]    float moveSpeed = 3;
    [SerializeField]    float swimForce = 50;
    [SerializeField]    float positionOffset = 0;
    [SerializeField]    Sprite playerFull;
    [SerializeField]    Sprite playerHit;
    [SerializeField]    Sprite playerDead;
    [SerializeField]    Sprite specialFull;
    [SerializeField]    Sprite specialHit;
    [SerializeField]    Sprite specialDead;

    Rigidbody2D submarine;
    GameObject gold;
    GameObject boost;
    GameSpawner gameSpawnerVariable;
    Transform leftBoundary;
    Transform rightBoundary;
    public int score;
    public int tanks;
    public int lives;
    public bool alive = true;
    bool invincibility = false;
    bool hook = false;
    public bool boostHeld = false;
    public bool boostActive = false;
    public float boostTimer;

    void Start()
    {
        submarine = GetComponent<Rigidbody2D>();
        gameSpawnerVariable = GameObject.Find("GameSpawner").GetComponent<GameSpawner>();
        leftBoundary = GameObject.Find("LeftBoundary").transform;
        rightBoundary = GameObject.Find("RightBoundary").transform;
        tanks = 2;
        lives = 2;
        boostTimer = 3;
}

    void Update()
    {
        if (alive == true)
        {
            transform.Translate(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, 0, 0);

            if (Input.GetKeyDown("space") || Input.GetKeyDown("w") || Input.GetKeyDown("up"))
            {
                submarine.AddForce(Vector2.up * swimForce);
            }
        }

        if (transform.position.x > rightBoundary.position.x - positionOffset - 1)
        {
            transform.position = new Vector2(leftBoundary.position.x + positionOffset, transform.position.y);
        }
        if (transform.position.x < leftBoundary.position.x + positionOffset)
        {
            transform.position = new Vector2(rightBoundary.position.x - positionOffset - 1, transform.position.y);
        }

        if (boostActive == true)
        {
            boostTimer -= Time.deltaTime;
            moveSpeed = 9;
            swimForce = 150;
            submarine.GetComponent<Rigidbody2D>().gravityScale = 0.6f;
        }

        if (boostTimer <= 0)
        {
            gameSpawnerVariable.boostPresent = false;
            boostActive = false;
            boostHeld = false;
            moveSpeed = 3;
            swimForce = 50;
            boostTimer = 3;
            submarine.GetComponent<Rigidbody2D>().gravityScale = 0.2f;
        }

        if (tanks == 2)
        {
            if (boostActive == true)
            {
                this.GetComponent<SpriteRenderer>().sprite = specialFull;
            }
            else
            {
                this.GetComponent<SpriteRenderer>().sprite = playerFull;
            }
        }

        if (tanks == 1)
        {
            if (boostActive == true)
            {
                this.GetComponent<SpriteRenderer>().sprite = specialHit;
            }
            else
            {
                this.GetComponent<SpriteRenderer>().sprite = playerHit;
            }
        }

        if (tanks == 0 && alive == true)
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
        }

        if (Input.GetKey(KeyCode.LeftShift) && boostHeld == true && gameSpawnerVariable.special == true)
        {
            boostActive = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Enemy" && tanks > 0 && invincibility == false)
        {
            invincibility = true;
            tanks -= 1;
        }

        if (collider.tag == "Gold" && hook == false)
        {
            collider.transform.parent = transform;
            collider.enabled = false;
            hook = true;
            gold = GetChildWithTag("Gold");
            gameSpawnerVariable.goldXList.Add((int)(gold.transform.position.x));
            gameSpawnerVariable.goldCounter -= 1;
            gold.transform.localPosition = new Vector2(0.06f, -1.1f);

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

        if (collider.tag == "Boost" && boostHeld == false)
        {
            Destroy(GameObject.FindGameObjectWithTag("Boost"));
            boostHeld = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Enemy" && invincibility == true)
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torpedo : MonoBehaviour
{
    [SerializeField]
    public float torpedoSpeed = 20;

    Player player;
    Octopus octopus;
    Transform northBoundary;
    Transform southBoundary;
    Transform eastBoundary;
    Transform westBoundary;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        octopus = GameObject.Find("Octopus(Clone)").GetComponent<Octopus>();
        transform.rotation = Quaternion.LookRotation((player.transform.position - transform.position).normalized);
        northBoundary = GameObject.Find("North Boundary").transform;
        southBoundary = GameObject.Find("South Boundary").transform;
        eastBoundary = GameObject.Find("East Boundary").transform;
        westBoundary = GameObject.Find("West Boundary").transform;
    }

    void Update()
    {
        if (GameObject.Find("Player").GetComponent<Player>().alive)
        {
            transform.Translate(transform.forward * torpedoSpeed * Time.deltaTime, Space.World);
        }
        if (transform.position.z > northBoundary.position.z || transform.position.z < southBoundary.position.z || transform.position.x > eastBoundary.position.x || transform.position.x < westBoundary.position.x || transform.position.y < 0)
        {
            octopus.torpedoPresent = false;
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            octopus.torpedoPresent = false;
            Destroy(gameObject);
        }
    }
}

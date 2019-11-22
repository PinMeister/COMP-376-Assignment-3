using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Underwater : MonoBehaviour
{
    Player player;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    void Update()
    {
        if (Input.GetKey("return"))
        {
            GetComponent<AudioSource>().mute = false;
        }
        if (!player.alive)
        {
            GetComponent<AudioSource>().mute = true;
        }
    }
}

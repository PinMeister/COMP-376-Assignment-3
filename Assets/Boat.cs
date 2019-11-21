using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    Vector3 startPosition;

    void Update()
    {
        startPosition = transform.position;
        startPosition.y += Mathf.Sin(Time.time) * Time.deltaTime;
        transform.position = startPosition;
    }
}

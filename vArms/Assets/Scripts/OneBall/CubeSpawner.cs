using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Respawn a ball (scenes with one ball)
/// </summary>
public class CubeSpawner : MonoBehaviour
{
    public GameObject cube0;
    public GameObject cube;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            cube0.active = true;
            cube.active = false;
        }
    }
}

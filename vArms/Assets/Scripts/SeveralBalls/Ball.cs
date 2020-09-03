using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public GameObject ball5;
    public GameObject ball5Rope;

    // Update is called once per frame
    void Update()
    {
        if (ball5.name == "BallWithRope5" && PlayerPrefs.GetFloat("BallsCount") < 5)
        {
            ball5.SetActive(false);
            ball5Rope.SetActive(false);
        }

        else
        {
            ball5.SetActive(true);
            ball5Rope.SetActive(true);
        }


    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using System;
using Neuron;

/// <summary>
/// handling the ball's behavior after collision with the arm 
/// </summary>
public class Explosion : MonoBehaviour
{
    public GameObject ballAnim;
    public GameObject ballRope;

    Timer eTimer;
    float explosionAngle;
    bool destroyable = true;



    // Use this for initialization
    void Start()
    {
        eTimer = gameObject.GetComponent<Timer>();

    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        explosionAngle = PlayerPrefs.GetFloat("ExplosionAngle");

        if (eTimer != null && destroyable && other.gameObject.tag == "Finger")
        {

            eTimer.Duration = PlayerPrefs.GetFloat("TimerDuration");
            eTimer.Run();
        }
    }

    private void OnTriggerStay(Collider other)
    {

        if (eTimer != null && eTimer.Finished && GameObject.FindWithTag("thumb").transform.localEulerAngles.z <= 288)
        {
            print(GameObject.FindWithTag("Avatar").GetComponent<ArmAnimator>().Data[1]);
            if (destroyable && GameObject.FindWithTag("Avatar").GetComponent<ArmAnimator>().Data[1] > explosionAngle)
            {
                gameObject.SetActive(false);
                ballAnim.SetActive(true);
                ballRope.SetActive(false);
                Arm arm = GameObject.FindWithTag("Avatar").GetComponent<Arm>();
                arm.goToNextBall = true;
                if (gameObject.name == "BallWithRope5")
                {
                    arm.moveToStart = true;
                }
            }
            else
            {
                destroyable = false;
                Arm arm = GameObject.FindWithTag("Avatar").GetComponent<Arm>();
                BallBehavior b = GameObject.Find(String.Format("BallWithRope{0}", arm.ballCount + 1)).GetComponent<BallBehavior>();

                b.move = true;
                arm.move = true;
                Destroy(gameObject.GetComponent<SphereCollider>());

            }
        }
    }


}
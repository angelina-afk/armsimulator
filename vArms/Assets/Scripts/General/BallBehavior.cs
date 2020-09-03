using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;

public class BallBehavior : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public Transform armPoint;
    Vector3 point0;
    Vector3 pointB;
    float startTime;
    float speed = .1f;
    float distanceLength;
    bool upBool = true;
    public bool move = false;
    public bool first = true;
    public bool rotate = false;
    public Vector3 offset;
    public Vector3 rOffset;
    public Vector3 o;
    public Vector3 midPoint = new Vector3(0f, -.15f, .3f);
    Vector3[] movePoints;
    public int currentPoint;

    // Start is called before the first frame update
    void Start()
    {
        int ballNumber = int.Parse(name.Substring(name.Length - 1));
        offset = armPoint.position - startPoint.position;
        rOffset = armPoint.eulerAngles - startPoint.eulerAngles;
        o = new Vector3(-0.317f, -0.26f, 0.199f);
        currentPoint = 0;
        startTime = Time.time;
        point0 = new Vector3(endPoint.position.x, -0.18f, endPoint.position.z);
        pointB = new Vector3(endPoint.position.x, -0.5f, endPoint.position.z);
        movePoints = new Vector3[] { midPoint,
                                    point0,
                                    // o,
                                    o + new Vector3(0.01f* ballNumber,
                                                    0.005f*ballNumber,
                                                    Mathf.Pow(-1,ballNumber-1)*0.01f* ballNumber ) };
        distanceLength = Vector3.Distance(startPoint.position, movePoints[0]);

    }

    // Update is called once per frame
    void Update()
    {
        if (move)
            MoveCube();

    }

    void MoveCube()
    {
        if (move && gameObject.transform.position == movePoints[currentPoint])
        {

            if (currentPoint >= movePoints.Length - 1)
            {
                move = false;
            }
            else
            {
                currentPoint++;
                startTime = Time.time;
                distanceLength = Vector3.Distance(gameObject.transform.position, movePoints[currentPoint]);
            }
        }

        if (first)
        {
            startTime = Time.time;
            first = false;

        }
        float distCovered = (Time.time - startTime) * speed;
        float fractionOfDistance = distCovered / distanceLength;

        if (currentPoint == 2)
        {
            distanceLength = Vector3.Distance(armPoint.position, new Vector3(0.883f, -1.041f, -0.18f));
            fractionOfDistance = distCovered / distanceLength;
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.Euler(76.45f, -75.19f, 8.593f),
                fractionOfDistance);
            transform.position = Vector3.Lerp(gameObject.transform.position, movePoints[currentPoint], fractionOfDistance);

        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, movePoints[currentPoint], Time.deltaTime * 0.5f);
        }
    }
}
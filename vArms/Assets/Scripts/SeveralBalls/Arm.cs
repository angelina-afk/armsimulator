using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// handling the arm's behavior after collision with the ball
/// </summary>
public class Arm : MonoBehaviour
{
    public Transform[] points = new Transform[5];
    public Transform cylinderPoint;
    public float startTime;
    float speed = .1f;
    public float distanceLength;
    public Vector3 pointB;
    public Vector3 offset;
    public Vector3 pointA;
    public Vector3 pointC;
    public bool move = false;
    public bool first = true;
    public Vector3[] startPoints;
    public Vector3[] endPoints;
    public Vector3 midPoint = new Vector3(0f, -.15f, .3f);
    public int currentPoint;
    public bool goToNextBall = false;
    bool moveToNextBall = false;
    public bool moveToStart = false;
    Quaternion startAngle;
    Timer eTimer;
    Vector3[] movePoints;
    public int ballCount;
    // Start is called before the first frame update
    void Start()
    {
        ballCount = 0;
        eTimer = gameObject.GetComponent<Timer>();
        currentPoint = 0;
        startPoints = new Vector3[5];
        endPoints = new Vector3[5];
        startAngle = gameObject.transform.rotation;
        for (int i = 0; i < points.Length; i++)
        {
            startPoints[i] = points[i].position;
            endPoints[i] = points[i].position;
        }

        offset = startPoints[ballCount] - gameObject.transform.position;
        pointA = endPoints[ballCount];
        pointB = new Vector3(cylinderPoint.position.x, -0.18f, cylinderPoint.position.z);
        pointC = new Vector3(cylinderPoint.position.x, -0.25f, cylinderPoint.position.z);
        movePoints = new Vector3[] { midPoint - offset,
                                        pointB - offset,
                                        new Vector3(0.883f, -1.061f, -0.18f),
                                        pointB - offset,
                                        midPoint - offset,
                                        endPoints[ballCount] - offset };
        currentPoint = 0;

        distanceLength = Vector3.Distance(gameObject.transform.position, movePoints[0]);

    }

    // Update is called once per frame
    void Update()
    {
        if (move)
            ArmMover();
        if (moveToStart)
            MoveToStart();
        if (goToNextBall && ballCount < startPoints.Length - 1)
        {
            goToNextBall = false;
            moveToNextBall = true;
            ballCount++;
            if (ballCount == 4)
                pointA = endPoints[0];
            else
                pointA = endPoints[ballCount];
            movePoints = new Vector3[] { midPoint - offset,
                                        pointB - offset,
                                        new Vector3(0.883f, -1.061f, -0.18f),
                                        pointB - offset,
                                        midPoint - offset,
                                        pointA - offset + new Vector3(0.01f* (currentPoint+1),
                                                    0.005f*(currentPoint+1),
                                                    Mathf.Pow(-1,currentPoint)*0.01f* (currentPoint+1) ) };

            startTime = Time.time;
            distanceLength = Vector3.Distance(gameObject.transform.position, startPoints[ballCount]);
        }
        if (moveToNextBall)
        {
            float distCovered = (Time.time - startTime) * speed;
            float fractionOfDistance = distCovered / distanceLength;
            transform.position = Vector3.Lerp(gameObject.transform.position, startPoints[ballCount] - offset, fractionOfDistance);
        }
        if (transform.position == startPoints[ballCount] - offset)
            moveToNextBall = false;

    }

    public void MoveToStart()
    {
        startTime = Time.time;
        distanceLength = Vector3.Distance(gameObject.transform.position, startPoints[0] - offset);
        transform.position = Vector3.MoveTowards(transform.position, startPoints[0] - offset, Time.deltaTime * 0.5f);

    }
    public void ArmMover()
    {
        if (move && gameObject.transform.position == movePoints[currentPoint])
        {

            if (currentPoint >= movePoints.Length - 1)
            {
                move = false;
                first = true;
                goToNextBall = true;
                currentPoint = 0;
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
            transform.rotation = Quaternion.Slerp(
                gameObject.transform.rotation,
                Quaternion.Euler(25.26f, -23.66f, 48.673f),
                fractionOfDistance);
            transform.position = Vector3.Lerp(gameObject.transform.position, movePoints[currentPoint], fractionOfDistance);

        }

        else if (currentPoint == 3)
        {
            transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, startAngle, fractionOfDistance);
            transform.position = Vector3.Lerp(gameObject.transform.position, movePoints[currentPoint], fractionOfDistance);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, movePoints[currentPoint], Time.deltaTime * 0.5f);
        }

    }
}

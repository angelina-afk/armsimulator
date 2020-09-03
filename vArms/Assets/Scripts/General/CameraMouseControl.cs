using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMouseControl : MonoBehaviour
{

    public float speedX = 2.0f;
    public float speedY = 2.0f;

    public float moveX = 0.0f;
    public float moveY = 0.0f;

    private float _x;
    private float _y;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        moveX -= Input.GetAxis("Vertical") * speedX;
        moveY += Input.GetAxis("Horizontal") * speedY;

        _x = Mathf.Clamp(moveX, -5.0f, 50.0f);
        _y = Mathf.Clamp(moveY, 5.0f, 5.0f);

        transform.eulerAngles = new Vector3(_x, 0.0f, 0.0f);
    }
}

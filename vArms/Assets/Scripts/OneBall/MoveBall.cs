using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// changes the appearance of the ball when changing settings
/// </summary>
public class MoveBall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(PlayerPrefs.GetFloat("CubeWidth"), PlayerPrefs.GetFloat("CubeWidth"), PlayerPrefs.GetFloat("CubeWidth"));
        transform.position = new Vector3(PlayerPrefs.GetFloat("CubeXCoord"), PlayerPrefs.GetFloat("CubeYCoord"), PlayerPrefs.GetFloat("CubeZCoord"));
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(PlayerPrefs.GetFloat("CubeWidth"), PlayerPrefs.GetFloat("CubeWidth"), PlayerPrefs.GetFloat("CubeWidth"));
        transform.position = new Vector3(PlayerPrefs.GetFloat("CubeXCoord"), PlayerPrefs.GetFloat("CubeYCoord"), PlayerPrefs.GetFloat("CubeZCoord"));
    }
}

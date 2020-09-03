using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMPanel : MonoBehaviour
{
    Animation start;
    public void Anim()
    {
        start = GetComponent<Animation>();
        start.Play();

    }

}

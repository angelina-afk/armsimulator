using System.Threading;
using UnityEngine;
using Utils;

public class Init : MonoBehaviour
{
    Thread listener = Listener.Listen();
    // GameObject settings;
    void Start()
    {
        listener.Start();
        //     settings = GameObject.FindWithTag("Settings");
        //     settings.SetActive(false);
    }
    void OnDisable()
    {
        listener.Abort();
    }

}
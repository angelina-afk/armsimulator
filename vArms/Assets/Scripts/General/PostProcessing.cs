using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

/// <summary>
/// moves camera focus with the arm
/// </summary>
public class PostProcessing : MonoBehaviour
{
    public Transform focusTarget;
    PostProcessVolume postProcessingVolume;
    PostProcessProfile postProfile;
    // Start is called before the first frame update
    void Start()
    {
        postProfile = gameObject.GetComponent<PostProcessVolume>().profile;
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(Camera.main.transform.position, focusTarget.position);
        DepthOfField dof = postProfile.GetSetting<DepthOfField>();
        dof.focusDistance.value = dist;

    }
}

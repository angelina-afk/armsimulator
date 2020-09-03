using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System;
using System.Linq;
using Utils;

/// <summary>
/// Animator for one arm (scenes with balls)
/// </summary>
public class ArmAnimator : MonoBehaviour
{
    public bool animate = true;

    public Animator animator = null;
    public InputField maxIndexAngleFemale;
    public InputField maxIndexAngleMale;

    public int test = 5;
    float[] data;

    public Renderer rend;

    public static int lH01 = 0;
    public static int lI = 1;
    public static int lM = 2;
    public static int lR = 3;
    public static int lP = 4;

    Vector3[] boneRotationOffsets = new Vector3[(int)HumanBodyBones.LastBone];

    Arm arm;
    public float[] Data
    {
        get { return data; }
    }
    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        arm = gameObject.GetComponent<Arm>();
    }


    // Update is called once per frame
    void Update()
    {
        if (animate)
        {
            try
            {
                // data = new float[] { 1, 3, 5, 7, 9 };
                data = DataTransfer.queue1.Dequeue();
                // print(data[0]);
                ApplyMotion(this, animator, data);
                // print(maxIndexAngleFemale.text);
                // print(maxIndexAngleMale.text);
                if (arm.move)
                {
                    data = new float[] { -60, 0, 0, 0, 0 };
                    ApplyMotion(this, animator, data);
                }


            }
            catch (InvalidOperationException) { } // В очереди пусто
        }


    }

    public void Animate()
    {
        animate = true;
    }
    public Vector3 GetReceivedRotation(int bone, float[] data, bool proxm)
    {

        int offset = 0;
        offset = bone;

        // for 48
        // offset += bone * 3;
        if (this.name == "FPArms_Female_LPR_LOD0")
        {
            if (proxm)
            {
                switch (bone)
                {
                    case 0:
                        return new Vector3(46f, -123f, -Mathf.Clamp(-data[offset] - 55, 0f, 10f) / 2 - 68);
                    case 1:
                        return new Vector3(0.0f, 5.0f, Mathf.Clamp(-data[offset] - 25f, PlayerPrefs.GetFloat("MaxIndexAngleFemale"), -5f));
                    case 2:
                        return new Vector3(0.0f, 5.0f, Mathf.Clamp(-data[offset] - 20f, -20f, -5f));
                    case 3:
                        return new Vector3(-18.0f, 15.0f, Mathf.Clamp(-data[offset] - 20f, -20f, -5f));
                    case 4:
                        return new Vector3(-21f, 20.0f, Mathf.Clamp(-data[offset] - 20f, -20f, -5f));
                    default:
                        return new Vector3(0, 0, 0);
                }
            }
            else
            {
                switch (bone)
                {
                    case 0:
                        return new Vector3(5f, -5f, -Mathf.Clamp(-data[offset] - 55, 0f, 10f) * 3);
                    case 1:
                        return new Vector3(0.0f, 0.0f, 2 * Mathf.Clamp(-data[offset] - 25f, PlayerPrefs.GetFloat("MaxIndexAngleFemale"), 0));
                    case 2:
                        return new Vector3(0.0f, 0.0f, Mathf.Clamp(-data[offset] - 20f, -20f, -5f));
                    case 3:
                        return new Vector3(0.0f, 0.0f, Mathf.Clamp(-data[offset] - 20f, -20f, -5f));
                    case 4:
                        return new Vector3(0.0f, 0.0f, Mathf.Clamp(-data[offset] - 20f, -20f, -5f));
                    default:
                        return new Vector3(0, 0, 0);
                }
            }
        }
        else
        {
            if (proxm)
            {
                switch (bone)
                {
                    case 0:
                        return new Vector3(43f, -117f, -Mathf.Clamp(-data[offset] - 55, 0f, 10f) / 2 - 68);
                    case 1:
                        return new Vector3(0.0f, 5.0f, Mathf.Clamp(-data[offset] - 20f, PlayerPrefs.GetFloat("MaxIndexAngleMale"), -5f));
                    case 2:
                        return new Vector3(0.0f, 5.0f, Mathf.Clamp(-data[offset] - 20f, -20f, -5f));
                    case 3:
                        return new Vector3(-18.0f, 15.0f, Mathf.Clamp(-data[offset] - 20f, -20f, -5f));
                    case 4:
                        return new Vector3(-21f, 20.0f, Mathf.Clamp(-data[offset] - 20f, -20f, -5f));
                    default:
                        return new Vector3(0, 0, 0);

                }
            }
            else
            {
                switch (bone)
                {
                    case 0:
                        return new Vector3(15f, -4f, -Mathf.Clamp(-data[offset] - 55, 0f, 10f) * 3);
                    case 1:
                        return new Vector3(0.0f, 0.0f, 2 * Mathf.Clamp(-data[offset] - 20f, PlayerPrefs.GetFloat("MaxIndexAngleMale"), 0));
                    case 2:
                        return new Vector3(0.0f, 0.0f, Mathf.Clamp(-data[offset] - 20f, -20f, -5f));
                    case 3:
                        return new Vector3(0.0f, 0.0f, Mathf.Clamp(-data[offset] - 20f, -20f, -5f));
                    case 4:
                        return new Vector3(0.0f, 5.0f, Mathf.Clamp(-data[offset] - 20f, -20f, -5f));
                    default:
                        return new Vector3(0, 0, 0);
                }
            }
        }
    }
    static void SetRotation(Animator animator, HumanBodyBones bone, Vector3 rotation)
    {
        Transform t = animator.GetBoneTransform(bone);
        if (t != null)
        {
            Quaternion rot = Quaternion.Euler(rotation);
            if (!float.IsNaN(rot.x) && !float.IsNaN(rot.y) && !float.IsNaN(rot.z) && !float.IsNaN(rot.w))
            {
                t.localRotation = rot;
            }
        }
    }

    public static void ApplyMotion(ArmAnimator actor, Animator animator, float[] data)
    {
        // SetRotation(animator, HumanBodyBones.LeftLowerArm, actor.GetReceivedRotation(lH, data, false));

        // // SetRotation(animator, HumanBodyBones.LeftThumbProximal, actor.GetReceivedRotation(lH01, data, true));
        // SetRotation(animator, HumanBodyBones.LeftThumbIntermediate, actor.GetReceivedRotation(lH01, data, true));
        // SetRotation(animator, HumanBodyBones.LeftThumbDistal, new Vector3(15f, (actor.GetReceivedRotation(lI, data, true).y - 10f) * 3f, -10f));
        // SetRotation(animator, HumanBodyBones.LeftThumbProximal, new Vector3(43f, -117f, (-actor.GetReceivedRotation(lH01, data, true).y / 2 - 68)));

        // SetRotation(animator, HumanBodyBones.LeftThumbDistal, new Vector3(15f, -4f, (-actor.GetReceivedRotation(lH01, data, true).y * 3)));

        SetRotation(animator, HumanBodyBones.LeftThumbProximal, actor.GetReceivedRotation(lH01, data, true));
        SetRotation(animator, HumanBodyBones.LeftThumbDistal, actor.GetReceivedRotation(lH01, data, false));

        SetRotation(animator, HumanBodyBones.LeftIndexProximal, actor.GetReceivedRotation(lI, data, true));
        SetRotation(animator, HumanBodyBones.LeftIndexIntermediate, actor.GetReceivedRotation(lI, data, false));
        SetRotation(animator, HumanBodyBones.LeftIndexDistal, actor.GetReceivedRotation(lI, data, false));

        SetRotation(animator, HumanBodyBones.LeftMiddleProximal, actor.GetReceivedRotation(lM, data, true));
        SetRotation(animator, HumanBodyBones.LeftMiddleIntermediate, actor.GetReceivedRotation(lM, data, false));
        // SetRotation(animator, HumanBodyBones.LeftMiddleDistal, actor.GetReceivedRotation(lM, data, false));

        // SetRotation(animator, HumanBodyBones.LeftMiddleProximal, actor.GetReceivedRotation(lM, data, true));
        // SetRotation(animator, HumanBodyBones.LeftMiddleIntermediate, actor.GetReceivedRotation(lM, data, false));
        // SetRotation(animator, HumanBodyBones.LeftMiddleDistal, actor.GetReceivedRotation(lM, data, false));

        SetRotation(animator, HumanBodyBones.LeftRingProximal, actor.GetReceivedRotation(lR, data, true));
        SetRotation(animator, HumanBodyBones.LeftRingIntermediate, actor.GetReceivedRotation(lR, data, false));
        // SetRotation(animator, HumanBodyBones.LeftRingDistal, actor.GetReceivedRotation(lR, data, false));

        SetRotation(animator, HumanBodyBones.LeftLittleProximal, actor.GetReceivedRotation(lP, data, true));
        SetRotation(animator, HumanBodyBones.LeftLittleIntermediate, actor.GetReceivedRotation(lP, data, false));
        // SetRotation(animator, HumanBodyBones.LeftLittleDistal, actor.GetReceivedRotation(lP, data, false));


        // SetRotation(animator, HumanBodyBones.LeftShoulder, actor.GetReceivedRotation(lSh));

        // for 48
        // SetRotation(animator, HumanBodyBones.LeftHand, actor.GetReceivedRotation(lH, data));
        // SetRotation(animator, HumanBodyBones.LeftThumbProximal, actor.GetReceivedRotation(lH01, data));
        // SetRotation(animator, HumanBodyBones.LeftThumbIntermediate, actor.GetReceivedRotation(lH02, data));
        // SetRotation(animator, HumanBodyBones.LeftThumbDistal, actor.GetReceivedRotation(lH03, data));
        // SetRotation(animator, HumanBodyBones.LeftIndexProximal, actor.GetReceivedRotation(lH1, data));
        // SetRotation(animator, HumanBodyBones.LeftIndexIntermediate, actor.GetReceivedRotation(lH2, data));
        // SetRotation(animator, HumanBodyBones.LeftIndexDistal, actor.GetReceivedRotation(lH3, data));
        // SetRotation(animator, HumanBodyBones.LeftMiddleProximal, actor.GetReceivedRotation(lH4, data));
        // SetRotation(animator, HumanBodyBones.LeftMiddleIntermediate, actor.GetReceivedRotation(lH5, data));
        // SetRotation(animator, HumanBodyBones.LeftMiddleDistal, actor.GetReceivedRotation(lH6, data));
        // SetRotation(animator, HumanBodyBones.LeftRingProximal, actor.GetReceivedRotation(lH7, data));
        // SetRotation(animator, HumanBodyBones.LeftRingIntermediate, actor.GetReceivedRotation(lH8, data));
        // SetRotation(animator, HumanBodyBones.LeftRingDistal, actor.GetReceivedRotation(lH9, data));
        // SetRotation(animator, HumanBodyBones.LeftLittleProximal, actor.GetReceivedRotation(lH10, data));
        // SetRotation(animator, HumanBodyBones.LeftLittleIntermediate, actor.GetReceivedRotation(lH11, data));
        // SetRotation(animator, HumanBodyBones.LeftLittleDistal, actor.GetReceivedRotation(lH12, data));
    }

}


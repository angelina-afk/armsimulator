using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System;
using System.Linq;
using Utils;

/// <summary>
/// Animator for the first arm (two arms scene)
/// </summary>
public class AnimatorOne : MonoBehaviour
{

    // public Transform lH; 

    public Animator animator = null;

    public Renderer rend;

    public static int lH = 0;
    public static int lI = 1;
    public static int lM = 2;
    public static int lR = 3;
    public static int lP = 4;
    public static int lH01 = 5;
    public static int lH02 = 6;
    public static int lH03 = 7;

    Vector3[] boneRotationOffsets = new Vector3[(int)HumanBodyBones.LastBone];


    // Use this for initialization
    void Start()
    {
        // rend.enabled = false;
        animator = GetComponent<Animator>();
        SetRotation(animator, HumanBodyBones.LeftUpperArm, new Vector3(-180, 90, -80));
        SetRotation(animator, HumanBodyBones.LeftLowerArm, new Vector3(0, 0, 80));
    }


    // Update is called once per frame
    void Update()
    {
        try
        {
            var data = DataTransfer.queue1.Dequeue();
            ApplyMotion(this, animator, data);
        }
        catch (InvalidOperationException) { } // В очереди пусто
    }

    public Vector3 GetReceivedRotation(int bone, float[] data, bool proxm)
    {
        int offset = 0;
        offset = bone;

        // int offset = 0;
        // offset += bone * 3;

        if (proxm)
        {
            switch (bone)
            {
                // case 0:
                //     return new Vector3(0.0f, Mathf.Clamp(180.0f - data[offset], 180.0f - 30.0f, 180.0f + 30.0f), 100.0f);
                case 1:
                    return new Vector3(0.0f, 5.0f, Mathf.Clamp(-data[offset], 0.0f, 90.0f));
                case 2:
                    return new Vector3(0.0f, 5.0f, Mathf.Clamp(-data[offset], 0.0f, 90.0f));
                case 3:
                    return new Vector3(0.0f, 5.0f, Mathf.Clamp(-data[offset], 0.0f, 90.0f));
                case 4:
                    return new Vector3(0.0f, -15.0f, Mathf.Clamp(-data[offset], 0.0f, 90.0f));
                default:
                    return new Vector3(0, 0, 0);

            }
        }
        else
        {
            switch (bone)
            {
                // case 0:
                //     return new Vector3(0.0f, 125 - (-data[offset]), 0.0f);
                case 1:
                    return new Vector3(0.0f, 0.0f, Mathf.Clamp(-data[offset], 0.0f, 90.0f));
                case 2:
                    return new Vector3(0.0f, 0.0f, Mathf.Clamp(-data[offset], 0.0f, 90.0f));
                case 3:
                    return new Vector3(0.0f, 0.0f, Mathf.Clamp(-data[offset], 0.0f, 90.0f));
                case 4:
                    return new Vector3(0.0f, 0.0f, Mathf.Clamp(-data[offset], 0.0f, 90.0f));
                case 5:
                    return new Vector3(data[offset + 1], -data[offset], -data[offset + 2]);
                case 6:
                    return new Vector3(data[offset + 1 + 2], -data[offset + 2], -data[offset + 2 + 2]);
                case 7:
                    return new Vector3(data[offset + 1 + 2], -data[offset + 2], -data[offset + 2 + 2]);
                default:
                    return new Vector3(0, 0, 0);
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

    public static void ApplyMotion(AnimatorOne actor, Animator animator, float[] data)
    {


        SetRotation(animator, HumanBodyBones.LeftIndexProximal, actor.GetReceivedRotation(lI, data, true));
        SetRotation(animator, HumanBodyBones.LeftIndexIntermediate, actor.GetReceivedRotation(lI, data, false));
        SetRotation(animator, HumanBodyBones.LeftMiddleProximal, actor.GetReceivedRotation(lM, data, true));
        SetRotation(animator, HumanBodyBones.LeftMiddleIntermediate, actor.GetReceivedRotation(lM, data, false));
        SetRotation(animator, HumanBodyBones.LeftRingProximal, actor.GetReceivedRotation(lR, data, true));
        SetRotation(animator, HumanBodyBones.LeftRingIntermediate, actor.GetReceivedRotation(lR, data, false));
        SetRotation(animator, HumanBodyBones.LeftLittleProximal, actor.GetReceivedRotation(lP, data, true));
        SetRotation(animator, HumanBodyBones.LeftLittleIntermediate, actor.GetReceivedRotation(lP, data, false));

        SetRotation(animator, HumanBodyBones.LeftThumbProximal, actor.GetReceivedRotation(lH01, data, false));
        SetRotation(animator, HumanBodyBones.LeftThumbIntermediate, actor.GetReceivedRotation(lH02, data, false));
        SetRotation(animator, HumanBodyBones.LeftThumbDistal, actor.GetReceivedRotation(lH03, data, false));

    }

}
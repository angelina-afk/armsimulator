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


public class ArmAnimatorForPiano : MonoBehaviour
{


    public Animator animator = null;

    public Renderer rend;

    public static int lH = 0;
    public static int lI = 1;
    public static int lM = 2;
    public static int lR = 3;
    public static int lP = 4;


    Vector3[] boneRotationOffsets = new Vector3[(int)HumanBodyBones.LastBone];


    // Use this for initialization
    void Start()
    {
        // rend.enabled = false;
        animator = GetComponent<Animator>();
        SetRotation(animator, HumanBodyBones.LeftShoulder, new Vector3(0, 90, 0));

        // SetRotation(animator, HumanBodyBones.LeftUpperArm, new Vector3(-180, 90, -80));
        // SetRotation(animator, HumanBodyBones.LeftLowerArm, new Vector3(0, 180, 100));
    }


    // Update is called once per frame
    void Update()
    {

        try
        {
            var data = DataTransfer.queue1.Dequeue();
            ApplyMotion(this, animator, data);
            print(data[3]);
        }
        catch (InvalidOperationException) { } // В очереди пусто
    }

    public Vector3 GetReceivedRotation(int bone, float[] data, bool proxm)
    {

        int offset = 0;
        offset = bone;

        // for 48
        // offset += bone * 3;
        if (proxm)
        {
            switch (bone)
            {
                // case 0:
                //     return new Vector3(0.0f, Mathf.Clamp(180.0f - data[offset], 180.0f - 30.0f, 180.0f + 30.0f), 100.0f);
                case 1:
                    return new Vector3(0.0f, 5.0f, Mathf.Clamp(-data[offset], 0.0f, 15.0f));
                case 2:
                    return new Vector3(0.0f, 5.0f, Mathf.Clamp(-data[offset], 0.0f, 15.0f));
                case 3:
                    return new Vector3(0.0f, 5.0f, Mathf.Clamp(-data[offset], 0.0f, 15.0f));
                case 4:
                    return new Vector3(0.0f, -15.0f, Mathf.Clamp(-data[offset], 0.0f, 15.0f));
                default:
                    return new Vector3(0, 0, 0);

            }
        }
        else
        {
            switch (bone)
            {
                case 0:
                    return new Vector3(0.0f, 125 - (-data[offset]), 0.0f);
                case 1:
                    return new Vector3(0.0f, 0.0f, Mathf.Clamp(-data[offset], 0.0f, 15.0f));
                case 2:
                    return new Vector3(0.0f, 0.0f, Mathf.Clamp(-data[offset], 0.0f, 15.0f));
                case 3:
                    return new Vector3(0.0f, 0.0f, Mathf.Clamp(-data[offset], 0.0f, 15.0f));
                case 4:
                    return new Vector3(0.0f, 0.0f, Mathf.Clamp(-data[offset], 0.0f, 15.0f));
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

    public static void ApplyMotion(ArmAnimatorForPiano actor, Animator animator, float[] data)
    {
        SetRotation(animator, HumanBodyBones.LeftLowerArm, actor.GetReceivedRotation(lH, data, false));
        SetRotation(animator, HumanBodyBones.LeftIndexProximal, actor.GetReceivedRotation(lI, data, true));
        SetRotation(animator, HumanBodyBones.LeftIndexIntermediate, actor.GetReceivedRotation(lI, data, false));
        SetRotation(animator, HumanBodyBones.LeftMiddleProximal, actor.GetReceivedRotation(lM, data, true));
        SetRotation(animator, HumanBodyBones.LeftMiddleIntermediate, actor.GetReceivedRotation(lM, data, false));
        SetRotation(animator, HumanBodyBones.LeftRingProximal, actor.GetReceivedRotation(lR, data, true));
        SetRotation(animator, HumanBodyBones.LeftRingIntermediate, actor.GetReceivedRotation(lR, data, false));
        SetRotation(animator, HumanBodyBones.LeftLittleProximal, actor.GetReceivedRotation(lP, data, true));
        SetRotation(animator, HumanBodyBones.LeftLittleIntermediate, actor.GetReceivedRotation(lP, data, false));

    }

}

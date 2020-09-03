using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class PianoKey : MonoBehaviour, /*IPointerEnterHandler, IPointerExitHandler,*/ IPointerDownHandler, IPointerUpHandler
{
    public int tone, octave;
    public PianoPitcher pitcher;
    GameObject piano;
    AudioClip[] clip;
    AudioMixerGroup group;
    public AudioSource curr;
    float volume = 0.25f;
    float scale = Mathf.Pow(2f, 1.0f / 12f);
    //bool needtoplay = true;

    GameObject index;
    GameObject middle;
    GameObject ring;
    GameObject pinky;
    Quaternion indexQuaternion;
    Quaternion middleQuaternion;
    Quaternion ringQuaternion;
    Quaternion pinkyQuaternion;
    float indexVA = 0.0f;
    float indexPVA = 0.0f;
    float indexAvrgVA = 0.0f;
    float middleVA = 0.0f;
    float middlePVA = 0.0f;
    float middleAvrgVA = 0.0f;
    float ringVA = 0.0f;
    float ringPVA = 0.0f;
    float ringAvrgVA = 0.0f;
    float pinkyVA = 0.0f;
    float pinkyPVA = 0.0f;
    float pinkyAvrgVA = 0.0f;


    void Start()
    {
        clip = pitcher.clip;
        group = pitcher.group;
        piano = pitcher.piano;

        index = GameObject.FindWithTag("Index");
        indexQuaternion = index.transform.localRotation;
        middle = GameObject.FindWithTag("Middle");
        middleQuaternion = middle.transform.localRotation;
        ring = GameObject.FindWithTag("Ring");
        ringQuaternion = ring.transform.localRotation;
        pinky = GameObject.FindWithTag("Pinky");
        pinkyQuaternion = pinky.transform.localRotation;


    }

    void FixedUpdate()
    {
        FingerAngularVelocity(index, indexQuaternion, indexVA, indexPVA, out indexQuaternion, out indexVA, out indexPVA, out indexAvrgVA);
        FingerAngularVelocity(middle, middleQuaternion, middleVA, middlePVA, out middleQuaternion, out middleVA, out middlePVA, out middleAvrgVA);
        FingerAngularVelocity(ring, ringQuaternion, ringVA, ringPVA, out ringQuaternion, out ringVA, out ringPVA, out ringAvrgVA);
        FingerAngularVelocity(pinky, pinkyQuaternion, pinkyVA, pinkyPVA, out pinkyQuaternion, out pinkyVA, out pinkyPVA, out pinkyAvrgVA);
    }

    public void OnCollisionEnter(Collision collision)
    {
        float vAngular = 0.0f;
        if (collision.gameObject.name == "Index")
            vAngular = indexAvrgVA;
        else if (collision.gameObject.name == "Middle")
            vAngular = middleAvrgVA;
        else if (collision.gameObject.name == "Ring")
            vAngular = ringAvrgVA;
        else if (collision.gameObject.name == "Pinky")
            vAngular = pinkyAvrgVA;

        volume = 100.0f * Mathf.Pow(vAngular, 2);
        PlayNote();
        if (curr != null)
        {
            StartCoroutine(SoundFade(curr, true));
        }
        this.transform.GetComponentInChildren<Animator>().SetBool("down", true);

        print("finger velocity " + vAngular);
    }

    public void OnCollisionExit(Collision other)
    {
        this.transform.GetComponentInChildren<Animator>().SetBool("down", false);

        if (curr != null)
        {
            StartCoroutine(SoundFade(curr, false));
        }

        // print("No longer in contact with " + other.gameObject.name);
    }
    public void OnPointerDown(PointerEventData eventData) //what happens when the key is pressed
    {
        PlayNote();
        GetComponent<Animator>().SetBool("down", true);
    }
    public void OnPointerUp(PointerEventData eventData)  //what happens when the key gets unpressed
    {
        GetComponent<Animator>().SetBool("down", false);
        if (curr != null)
        {
            StartCoroutine(SoundFade(curr, false));
        }
    }

    void PlayNote() //this part instantiates new audiosources every time the button is pressed
    {
        curr = piano.AddComponent<AudioSource>() as AudioSource;
        curr.loop = false;
        curr.volume = volume;
        curr.outputAudioMixerGroup = group;
        curr.pitch = Mathf.Pow(scale, tone);
        curr.clip = clip[pitcher.octaveOffset + octave - 1];
        curr.Play();
    }
    IEnumerator SoundFade(AudioSource source, bool stayInContact) //sound fade after the button gets unpressed
    {
        float progress = 0;
        float coef;
        if (stayInContact)
            coef = 0.1f;
        else
            coef = 0.75f;
        while (progress < 1)
        {
            progress += coef * Time.deltaTime;

            if (source != null)
                source.volume -= source.volume * progress;
            // print("volume " + source.volume);
            yield return null;
        }
        Destroy(source);
        yield return null;
    }

    void FingerAngularVelocity(GameObject finger, Quaternion prv, float prevVM, float prevPrevVM, out Quaternion prv1, out float ringVM, out float ringPVM, out float avrgVM)
    {
        Quaternion deltaRotation = finger.transform.rotation * Quaternion.Inverse(prv);

        prv1 = finger.transform.rotation;

        float angle = 0.0f;
        Vector3 axis = Vector3.zero;
        Vector3 ringV;

        deltaRotation.ToAngleAxis(out angle, out axis);

        angle *= Mathf.Deg2Rad;

        ringV = axis * angle * (1.0f / Time.fixedDeltaTime);
        ringVM = ringV.magnitude;
        ringPVM = prevVM;
        avrgVM = Mathf.Round(((ringVM + prevVM + ringPVM) / 3) * 10) / 10;
        // avrgVM = ringVM;
    }
}
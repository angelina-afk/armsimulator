using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsProcessingSB : MonoBehaviour
{
    // [SerializeField]
    // InputField timerDurationInputField;
    // [SerializeField]
    // InputField explosionAngleInputField;
    // [SerializeField]
    // InputField maxIndexAngleFemaleInputField;
    // [SerializeField]
    // InputField maxIndexAngleMaleInputField;
    // [SerializeField]
    // Toggle boolExplodeToggle;


    // Start is called before the first frame update
    void Start()
    {
        float timerDuration = PlayerPrefs.GetFloat("TimerDuration", 2f);
        // float ballsCount = PlayerPrefs.GetFloat("BallsCount", 5f);
        float explosionAngle = PlayerPrefs.GetFloat("ExplosionAngle", 0f);
        float thumbBias = PlayerPrefs.GetFloat("ThumbBias", 50f);
        float indexBias = PlayerPrefs.GetFloat("IndexBias", 50f);

        InputField timerDurationInputField = GameObject.FindWithTag("TimerDuration").GetComponent<InputField>();
        // InputField ballsCountInputField = GameObject.FindWithTag("BallsCount").GetComponent<InputField>();
        InputField explosionAngleInputField = GameObject.FindWithTag("ExplosionAngle").GetComponent<InputField>();

        InputField thumbBiasInputField = GameObject.FindWithTag("ThumbBias").GetComponent<InputField>();
        InputField indexBiasInputField = GameObject.FindWithTag("IndexBias").GetComponent<InputField>();

        timerDurationInputField.text = timerDuration.ToString();
        // ballsCountInputField.text = ballsCount.ToString();
        thumbBiasInputField.text = thumbBias.ToString();
        indexBiasInputField.text = indexBias.ToString();
        explosionAngleInputField.text = explosionAngle.ToString();



        timerDurationInputField.onValueChange.AddListener(delegate { SaveGamertag(timerDurationInputField.text, "TimerDuration"); });
        // ballsCountInputField.onValueChange.AddListener(delegate { SaveGamertag(ballsCountInputField.text, "BallsCount"); });
        explosionAngleInputField.onValueChange.AddListener(delegate { SaveGamertag(explosionAngleInputField.text, "ExplosionAngle"); });
        thumbBiasInputField.onValueChange.AddListener(delegate { SaveGamertag(thumbBiasInputField.text, "ThumbBias"); });
        indexBiasInputField.onValueChange.AddListener(delegate { SaveGamertag(indexBiasInputField.text, "IndexBias"); });
    }

    void SaveGamertag(string value, string name)
    {
        PlayerPrefs.SetFloat(name, float.Parse(value));
        // PlayerPrefs.SetString(name, value);
        PlayerPrefs.Save();
    }
    void SaveInt(int value, string name)
    {
        PlayerPrefs.SetInt(name, value);
        // PlayerPrefs.SetString(name, value);
        PlayerPrefs.Save();
    }
}
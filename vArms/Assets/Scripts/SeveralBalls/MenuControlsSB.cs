using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MenuControlsSB : MonoBehaviour
{
    public void TimerDurationPlusPressed()
    {
        InputField inputField = GameObject.FindWithTag("TimerDuration").GetComponent<InputField>();
        inputField.text = (float.Parse(inputField.text) + 0.5f).ToString();
    }
    public void TimerDurationMinusPressed()
    {
        InputField inputField = GameObject.FindWithTag("TimerDuration").GetComponent<InputField>();
        inputField.text = (float.Parse(inputField.text) - 0.5f).ToString();
    }

    public void ExplosionAngleMinusPressed()
    {
        InputField inputField = GameObject.FindWithTag("ExplosionAngle").GetComponent<InputField>();
        inputField.text = (float.Parse(inputField.text) - 0.5f).ToString();
    }
    public void ExplosionAnglePlusPressed()
    {
        InputField inputField = GameObject.FindWithTag("ExplosionAngle").GetComponent<InputField>();
        inputField.text = (float.Parse(inputField.text) + 0.5f).ToString();
    }
    public void ThumbBiasMinusPressed()
    {
        InputField inputField = GameObject.FindWithTag("ThumbBias").GetComponent<InputField>();
        inputField.text = (float.Parse(inputField.text) - 1f).ToString();
    }
    public void ThumbBiasPlusPressed()
    {
        InputField inputField = GameObject.FindWithTag("ThumbBias").GetComponent<InputField>();
        inputField.text = (float.Parse(inputField.text) + 1f).ToString();
    }
    public void IndexBiasMinusPressed()
    {
        InputField inputField = GameObject.FindWithTag("IndexBias").GetComponent<InputField>();
        inputField.text = (float.Parse(inputField.text) - 1f).ToString();
    }
    public void IndexBiasPlusPressed()
    {
        InputField inputField = GameObject.FindWithTag("IndexBias").GetComponent<InputField>();
        inputField.text = (float.Parse(inputField.text) + 1f).ToString();
    }
    public void ResetSettings()
    {
        float timerDurationDefolt = 2f;
        float explosionAngleDefolt = 0f;

        float thumbBiasDefolt = 50f;
        float indexBiasDefolt = 50f;

        ResettingSupport("TimerDuration", timerDurationDefolt);
        ResettingSupport("ExplosionAngle", explosionAngleDefolt);

        ResettingSupport("ThumbBias", thumbBiasDefolt);
        ResettingSupport("IndexBias", indexBiasDefolt);
    }

    void ResettingSupport(string name, float value)
    {
        InputField inputField = GameObject.FindWithTag(name).GetComponent<InputField>();

        PlayerPrefs.SetFloat(name, value);
        PlayerPrefs.Save();
        inputField.text = value.ToString();
    }

    public void SaveData()
    {
        StreamWriter file = new StreamWriter(Path.Combine(
                Application.streamingAssetsPath, "ConfigurationDataEditableBS.csv"));

        file.WriteLine("TimerDuration;BallsCount;ExplosionAngle;ThumbBias;IndexBias");
        file.Write(PlayerPrefs.GetFloat("TimerDuration"));
        file.Write(";");
        file.Write(PlayerPrefs.GetFloat("BallsCount"));
        file.Write(";");
        file.Write(PlayerPrefs.GetFloat("ExplosionAngle"));
        file.Write(";");
        file.Write(PlayerPrefs.GetFloat("ThumbBias"));
        file.Write(";");
        file.Write(PlayerPrefs.GetFloat("IndexBias"));

        file.Close();
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MenuControls : MonoBehaviour
{
    public void CubeWidthPlusPressed()
    {
        InputField inputField = GameObject.FindWithTag("CubeWidth").GetComponent<InputField>();
        inputField.text = (float.Parse(inputField.text) + 0.01f).ToString();
    }
    public void CubeWidthMinusPressed()
    {
        InputField inputField = GameObject.FindWithTag("CubeWidth").GetComponent<InputField>();
        inputField.text = (float.Parse(inputField.text) - 0.01f).ToString();
    }
    public void CubeXLocPlusPressed()
    {
        InputField inputField = GameObject.FindWithTag("CubeXCoord").GetComponent<InputField>();
        inputField.text = (float.Parse(inputField.text) + 0.0005f).ToString();
    }
    public void CubeXLocMinusPressed()
    {
        InputField inputField = GameObject.FindWithTag("CubeXCoord").GetComponent<InputField>();
        inputField.text = (float.Parse(inputField.text) - 0.0005f).ToString();
    }
    public void CubeYLocPlusPressed()
    {
        InputField inputField = GameObject.FindWithTag("CubeYCoord").GetComponent<InputField>();
        inputField.text = (Mathf.Round((float.Parse(inputField.text) + 0.0005f) * 10000) / 10000).ToString();
    }
    public void CubeYLocMinusPressed()
    {
        InputField inputField = GameObject.FindWithTag("CubeYCoord").GetComponent<InputField>();
        inputField.text = (Mathf.Round((float.Parse(inputField.text) - 0.0005f) * 10000) / 10000).ToString();
    }
    public void CubeZLocPlusPressed()
    {
        InputField inputField = GameObject.FindWithTag("CubeZCoord").GetComponent<InputField>();
        inputField.text = (Mathf.Round((float.Parse(inputField.text) + 0.0005f) * 10000) / 10000).ToString();
    }
    public void CubeZLocMinusPressed()
    {
        InputField inputField = GameObject.FindWithTag("CubeZCoord").GetComponent<InputField>();
        inputField.text = (Mathf.Round((float.Parse(inputField.text) - 0.0005f) * 10000) / 10000).ToString();
    }
    // public void CubeRotationAngleMinusPressed()
    // {
    //     InputField inputField = GameObject.FindWithTag("CubeRotationAngle").GetComponent<InputField>();
    //     inputField.text = (float.Parse(inputField.text) - 0.5f).ToString();
    // }
    // public void CubeRotationAnglePlusPressed()
    // {
    //     InputField inputField = GameObject.FindWithTag("CubeRotationAngle").GetComponent<InputField>();
    //     inputField.text = (float.Parse(inputField.text) + 0.5f).ToString();
    // }
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
    public void MaxIndexAngleFemaleMinusPressed()
    {
        InputField inputField = GameObject.FindWithTag("MaxIndexAngleFemale").GetComponent<InputField>();
        inputField.text = (float.Parse(inputField.text) - 0.5f).ToString();
    }
    public void MaxIndexAngleFemalePlusPressed()
    {
        InputField inputField = GameObject.FindWithTag("MaxIndexAngleFemale").GetComponent<InputField>();
        inputField.text = (float.Parse(inputField.text) + 0.5f).ToString();
    }
    public void MaxIndexAngleMaleMinusPressed()
    {
        InputField inputField = GameObject.FindWithTag("MaxIndexAngleMale").GetComponent<InputField>();
        inputField.text = (float.Parse(inputField.text) - 0.5f).ToString();
    }
    public void MaxIndexAngleMalePlusPressed()
    {
        InputField inputField = GameObject.FindWithTag("MaxIndexAngleMale").GetComponent<InputField>();
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
        float cubeWidthDefolt = 1.3f;
        float cubeXCoordDefolt = 0.13f;
        float cubeYCoordDefolt = -0.255f;
        float cubeZCoordDefolt = 0.565f;
        // float cubeRotationAngleDefolt = 0f;
        float explosionAngleDefolt = -25f;
        float maxIndexAngleFemaleDefolt = -20f;
        float maxIndexAngleMaleDefolt = -15f;
        float thumbBiasDefolt = 50f;
        float indexBiasDefolt = 50f;

        ResettingSupport("CubeWidth", cubeWidthDefolt);
        ResettingSupport("CubeXCoord", cubeXCoordDefolt);
        ResettingSupport("CubeYCoord", cubeYCoordDefolt);
        ResettingSupport("CubeZCoord", cubeZCoordDefolt);
        // ResettingSupport("CubeRotationAngle", cubeRotationAngleDefolt);
        ResettingSupport("ExplosionAngle", explosionAngleDefolt);
        ResettingSupport("MaxIndexAngleFemale", maxIndexAngleFemaleDefolt);
        ResettingSupport("MaxIndexAngleMale", maxIndexAngleMaleDefolt);
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
                Application.streamingAssetsPath, "ConfigurationDataEditable.csv"));

        file.WriteLine("CubeWidth;CubeXCoord;CubeYCoord;CubeZCoord;ExplosionAngle;MaxIndexAngleFemale;MaxIndexAngleMale;ThumbBias;IndexBias");
        file.Write(PlayerPrefs.GetFloat("CubeWidth"));
        file.Write(";");
        file.Write(PlayerPrefs.GetFloat("CubeXCoord"));
        file.Write(";");
        file.Write(PlayerPrefs.GetFloat("CubeYCoord"));
        file.Write(";");
        file.Write(PlayerPrefs.GetFloat("CubeZCoord"));
        // file.Write(";");
        // file.Write(PlayerPrefs.GetFloat("CubeRotationAngle"));
        file.Write(";");
        file.Write(PlayerPrefs.GetFloat("ExplosionAngle"));
        file.Write(";");
        file.Write(PlayerPrefs.GetFloat("MaxIndexAngleFemale"));
        file.Write(";");
        file.Write(PlayerPrefs.GetFloat("MaxIndexAngleMale"));
        file.Write(";");
        file.Write(PlayerPrefs.GetFloat("ThumbBias"));
        file.Write(";");
        file.Write(PlayerPrefs.GetFloat("IndexBias"));

        file.Close();
    }
    // public void PlayPressed()
    // {
    //     SceneManager.LoadScene("CubeTouch");
    // }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsProcessing : MonoBehaviour
{
    [SerializeField]
    InputField cubeWidthInputField;
    [SerializeField]
    InputField cubeXLocInputField;
    [SerializeField]
    InputField cubeYLocInputField;
    // [SerializeField]
    // InputField cubeRotationAngleInputField;
    [SerializeField]
    InputField explosionAngleInputField;
    [SerializeField]
    InputField maxIndexAngleFemaleInputField;
    [SerializeField]
    InputField maxIndexAngleMaleInputField;
    [SerializeField]
    Toggle boolExplodeToggle;


    // Start is called before the first frame update
    void Start()
    {
        float boolExplode = PlayerPrefs.GetInt("BoolExplode", 1);
        float cubeWidth = PlayerPrefs.GetFloat("CubeWidth", 1.3f);
        float cubeXLoc = PlayerPrefs.GetFloat("CubeXCoord", 0.13f);
        float cubeYLoc = PlayerPrefs.GetFloat("CubeYCoord", -0.255f);
        float cubeZLoc = PlayerPrefs.GetFloat("CubeZCoord", 0.565f);
        // float cubeRotationAngle = PlayerPrefs.GetFloat("CubeRotationAngle", 0f);
        float explosionAngle = PlayerPrefs.GetFloat("ExplosionAngle", -25f);
        float maxIndexAngleFemale = PlayerPrefs.GetFloat("MaxIndexAngleFemale", -20f);
        float maxIndexAngleMale = PlayerPrefs.GetFloat("MaxIndexAngleMale", -15f);
        float thumbBias = PlayerPrefs.GetFloat("ThumbBias", 50f);
        float indexBias = PlayerPrefs.GetFloat("IndexBias", 50f);

        InputField cubeZLocInputField = GameObject.FindWithTag("CubeZCoord").GetComponent<InputField>();
        InputField thumbBiasInputField = GameObject.FindWithTag("ThumbBias").GetComponent<InputField>();
        InputField indexBiasInputField = GameObject.FindWithTag("IndexBias").GetComponent<InputField>();

        cubeZLocInputField.text = cubeZLoc.ToString();
        thumbBiasInputField.text = thumbBias.ToString();
        indexBiasInputField.text = indexBias.ToString();

        cubeWidthInputField.text = cubeWidth.ToString();
        cubeXLocInputField.text = cubeXLoc.ToString();
        cubeYLocInputField.text = cubeYLoc.ToString();
        // cubeRotationAngleInputField.text = cubeRotationAngle.ToString();
        explosionAngleInputField.text = explosionAngle.ToString();
        maxIndexAngleFemaleInputField.text = maxIndexAngleFemale.ToString();
        maxIndexAngleMaleInputField.text = maxIndexAngleMale.ToString();
        boolExplodeToggle.isOn = (boolExplode == 1) ? true : false;


        cubeWidthInputField.onValueChange.AddListener(delegate { SaveGamertag(cubeWidthInputField.text, "CubeWidth"); });
        cubeXLocInputField.onValueChange.AddListener(delegate { SaveGamertag(cubeXLocInputField.text, "CubeXCoord"); });
        cubeYLocInputField.onValueChange.AddListener(delegate { SaveGamertag(cubeYLocInputField.text, "CubeYCoord"); });
        cubeZLocInputField.onValueChange.AddListener(delegate { SaveGamertag(cubeZLocInputField.text, "CubeZCoord"); });
        // cubeRotationAngleInputField.onValueChange.AddListener(delegate { SaveGamertag(cubeRotationAngleInputField.text, "CubeRotationAngle"); });
        explosionAngleInputField.onValueChange.AddListener(delegate { SaveGamertag(explosionAngleInputField.text, "ExplosionAngle"); });
        maxIndexAngleFemaleInputField.onValueChange.AddListener(delegate { SaveGamertag(maxIndexAngleFemaleInputField.text, "MaxIndexAngleFemale"); });
        maxIndexAngleMaleInputField.onValueChange.AddListener(delegate { SaveGamertag(maxIndexAngleMaleInputField.text, "MaxIndexAngleMale"); });
        boolExplodeToggle.onValueChanged.AddListener(delegate { SaveInt(boolExplodeToggle.isOn ? 1 : 0, "BoolExplode"); });

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

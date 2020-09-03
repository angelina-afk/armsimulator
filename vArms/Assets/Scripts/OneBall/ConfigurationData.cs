using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class ConfigurationData : MonoBehaviour
{
    const string ConfigurationDataFileName = "ConfigurationData.csv";

    float cubeWidth = 0.02f;
    float explosionAngle = 45f;
    float maxIndexAngleFemale = 35f;
    float maxIndexAngleMale = 40f;

    public float CubeWidth
    {
        get { return cubeWidth; }
    }

    public float ExplosionAngle
    {
        get { return explosionAngle; }
    }

    public float MaxIndexAngleFemale
    {
        get { return maxIndexAngleFemale; }
    }
    public float MaxIndexAngleMale
    {
        get { return maxIndexAngleMale; }
    }

    public ConfigurationData()
    {
        // read and save configuration data from file
        StreamReader input = null;
        try
        {
            // create stream reader object
            input = File.OpenText(Path.Combine(
                Application.streamingAssetsPath, ConfigurationDataFileName));

            // read in names and values
            string names = input.ReadLine();
            string values = input.ReadLine();

            // set configuration data fields
            SetConfigurationDataFields(values);
        }
        catch (Exception e)
        {
        }
        finally
        {
            // always close input file
            if (input != null)
            {
                input.Close();
            }
        }
    }

    void SetConfigurationDataFields(string csvValues)
    {
        string[] values = csvValues.Split(';');
        cubeWidth = float.Parse(values[0]);
        explosionAngle = float.Parse(values[1]);
        maxIndexAngleFemale = float.Parse(values[2]);
        maxIndexAngleMale = float.Parse(values[3]);
    }
}

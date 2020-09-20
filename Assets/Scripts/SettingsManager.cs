﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    readonly static string kSettingsFilePath = "settings.cfg";
    public static Dictionary<string, string> Settings { get; set; }

    // Awake is called before all Start() calls
    private void Awake()
    {
        InitSettings();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Retrieve settings from config
    /// </summary>
    public static void InitSettings()
    {
        Settings = new Dictionary<string, string>();

        string settingsFilePath = Path.Combine(Application.dataPath, kSettingsFilePath);

        using (StreamReader reader = new StreamReader(settingsFilePath))
        {
            string line = reader.ReadLine();
            while (string.IsNullOrEmpty(line) == false)
            {
                string[] id_value = line.Split('=');
                if (id_value.Length == 2)
                {
                    Settings.Add(id_value[0], id_value[1]);
                }
                line = reader.ReadLine();
            }
        }
    }
}

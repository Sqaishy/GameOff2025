using UnityEngine;
using System.Collections.Generic;
using System.IO;
using Unity.Mathematics;

/// <summary>
/// Static class for JSON-based save- and loading functions
/// Manages player data and user settings.
/// 
/// todo: implement player data
/// 
/// </summary>

public class UserSettingsJSON
{
    private const int SAVE_FILE_VERSION = 1;
    private const string JSON_EXTENSTION = ".json";

    public struct UserSettings
    {
        public float masterVolume;
        public float musicVolume;
        public float sfxVolume;
        public float ambienceVolume;
        public int saveFileVersion;
    }

    /// <summary>
    /// Saves the user settings as a JSON-file
    /// </summary>
    /// <param name="data">Data which is supposed to be saved.</param>
    /// <param name="filename">Filename without extension.</param>
    public static void SaveUserSettings(UserSettings data, string filename)
    {
        data.saveFileVersion = SAVE_FILE_VERSION;
        string json = JsonUtility.ToJson(data, true);
        string filePath = GetFilePath(filename);
        File.WriteAllText(filePath, json);
    }

    /// <summary>
    /// Loads the user settings from a JSON-file
    /// </summary>
    /// <param name="filename">The to be loaded filename without extension.</param>
    /// <returns>Loaded user settings or default values.</returns>
    public static UserSettings LoadUserSettings(string filename)
    {
        string filePath = GetFilePath(filename);

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            return JsonUtility.FromJson<UserSettings>(json);
        }

        return new UserSettings();
    }

    /// <summary>
    /// Creates a complete path for the input filename
    /// </summary>
    /// <param name="filename">Filename without extension.</param>
    /// <returns>Complete filename-path</returns>
    private static string GetFilePath(string filename)
    {
        return Path.Combine(Application.persistentDataPath, filename + JSON_EXTENSTION);
    }

    /// <summary>
    /// Deletes a savefile
    /// </summary>
    /// <param name="filename">The to be deleted savefile's name.</param>
    private static void ClearData(string filename)
    {
        string filePath = GetFilePath(filename);
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }
}

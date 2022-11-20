using System;
using UnityEditor;

#if UNITY_EDITOR
[InitializeOnLoad]
class ChangePathJava
{
    static ChangePathJava()
    {
        string newJDKPath = EditorApplication.applicationPath.Replace("Unity.exe", "Data/PlaybackEngines/AndroidPlayer/OpenJDK");

        if (Environment.GetEnvironmentVariable("JAVA_HOME") != newJDKPath)
        {
            Environment.SetEnvironmentVariable("JAVA_HOME", newJDKPath);
        }
    }
}
#endif
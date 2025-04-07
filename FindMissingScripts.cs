// COPYRIGHT 2025  @Neil Augereau / github : neilaugereau
using UnityEngine;
using UnityEditor;

public class MissingScriptsFinder
{
    [MenuItem("Tools/Find Missing Scripts In Scene")]
    static void FindMissingScripts()
    {
        int missingCount = 0;

        foreach (GameObject go in GameObject.FindObjectsOfType<GameObject>())
        {
            Component[] components = go.GetComponents<Component>();
            for (int i = 0; i < components.Length; i++)
            {
                if (components[i] == null)
                {
                    string fullPath = GetGameObjectPath(go);
                    Debug.LogWarning($"Missing script on: '{go.name}' at path: {fullPath}", go);
                    missingCount++;
                }
            }
        }

        Debug.Log($"Search complete. Total missing scripts found: {missingCount}");
    }

    // Builds the full path in the hierarchy (e.g. "Root/Child/Target")
    static string GetGameObjectPath(GameObject obj)
    {
        string path = obj.name;
        Transform current = obj.transform;
        while (current.parent != null)
        {
            current = current.parent;
            path = current.name + "/" + path;
        }
        return path;
    }
}
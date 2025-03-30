using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;

public class PrefabCreatorAndLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string searchName;
    public string prefabName;
    private string savePath = "Assets/SavedPrefabs";
    public TMP_InputField prefabSearchInput;
    public TMP_InputField prefabNameInput;
    public TMP_InputField prefabLoadInput;

    public void AttemptToSavePrefab()
    {
        string searchName = prefabSearchInput.text.Trim();


        if (string.IsNullOrEmpty(searchName))
        {
            Debug.LogWarning("Search field is empty! Enter a name or tag.");
            return;
        }

        if (SavePrefabByName(searchName) == false)
        {
            SavePrefabsByTag(searchName);
        }
    }

    public bool SavePrefabByName(string t_name)
    {
        string saveName = prefabNameInput.text.Trim();

        GameObject foundObject = GameObject.Find(t_name);
        if (foundObject != null)
        {
            string finalPrefabName = string.IsNullOrEmpty(saveName) ? foundObject.name : saveName;
            SavePrefab(foundObject, finalPrefabName);
            return true;
        }
        else
        {
            Debug.LogWarning("GameObject with name '" + t_name + "' not found.");
        }
        return false;
    }

    public bool SavePrefabsByTag(string t_name)
    {
        string saveName = prefabNameInput.text.Trim();

        GameObject[] foundObjects = GameObject.FindGameObjectsWithTag(t_name);
        if (foundObjects.Length > 0)
        {
            foreach (GameObject obj in foundObjects)
            {
                string finalPrefabName = string.IsNullOrEmpty(saveName) ? obj.name : saveName + "_" + obj.name;
                SavePrefab(obj, finalPrefabName);
            }
        }
        else
        {
            Debug.LogWarning("No GameObjects with tag '" + t_name + "' found.");
        }
        return false;
    }

    private void SavePrefab(GameObject obj, string prefabFileName)
    {
        if (!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
            AssetDatabase.Refresh();
        }

        string fullPath = Path.Combine(savePath, prefabFileName + ".prefab");

        PrefabUtility.SaveAsPrefabAsset(obj, fullPath);
        Debug.Log("Prefab saved at: " + fullPath);
    }
}

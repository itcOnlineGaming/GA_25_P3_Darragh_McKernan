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
        UpdatePrefabDropdown();
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
    public TMP_Dropdown loadPrefabUI;
    private List<string> prefabNames = new List<string>();
    private List<GameObject> generatedPrefabs = new List<GameObject>();
    Vector3 spawnPoint = new Vector3(4, 5, 0);

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

        UpdatePrefabDropdown();
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
        if (obj.GetComponent<DataTracker>() == null)
        {
            obj.AddComponent<DataTracker>();
        }

        if (!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
            AssetDatabase.Refresh();
        }

        string fullPath = Path.Combine(savePath, prefabFileName + ".prefab");

        PrefabUtility.SaveAsPrefabAsset(obj, fullPath);
        Debug.Log("Prefab saved at: " + fullPath);
    }

    public void LoadSavedPrefab()
    {
        string prefabName = prefabLoadInput.text.Trim();

        if (string.IsNullOrEmpty(prefabName))
        {
            Debug.LogWarning("Please enter a prefab name to load.");
            return;
        }

        string fullPath = Path.Combine(savePath, prefabName + ".prefab");
        GameObject loadedPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(fullPath);

        if (loadedPrefab != null)
        {
            generatedPrefabs.Add(Instantiate(loadedPrefab, spawnPoint, Quaternion.identity));
            spawnPoint.y -= 2;
            Debug.Log("Prefab loaded and instantiated: " + prefabName);
        }
        else
        {
            Debug.LogWarning("Prefab not found: " + fullPath);
        }
    }

    public void LoadSelectedPrefab()
    {
        if (loadPrefabUI.options.Count == 0)
        {
            Debug.LogWarning("No prefabs available to load.");
            return;
        }

        string selectedPrefab = loadPrefabUI.options[loadPrefabUI.value].text;

        if (selectedPrefab == "No Prefabs Found")
        {
            Debug.LogWarning("No valid prefab selected.");
            return;
        }

        InstantiatePrefab(selectedPrefab);
    }

    private void InstantiatePrefab(string prefabName)
    {
        string fullPath = Path.Combine(savePath, prefabName + ".prefab");
        GameObject loadedPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(fullPath);

        if (loadedPrefab != null)
        {
            generatedPrefabs.Add(Instantiate(loadedPrefab, spawnPoint, Quaternion.identity));
            spawnPoint.y -= 2;
            Debug.Log("Prefab loaded and instantiated: " + prefabName);
        }
        else
        {
            Debug.LogWarning("Prefab not found: " + fullPath);
        }
    }

    public void UpdatePrefabDropdown()
    {
        loadPrefabUI.ClearOptions();
        prefabNames.Clear();

        string[] prefabFiles = Directory.GetFiles(savePath, "*.prefab");
        foreach (string filePath in prefabFiles)
        {
            string prefabName = Path.GetFileNameWithoutExtension(filePath);
            prefabNames.Add(prefabName);
        }

        if (prefabNames.Count > 0)
        {
            loadPrefabUI.AddOptions(prefabNames);
        }
        else
        {
            loadPrefabUI.AddOptions(new List<string> { "No Prefabs Found" });
        }
    }

    public void ClearPrefabs()
    {
        for(int index = 0;index < generatedPrefabs.Count;index++)
        {
            Destroy(generatedPrefabs[index]);
        }
        generatedPrefabs.Clear();
        spawnPoint = new Vector3(4, 5, 0);
    }
}
